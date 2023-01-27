namespace AppAsToy.Json;

public sealed class JConvertOption
{
    public static JConvertOption Default { get; } = new();

    public bool IncludeInterfaceAndAbstractClass { get; set; } = false;
    public bool IncludeRootTypeInfo { get; set; } = false;
    public ConvertMembers ConvertMembers { get; set; } = ConvertMembers.PublicField | ConvertMembers.PublicProperty;
}
