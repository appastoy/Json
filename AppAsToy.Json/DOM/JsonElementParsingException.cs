using System;

namespace AppAsToy.Json.DOM;
public sealed class JsonElementParsingException : Exception
{
    public int Line { get; }
    public int Column { get; }

    public JsonElementParsingException(string message, int line, int column) 
        : base($"{message} (line:{(line+1).ToString()}, column:{(column+1).ToString()})") 
    {
        Line = line + 1;
        Column = column + 1;
    }
}
