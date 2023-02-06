namespace AppAsToy.Json.Conversion.Formatters;
public abstract class SharedFormatter<T, TFormatter> : IFormatter<T>
    where TFormatter : class, IFormatter<T>, new()
{
    public static readonly IFormatter<T> Shared = new TFormatter();
    public abstract void Read(ref JReader reader, out T value);
    public abstract void Write(ref JWriter writer, T value);
}
