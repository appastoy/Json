using System;

namespace AppAsToy.Json;
public sealed class JElementParseException : Exception
{
    public int Line { get; }
    public int Column { get; }

    public JElementParseException(string message, int line, int column) 
        : base($"{message} (line:{(line+1).ToString()}, column:{(column+1).ToString()})") 
    {
        Line = line + 1;
        Column = column + 1;
    }
}
