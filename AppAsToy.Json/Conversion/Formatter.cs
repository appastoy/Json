namespace AppAsToy.Json.Conversion;
public static class Formatter<T>
{
    public static readonly IFormatter<T> Shared = FormatterResolver.Resolve<T>();
}
