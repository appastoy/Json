using System;

namespace AppAsToy.Json.DOM
{
    public sealed class JsonGuid : JsonElement
    {
        public static JsonGuid New => new JsonGuid(Guid.NewGuid());

        public readonly Guid RawValue;

        public override JsonElementType Type => JsonElementType.Guid;
        public override Guid? asGuid => RawValue;
        public override Guid toGuid => RawValue;

        public JsonGuid(Guid guid)
        {
            RawValue = guid;
        }

        public override string ToString(bool _) => $"\"{RawValue.ToString()}\"";

        protected override bool IsEqual(JsonElement? element)
            => element is JsonGuid guid && guid.RawValue == RawValue;

        public override bool Equals(Guid other) => other == RawValue;

        public override int GetHashCode() => RawValue.GetHashCode();
    }
}
