namespace AppAsToy.Json.Documents;

internal struct JPropertyData
{
    public readonly string Key;
    public JRef Value;

    public JType Type => JType.ObjectProperty;

    internal JPropertyData(string key, JRef value)
    {
        Key = key;
        Value = value;
    }
}