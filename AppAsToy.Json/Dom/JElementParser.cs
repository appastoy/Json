using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace AppAsToy.Json;

internal ref struct JElementParser
{
    private ReadOnlySpan<char> _json;
    private int _index;
    private int _line;
    private int _column;

    private int RemainLength => _json.Length - _index;

    public JElementParser(string json)
    {
        _json = json;
        _index = 0;
        _line = 0;
        _column = 0;
    }

    public JElement Parse()
    {
        return ParseElement();
    }

    private JElement ParseElement()
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

    private JElement ParseNull()
    {
        if (TryParseKeyword("null"))
        {
            StepForward(4);
            return JElement.Null;
        }
        throw InvalidToken(4);
    }

    private JElement ParseTrue()
    {
        if (TryParseKeyword("true"))
        {
            StepForward(4);
            return JBool.True;
        }
        throw InvalidToken(4);
    }

    private JElement ParseFalse()
    {
        if (TryParseKeyword("false"))
        {
            StepForward(5);
            return JBool.False;
        }
        throw InvalidToken(5);
    }

    private JElement ParseString()
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
            return JString.Empty;

        return new JString(builder.ToString());
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

            if (!ushort.TryParse(_json.Slice(currentIndex, 4), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out var result))
                throw InvalidToken("Unicode value is not valid.");

            currentIndex += 4;
            return (char)result;
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

    private JElement ParseNumber()
    {
        const NumberStyles jsonNumberStyles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign;

        var length = GetNumberEndIndex() - _index;
        var numberString = _json.Slice(_index, length);
        if (!double.TryParse(numberString, jsonNumberStyles, CultureInfo.InvariantCulture, out var value))
            throw InvalidToken($"Can't parse to number. (\"{new string(numberString)}\")");

        StepForward(length);

        if (value == 0d)
            return JNumber.Zero;

        return new JNumber(value);
    }

    bool TryParseKeyword(ReadOnlySpan<char> word)
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

    private JElement ParseArray()
    {
        if (RemainLength < 2)
            throw InvalidToken("Array is terminated abnormally.");

        StepForward(1);

        var array = new JArray();
        var ch = MoveNextToken();
        if (ch == ']')
        {
            StepForward(1);
            return array;
        }
        
        while (_index < _json.Length)
        {
            array.Add(ParseElement());
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

    private JElement ParseObject()
    {
        if (RemainLength < 2)
            throw InvalidToken("Object is terminated abnormally.");

        StepForward(1);

        var obj = new JObject();
        var ch = MoveNextToken();
        if (ch == '}')
        {
            StepForward(1);
            return obj;
        }
        
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

    private void ParseObjectProperty(JObject obj)
    {
        if (_json[_index] != '"')
            throw InvalidToken("Object property key is not a string value.");

        var key = ParseString();
        var ch = MoveNextToken();
        if (ch != ':')
            throw InvalidToken("Object property doesn't have colon(:).");

        StepForward(1);
        var value = ParseElement();

        obj.Add(key.ToStringValue, value);
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

    private JElementParseException UnexpectedEndReached()
    {
        return new JElementParseException("Parsing has not yet completed, but the end of the string has been reached.", _line, _column);
    }

    private JElementParseException InvalidToken(string message)
    {
        return new JElementParseException($"Invalid token - {message}", _line, _column);
    }

    private JElementParseException InvalidToken(int characterCount)
    {
        return new JElementParseException($"Invalid token - \"{new string(_json.Slice(_index, Math.Min(_json.Length - _index, characterCount)))}\"", _line, _column);
    }
}
