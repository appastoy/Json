using System;
using System.Globalization;
using System.Text;

namespace AppAsToy.Json.Conversion;
public ref struct JReader
{
    private readonly ReadOnlySpan<char> _buffer;
    private int _position;
    private int _line;
    private int _column;

    private int RemainLength => _buffer.Length - _position;

    internal JReader(string json)
    {
        _buffer = new ReadOnlySpan<char>(json?.ToCharArray() ?? throw new ArgumentNullException(nameof(json)));
        _position = 0;
        _line = 0;
        _column = 0;
    }
    
    public bool? ReadBoolOrNull()
    {
        var ch = MoveNextToken();
        return ch switch
        {
            'f' => TryReadKeyword("false") ? false : throw InvalidToken($"Invalid bool string."),
            't' => TryReadKeyword("true") ? true : throw InvalidToken($"Invalid bool string."),
            'n' => TryReadKeyword("null") ? null : throw InvalidToken($"Invalid null string."),
            _ => throw InvalidToken($"Can't read bool. ('{ch}'({((int)ch).ToString()}))"),
        };
    }

    public bool ReadBool()
    {
        return ReadBoolOrNull() ?? throw InvalidToken("Bool value should not be null.");
    }

    public double? ReadNumberOrNull()
    {
        var ch = MoveNextToken();
        return ch switch
        {
            'n' => TryReadKeyword("null") ? null : throw InvalidToken($"Invalid null string."),
            '-' or '0' or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9' => ReadNumberCore(),
            _ => throw InvalidToken($"Can't read number. ('{ch}'({((int)ch).ToString()}))"),
        };
    }

    public double ReadNumber()
    {
        return ReadNumberOrNull() ?? throw InvalidToken("Number value should not be null.");
    }

    public string? ReadStringOrNull()
    {
        var ch = MoveNextToken();
        return ch switch
        {
            'n' => TryReadKeyword("null") ? null : throw InvalidToken($"Invalid null string."),
            '"' => ReadStringCore(),
            _ => throw InvalidToken($"Can't read string. ('{ch}'({((int)ch).ToString()}))"),
        };
    }

    public string ReadString()
    {
        return ReadStringOrNull() ?? throw InvalidToken("String value should not be null.");
    }

    public bool ReadArray()
    {
        var ch = MoveNextToken();
        return ch switch
        {
            '[' => true,
            'n' => TryReadKeyword("null") ? false : throw InvalidToken("Invalid null string."),
            _ => throw InvalidToken($"Can't read array. ('{ch}'({((int)ch).ToString()}))")
        };
    }
    public bool CanReadNextArrayItem()
    {
        return CanReadNextCollectionItem(']');
    }
    public bool ReadObject()
    {
        var ch = MoveNextToken();
        return ch switch
        {
            '{' => true,
            'n' => TryReadKeyword("null") ? false : throw InvalidToken("Invalid null string."),
            _ => throw InvalidToken($"Can't read object. ('{ch}'({((int)ch).ToString()}))")
        };
    }
    public bool CanReadNextObjectItem()
    {
        return CanReadNextCollectionItem('}');
    }
    public string ReadPropertyName()
    {
        var propertyName = ReadStringOrNull();
        if (propertyName == null)
            throw InvalidToken("Property name should not be null.");

        var ch = MoveNextToken();
        if (ch != ':')
            throw InvalidToken("Property name should have a colon(:).");

        _position += 1;
        return propertyName;
    }


    private double ReadNumberCore()
    {
        const NumberStyles jsonNumberStyles = NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign;
        var length = GetNumberEndIndex() - _position;
        var numberString = _buffer.Slice(_position, length);

        if (!double.TryParse(numberString, jsonNumberStyles, CultureInfo.InvariantCulture, out var value))
            throw InvalidToken($"Invalid number format. (\"{new string(numberString)}\")");

        StepForward(length);

        return value;
    }

    private int GetNumberEndIndex()
    {
        var curPos = _position;
        while (curPos < _buffer.Length)
        {
            switch (_buffer[curPos])
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '-':
                case '+':
                case '.':
                case 'e':
                case 'E':
                    curPos += 1;
                    continue;

                default:
                    return curPos;
            }
        }

        return curPos;
    }

    private string ReadStringCore()
    {
        if (RemainLength < 2)
            throw InvalidToken("String value is terminated abnormally.");

        var builder = new StringBuilder(4096);
        int curPos = _position + 1;
        while (curPos < _buffer.Length)
        {
            var ch = _buffer[curPos];
            if (ch == '\\')
            {
                ch = ReadEscapedCharacter(ref curPos);
                builder.Append(ch);
                continue;
            }
            if (ch == '"')
                break;

            builder.Append(ch);
            curPos++;
        }
        if (curPos >= _buffer.Length)
            throw UnexpectedEndReached();

        StepForward(curPos - _position + 1);

        if (builder.Length == 0)
            return string.Empty;

        return builder.ToString();
    }

    private char ReadEscapedCharacter(ref int curPos)
    {
        curPos++;
        if (curPos >= _buffer.Length)
            throw UnexpectedEndReached();

        var ch = _buffer[curPos++];
        if (ch == 'u')
        {
            if ((_buffer.Length - curPos) < 4)
                throw InvalidToken("Unicode value is terminated abnormally.");

            if (!ushort.TryParse(_buffer.Slice(curPos, 4), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out var result))
                throw InvalidToken("Unicode value is not valid.");

            curPos += 4;
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

    private bool CanReadNextCollectionItem(char endCharacter)
    {
        var ch = MoveNextToken();
        if (ch == ',')
        {
            _position += 1;
            ch = MoveNextToken();
        }
        if (ch == endCharacter)
        {
            _position += 1;
            return false;
        }

        return true;
    }

    private bool TryReadKeyword(ReadOnlySpan<char> keyword)
    {
        if (RemainLength < keyword.Length)
            throw InvalidToken($"Can't read keyword. (\"{keyword.ToString()}\")");

        if (!keyword.SequenceEqual(_buffer.Slice(_position, keyword.Length)))
            return false;

        _position += keyword.Length;
        return true;
    }

    private char MoveNextToken()
    {
        SkipWhiteSpace();
        if (_position >= _buffer.Length)
            throw UnexpectedEndReached();

        return _buffer[_position];
    }

    private void SkipWhiteSpace()
    {
        while (_position < _buffer.Length)
        {
            switch (_buffer[_position])
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

    private void StepForward(int count)
    {
        _position += count;
        _column += count;
    }

    private void StepNewLine()
    {
        _position += 1;
        _line += 1;
        _column = 0;
    }

    private JReaderException UnexpectedEndReached()
    {
        return new JReaderException("Parsing has not yet completed, but the end of the string has been reached.", _line, _column);
    }

    private JReaderException InvalidToken(string message)
    {
        return new JReaderException($"Invalid token - {message}", _line, _column);
    }
}

public sealed class JReaderException : Exception
{
    public int Line { get; }
    public int Column { get; }

    public JReaderException(string message, int line, int column)
        : base($"{message} (line:{line}, column:{column})")
    {
        Line = line;
        Column = column;
    }
}
