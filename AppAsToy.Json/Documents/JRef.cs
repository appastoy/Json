using System;

namespace AppAsToy.Json.Documents;

internal readonly struct JRef : IJElement, IEquatable<JRef>
{
    private readonly uint _refValue;
    public JType Type => (JType)((_refValue & 0x_F000_0000U) >> 28);
    public int Index => (int)(_refValue & 0x_0FFF_FFFFU);
    internal JRef(uint refValue) => _refValue = refValue;

    public bool Equals(JRef other) => _refValue.Equals(other._refValue);
}
