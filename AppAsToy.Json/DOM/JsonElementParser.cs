using System;
using System.Globalization;
using System.Text;

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
        switch (GetNextToken())
        {
            case 'n': return ParseNull();
            case 'f': return ParseFalse();
            case 't': return ParseTrue();
            case '"': return ParseString();
            case '[': return ParseArray();
            case '{': return ParseObject();

            case '-': case '0': case '1': case '2': 
            case '3': case '4': case '5': case '6': 
            case '7': case '8': case '9':
                return ParseNumber();

            default: throw InvalidToken();
        }
    }

    private char GetNextToken()
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
        if (RemainLength >= 4 &&
            _json[_index + 1] == 'u' &&
            _json[_index + 2] == 'l' &&
            _json[_index + 3] == 'l')
        {
            StepForward(4);
            return JsonElement.Null;
        }
        throw InvalidToken();
    }

    

    private IJsonElement ParseTrue()
    {
        if (RemainLength >= 4 &&
            _json[_index + 1] == 'r' &&
            _json[_index + 2] == 'u' &&
            _json[_index + 3] == 'e')
        {
            StepForward(4);
            return JsonBool.True;
        }
        throw InvalidToken();
    }

    private IJsonElement ParseFalse()
    {
        if (RemainLength >= 5 &&
            _json[_index + 1] == 'a' &&
            _json[_index + 2] == 'l' &&
            _json[_index + 3] == 's' &&
            _json[_index + 4] == 'e')
        {
            StepForward(5);
            return JsonBool.False;
        }
        throw InvalidToken();
    }

    private IJsonElement ParseString()
    {
        if (RemainLength < 2)
            throw InvalidToken();

        var builder = new StringBuilder(4096);
        int currentIndex = _index + 1;
        while (currentIndex < _json.Length)
        {
            var ch = _json[currentIndex++];
            if (ch == '\\')
            {
                builder.Append(ParseEscapedString(ref currentIndex));
                continue;
            }
            if (ch == '"')
                break;

            builder.Append(ch);
        }
        if (currentIndex >= _json.Length)
            throw UnexpectedEndReached();

        StepForward(currentIndex - _index);

        if (builder.Length == 0)
            return JsonString.Empty;

        return new JsonString(builder.ToString());
    }

    private char ParseEscapedString(ref int currentIndex)
    {
        char ch;
        if (currentIndex >= _json.Length)
            throw UnexpectedEndReached();

        ch = _json[currentIndex++];
        if (ch == 'u')
        {
            if ((_json.Length - currentIndex) < 4)
                throw InvalidToken();

            if (!char.TryParse(new string(_json.Slice(currentIndex, 4)), out var unicodeChar))
                throw InvalidToken();

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
            _ => throw InvalidToken()
        };
    }

    private IJsonElement ParseNumber()
    {
        var length = GetNumberEndIndex() - _index;
        if (!double.TryParse(
            _json.Slice(_index, length), 
            NumberStyles.Any, 
            CultureInfo.InvariantCulture,  
            out var value))
            throw InvalidToken();

        StepForward(length);

        if (value == 0d)
            return JsonNumber.Zero;

        return new JsonNumber(value);
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
            throw InvalidToken();

        // TODO: Parse Array
        return null;
    }

    private IJsonElement ParseObject()
    {
        if (RemainLength < 2)
            throw InvalidToken();

        // TODO: Parse Object
        return null;
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
        return new JsonElementParsingException("Parsing has not yet completed, but the end of the string has been reached.");
    }

    private JsonElementParsingException InvalidToken()
    {
        return new JsonElementParsingException("invalid token", _line, _column);
    }
}
