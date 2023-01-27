using System;
using System.Globalization;

namespace AppAsToy.Json.Conversion;
public ref struct JWriter
{
    private static readonly string[] _indents =
    {
        "", // 0
        "  ", // 1
        "    ", // 2
        "      ", // 3
        "        ", // 4
        "          ", // 5
        "            ", // 6
        "              ", // 7
        "                ", // 8
        "                  ", // 9
        "                    ", // 10
        "                      ", // 11
        "                        ", // 12
        "                          ", // 13
        "                            ", // 14
        "                              ", // 15
    };

    private char[] _bufferRaw;
    private Span<char> _buffer;
    private int _position;
    private int _depth;
    private bool _writeIndent;
    private bool _escapeUnicode;

    internal JWriter(int capacity, bool writeIndent, bool escapeUnicode)
    {
        _bufferRaw = new char[capacity];
        _buffer = _bufferRaw;
        _position = 0;
        _depth = 0;
        _writeIndent = writeIndent;
        _escapeUnicode = escapeUnicode;
    }

    public void WriteNull() => WriteCore("null");
    public void Write(bool value) => WriteCore(value ? "true" : "false");
    public void Write(string value) => WriteCore(JString.ConvertToJson(value, _escapeUnicode));
    public void Write(double value, string? format = null, IFormatProvider? formatProvider = null) 
        => WriteCore(format != null 
            ? value.ToString(format, formatProvider ?? CultureInfo.InvariantCulture) 
            : value.ToString(formatProvider ?? CultureInfo.InvariantCulture));
    public void WriteEmptyArray() => WriteCore("[]");
    public void WriteArrayStart() => WriteStart('[');
    public void WriteArrayEnd() => WriteEnd(']');
    public void WriteEmptyObject() => WriteCore("{}");
    public void WriteOjectStart() => WriteStart('{');
    public void WriteObjectEnd() => WriteEnd('}');
    public void WritePropertyName(string name)
    {
        Write(name);
        if (_writeIndent)
            WriteCore(": ");
        else
            WriteChar(':');
    }
    public void WriteComma()
    {
        WriteChar(',');
        if (_writeIndent)
        {
            WriteLine();
            WriteIndent();
        }
    }

    private void WriteStart(char character)
    {
        WriteChar(character);
        if (_writeIndent)
        {
            WriteLine();
            _depth += 1;
            WriteIndent();
        }
    }
    private void WriteEnd(char character)
    {
        if (_writeIndent)
        {
            WriteLine();
            _depth -= 1;
            WriteIndent();
        }
        WriteChar(character);
    }
    private void WriteLine()
    {
        WriteCore(Environment.NewLine);
    }
    private void WriteIndent()
    {
        if (_depth < _indents.Length)
        {
            WriteCore(_indents[_depth]);
        }
        else
        {
            WriteCore(_indents[^1]);
            var remainIndents = _depth - _indents.Length + 1;
            for (int i = 0; i < remainIndents; i++)
                WriteCore("  ");
        }
    }
    private void WriteChar(char value)
    {
        EnsureLength(1);
        _buffer[_position++] = value;
    }
    private void WriteCore(ReadOnlySpan<char> value)
    {
        EnsureLength(value.Length);

        value.CopyTo(_buffer.Slice(_position, value.Length));
        _position += value.Length;
    }
    private void EnsureLength(int length)
    {
        var needLength = _position + length;
        if (needLength <= _buffer.Length)
            return;

        var newSize = needLength + (needLength >> 1);
        Array.Resize(ref _bufferRaw, newSize);
        _buffer = _bufferRaw;
    }
    public override string ToString() 
        => _buffer[.._position].ToString();
}
