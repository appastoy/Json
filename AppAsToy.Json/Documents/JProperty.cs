namespace AppAsToy.Json.Documents;

public struct JProperty : IJElement
{
    public readonly string Key;
    public JElement Value;

    public JType Type => JType.ObjectProperty;

    internal JProperty(ref JPropertyData data, JDocument document)
    {
        Key = data.Key;
        Value = new JElement(document, data.Value);
    }
}
