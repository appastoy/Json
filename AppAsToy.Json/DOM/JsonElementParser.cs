using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace AppAsToy.Json.DOM;

internal ref struct JsonElementParser
{
    private ReadOnlySpan<char> _json;
    private int _index;
    private int _line;
    private int _column;

    private int RemainLength => _json.Length - _index;

    public JsonElementParser(string json)
    {
        _json = json;
        _index = 0;
        _line = 0;
        _column = 0;
    }

    public IJsonElement Parse()
    {
        return ParseElement();
    }

    private IJsonElement ParseElement()
    {
        return MoveNextToken() switch
        {
            'n' => ParseNull(),
            'f' => ParseFalse(),
            't' => ParseTrue(),
            '"' => ParseString(),
            '[' => ParseArray(),
            '{' => ParseObject(),
            '-' or '0' or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9' => ParseNumber(),
            _ => throw InvalidToken($"Unexpected character. ('{_json[_index]}'({(int)(_json[_index])}))"),
        };
    }

    private char MoveNextToken()
    {
        SkipWhiteSpace();
        if (_index >= _json.Length)
            throw UnexpectedEndReached();

        return _json[_index];
    }

    private void SkipWhiteSpace()
    {
        while (_index < _json.Length)
        {
            switch (_json[_index])
            {
                case ' ':
                case '\t':
                case '\r':
                    StepForward(1);
                    break;

                case '\n':
                    StepNewLine();
                    break;

                default:
                    return;
            }
        }
    }

    private IJsonElement ParseNull()
    {
        if (CanParseWord("null"))
        {
            StepForward(4);
            return JsonElement.Null;
        }
        throw InvalidToken(4);
    }

    private IJsonElement ParseTrue()
    {
        if (CanParseWord("true"))
        {
            StepForward(4);
            return JsonBool.True;
        }
        throw InvalidToken(4);
    }

    private IJsonElement ParseFalse()
    {
        if (CanParseWord("false"))
        {
            StepForward(5);
            return JsonBool.False;
        }
        throw InvalidToken(5);
    }

    private IJsonElement ParseString()
    {
        if (RemainLength < 2)
            throw InvalidToken("String value is terminated abnormally.");

        var builder = new StringBuilder(4096);
        int currentIndex = _index + 1;
        while (currentIndex < _json.Length)
        {
            var ch = _json[currentIndex];
            if (ch == '\\')
            {
                builder.Append(ParseEscapedString(ref currentIndex));
                continue;
            }
            if (ch == '"')
                break;

            builder.Append(ch);
            currentIndex++;
        }
        if (currentIndex >= _json.Length)
            throw UnexpectedEndReached();

        StepForward(currentIndex - _index + 1);

        if (builder.Length == 0)
            return JsonString.Empty;

        return new JsonString(builder.ToString());
    }

    private char ParseEscapedString(ref int currentIndex)
    {
        currentIndex++;
        if (currentIndex >= _json.Length)
            throw UnexpectedEndReached();

        var ch = _json[currentIndex++];
        if (ch == 'u')
        {
            if ((_json.Length - currentIndex) < 4)
                throw InvalidToken("Unicode value is terminated abnormally.");

            if (!char.TryParse(Regex.Unescape(new string(_json.Slice(currentIndex - 2, 6))), out var unicodeChar))
                throw InvalidToken("Unicode value is not valid.");

            currentIndex += 4;
            return unicodeChar;
        }

        return ch switch
        {
            'b' => '\b',
            'f' => '\f',
            'n' => '\n',
            'r' => '\r',
            't' => '\t',
            '/' => '/',
            '\\' => '\\',
            '"' => '"',
            _ => throw InvalidToken($"Unexpected character is escaped. ('{ch}'({(int)ch}))")
        };
    }

    private IJsonElement ParseNumber()
    {
        const NumberStyles jsonNumberStyles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign;

        var length = GetNumberEndIndex() - _index;
        var numberString = _json.Slice(_index, length);
        if (!double.TryParse(numberString, jsonNumberStyles, CultureInfo.InvariantCulture, out var value))
            throw InvalidToken($"Can't parse to number. (\"{new string(numberString)}\")");

        StepForward(length);

        if (value == 0d)
            return JsonNumber.Zero;

        return new JsonNumber(value);
    }

    bool CanParseWord(ReadOnlySpan<char> word)
    {
        if (RemainLength < word.Length)
            return false;

        if (!word.SequenceEqual(_json.Slice(_index, word.Length)))
            return false;

        var nextCharacterIndex = _index + word.Length;
        if (nextCharacterIndex >= _json.Length)
            return true;

        return _json[nextCharacterIndex] switch
        {
            ',' or '\r' or '\n' or ' ' or '\t' or ']' or '}' => true,
            _ => false
        };
    }

    int GetNumberEndIndex()
    {
        var currentIndex = _index;
        while (currentIndex < _json.Length)
        {
            switch (_json[currentIndex])
            {
                case '0': case '1': case '2': case '3': case '4':
                case '5': case '6': case '7': case '8': case '9':
                case '-': case '+': case '.': case 'e': case 'E':
                    currentIndex += 1;
                    continue;

                default:
                    return currentIndex;
            }
        }

        return currentIndex;
    }

    private IJsonElement ParseArray()
    {
        if (RemainLength < 2)
            throw InvalidToken("Array is terminated abnormally.");

        StepForward(1);
        var ch = MoveNextToken();
        if (ch == ']')
        {
            StepForward(1);
            return JsonArray.Empty;
        }

        var array = new JsonArray();
        while (_index < _json.Length)
        {
            array.Add((JsonElement)ParseElement());
            ch = MoveNextToken();
            if (ch == ',')
            {
                StepForward(1);
                ch = MoveNextToken();
            }
            if (ch == ']')
            {
                StepForward(1);
                return array;
            }
        }
        throw UnexpectedEndReached();
    }

    private IJsonElement ParseObject()
    {
        if (RemainLength < 2)
            throw InvalidToken("Object is terminated abnormally.");

        StepForward(1);
        var ch = MoveNextToken();
        if (ch == '}')
        {
            StepForward(1);
            return JsonObject.Empty;
        }

        var obj = new JsonObject();
        while (_index < _json.Length)
        {
            ParseObjectProperty(obj);
            ch = MoveNextToken();
            if (ch == ',')
            {
                StepForward(1);
                ch = MoveNextToken();
            }
            if (ch == '}')
            {
                StepForward(1);
                return obj;
            }
        }
        throw UnexpectedEndReached();
    }

    private void ParseObjectProperty(JsonObject obj)
    {
        if (_json[_index] != '"')
            throw InvalidToken("Object property key is not a string value.");

        var key = ParseString();
        var ch = MoveNextToken();
        if (ch != ':')
            throw InvalidToken("Object property doesn't have colon(:).");

        StepForward(1);
        var value = ParseElement();

        obj.Add(key.ToStringValue, (JsonElement)value);
    }

    private void StepForward(int count)
    {
        _index += count;
        _column += count;
    }

    private void StepNewLine()
    {
        _index += 1;
        _line += 1;
        _column = 0;
    }

    private JsonElementParsingException UnexpectedEndReached()
    {
        return new JsonElementParsingException("Parsing has not yet completed, but the end of the string has been reached.", _line, _column);
    }

    private JsonElementParsingException InvalidToken(string message)
    {
        return new JsonElementParsingException($"Invalid token - {message}", _line, _column);
    }

    private JsonElementParsingException InvalidToken(int characterCount)
    {
        return new JsonElementParsingException($"Invalid token - \"{new string(_json.Slice(_index, Math.Min(_json.Length - _index, characterCount)))}\"", _line, _column);
    }
}
