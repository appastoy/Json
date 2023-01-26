namespace AppAsToy.Json
{
    internal sealed class JNull : JElement
    {
        internal static JNull Shared { get; } = new JNull();

        public override JElementType Type => JElementType.Null;

        private JNull() { }

        public override string Serialize(bool _, string? __, bool ___) => "null";

        protected override bool IsEqual(JElement? element) => element is null || element is JNull;
        public override bool Equals(string other) => other == null;
        public override bool Equals(byte[] other) => other == null;
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
