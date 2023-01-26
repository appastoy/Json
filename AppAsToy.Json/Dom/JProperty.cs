using System;

namespace AppAsToy.Json
{
    public sealed class JProperty : JElement
    {
        private JElement _value;

        public override JElementType Type => JElementType.Property;

        public override string PropertyName { get; }

        public override JElement PropertyValue
        {
            get => _value;
            set => _value = value ?? Null;
        }

        public JProperty(string name, JElement? value)
        {
            PropertyName = name ?? throw new ArgumentNullException(nameof(name));
            _value = value ?? Null;
        }

        public override string Serialize(bool writeIndent = true, string? numberFormat = null, bool escapeUnicode = false)
        {
            var propertyName = JString.ConvertToJson(PropertyName, escapeUnicode);
            var propertyValue = new JElementSerializer(writeIndent, numberFormat, escapeUnicode).Serialize(PropertyValue);
            return $"{propertyName}{(writeIndent ? ": " : ":")}{propertyValue}";
        }

        protected override bool IsEqual(JElement? element) =>
            element is JProperty property &&
            PropertyName == property.PropertyName &&
            PropertyValue.Equals(property.PropertyValue);

        public override int GetHashCode()
        {
            return HashCode.Combine(PropertyName, PropertyValue);
        }
    }
}