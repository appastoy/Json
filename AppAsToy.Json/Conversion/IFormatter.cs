using System.Text;

namespace AppAsToy.Json.Conversion;
internal interface IFormatter<T>
{
    void Write(StringBuilder builder, T value);
    void Read(ref JReader reader, out T value);
}
