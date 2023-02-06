using System.Text;

namespace AppAsToy.Json.Conversion;

public interface IFormatter { }

public interface IFormatter<T> : IFormatter
{
    void Write(ref JWriter writer, T value);
    void Read(ref JReader reader, out T value);
}
