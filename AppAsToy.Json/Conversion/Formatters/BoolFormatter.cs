namespace AppAsToy.Json.Conversion.Formatters;

internal sealed class BoolFormatter : IFormatter<bool>
{
    public void Read(ref JReader reader, out bool value)
    {
        value = reader.ReadBool();
    }

    public void Write(ref JWriter writer, bool value)
    {
        writer.Write(value);
    }
}