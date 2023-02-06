namespace AppAsToy.Json.Conversion;

public interface IFormatterResolver
{
    IFormatter<T>? Resolve<T>();
}
