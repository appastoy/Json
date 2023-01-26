namespace AppAsToy.Json;
public interface IReadOnlyJProperty
{
    string PropertyName { get; }
    IReadOnlyJElement PropertyValue { get; }
}
