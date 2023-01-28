using System.Text;

namespace AppAsToy.Json.Conversion;
public interface IFormatter<T>
{
    void Write(ref JWriter writer, T value);
    void Read(ref JReader reader, out T value);
}
