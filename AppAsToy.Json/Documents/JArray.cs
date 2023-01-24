namespace AppAsToy.Json.Documents;

public struct JArray : IJElement
{
    private readonly JDocument _document;
    private readonly int _index;

    public JType Type => JType.Array;

    internal JArray(JDocument document, int index)
    {
        _document = document;
        _index = index;
    }
}
