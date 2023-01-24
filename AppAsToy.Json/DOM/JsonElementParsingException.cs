using System;

namespace AppAsToy.Json.DOM;
public sealed class JsonElementParsingException : Exception
{
    public JsonElementParsingException(string message) : base(message) { }
    public JsonElementParsingException(string message, int line, int column) 
        : base($"{message} (line:{(line+1).ToString()}, column:{(column+1).ToString()})") { }
}
