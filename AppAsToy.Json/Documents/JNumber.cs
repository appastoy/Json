namespace AppAsToy.Json.Documents;

public struct JNumber : IJElement
{
    private JDocument _document;
    private int _index;

    public JType Type => JType.Number;
    public ref double Value
    {
        get => ref _document._numberPool[_index];
    }

    internal JNumber(JDocument document, int index)
    {
        _document = document;
        _index = index;
    }
}