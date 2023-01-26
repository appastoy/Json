namespace AppAsToy.Json
{
    public sealed class JBool : JValue<bool>
    {
        public static JBool True { get; } = new(true);
        public static JBool False { get; } = new(false);

        public override JElementType Type => JElementType.Bool;

        public override bool? AsBool => RawValue;
        public override bool ToBool => RawValue;

        private JBool(bool value) : base(value) { }

        public override string Serialize(bool _, string? __, bool ___) => RawValue ? "true" : "false";

        public override bool Equals(bool other) => RawValue == other;
    }
}
