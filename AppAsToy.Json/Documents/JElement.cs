namespace AppAsToy.Json.Documents;

public readonly struct JElement : IJElement
{
    private readonly JDocument _document;
    internal readonly JRef _ref;

    public JType Type => _ref.Type;

    internal JElement(JDocument document, JRef @ref)
    {
        _document = document;
        _ref = @ref;
    }
}
