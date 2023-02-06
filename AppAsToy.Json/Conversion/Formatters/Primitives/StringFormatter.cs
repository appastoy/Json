namespace AppAsToy.Json.Conversion.Formatters.Primitives;

#pragma warning disable CS8767

internal sealed class StringFormatter : IFormatter<string>
{
    public void Read(ref JReader reader, out string? value)
    {
        value = reader.ReadStringOrNull();
    }

    public void Write(ref JWriter writer, string? value)
    {
        if (value == null)
            writer.WriteNull();
        else
            writer.Write(value);
    }
}
