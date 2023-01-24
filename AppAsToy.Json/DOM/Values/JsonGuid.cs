using System;

namespace AppAsToy.Json.DOM
{
    public sealed class JsonGuid : JsonValue<Guid>
    {
        public static JsonGuid New => new(Guid.NewGuid());

        public override JsonElementType Type => JsonElementType.Guid;
        public override Guid? asGuid => RawValue;
        public override Guid toGuid => RawValue;

        public JsonGuid(Guid guid) : base(guid) { }
        public override string ToString(bool _) => $"\"{RawValue.ToString()}\"";
        public override bool Equals(Guid other) => other == RawValue;
    }
}
