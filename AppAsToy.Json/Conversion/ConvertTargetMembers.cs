using System;

namespace AppAsToy.Json;

[Flags]
public enum ConvertMembers
{
    PrivateField = 1 << 1,
    ProtectedField = 1 << 2,
    PublicField = 1 << 3,

    PrivateReadOnlyField = 1 << 4,
    ProtectedReadOnlyField = 1 << 5,
    PublicReadOnlyField = 1 << 6,

    PrivateConstantField = 1 << 7,
    ProtectedConstantField = 1 << 8,
    PublicConstantField = 1 << 9,

    PrivateProperty = 1 << 10,
    ProtectedProperty = 1 << 11,
    PublicProperty = 1 << 12,
}
