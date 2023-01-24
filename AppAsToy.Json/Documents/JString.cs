namespace AppAsToy.Json.Documents;

public struct JString : IJElement
{
    private JDocument _document;
    private int _index;

    public JType Type => JType.String;
    public ref string Value
    {
        get => ref _document._stringPool[_index];
    }

    internal JString(JDocument document, int index)
    {
        _document = document;
        _index = index;
    }
}
