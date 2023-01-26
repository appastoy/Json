using System.Text;

namespace AppAsToy.Json
{
    internal ref struct JElementSerializer
    {
        private static readonly string[] _indents =
        {
            "", // 0
            "  ", // 1
            "    ", // 2
            "      ", // 3
            "        ", // 4
            "          ", // 5
            "            ", // 6
            "              ", // 7
            "                ", // 8
            "                  ", // 9
            "                    ", // 10
            "                      ", // 11
            "                        ", // 12
            "                          ", // 13
            "                            ", // 14
            "                              ", // 15
        };

        private readonly bool _writeIndent;
        private readonly string? _numberFormat;
        private readonly bool _escapeUnicode;
        private readonly StringBuilder _builder;

        public JElementSerializer(bool writeIndent, string? numberFormat, bool escapeUnicode)
        {
            _writeIndent = writeIndent;
            _numberFormat = numberFormat;
            _escapeUnicode = escapeUnicode;
            _builder = new StringBuilder(4096);
        }

        public string Serialize(JElement element)
        {
            _builder.Clear();

            if (element is null)
                return "null";

            SerializeElement(element, 0);

            return _builder.ToString();
        }

        private void SerializeElement(JElement? element, int depth)
        {
            if (element is null)
                _builder.Append("null");
            else if (element is JArray array)
                SerializeArray(array, depth);
            else if (element is JObject @object)
                SerializeObject(@object, depth);
            else
                _builder.Append(element.Serialize(_writeIndent, _numberFormat, _escapeUnicode));
        }

        private void SerializeArray(JArray array, int depth)
        {
            _builder.Append('[');

            if (array.Count > 0)
            {
                AppendLine(depth + 1);
                SerializeElement(array[0], depth + 1);
                for (int i = 1; i < array.Count; i++)
                {
                    AppendComma(depth + 1);
                    SerializeElement(array[i], depth + 1);
                }
                AppendLine(depth);
            }

            _builder.Append(']');
        }

        private void SerializeObject(JObject @object, int depth)
        {
            _builder.Append('{');

            if (@object.Count > 0)
            {
                var properties = @object._properties;
                AppendLine(depth + 1);
                SerializeProperty(properties[0], depth + 1);
                for (int i = 1; i < properties.Count; i++)
                {
                    AppendComma(depth + 1);
                    SerializeProperty(properties[i], depth + 1);
                }
                AppendLine(depth);
            }

            _builder.Append('}');
        }

        private void SerializeProperty(JProperty property, int depth)
        {
            _builder.Append(JString.ConvertToJson(property.PropertyName, _escapeUnicode));
            AppendColon();
            SerializeElement(property.PropertyValue, depth);
        }

        private void AppendLine(int depth)
        {
            if (!_writeIndent)
                return;

            _builder.AppendLine();
            AppendIndent(depth);
        }

        private void AppendComma(int depth)
        {
            if (!_writeIndent)
            {
                _builder.Append(',');
                return;
            }

            _builder.AppendLine(",");
            AppendIndent(depth);
        }

        private void AppendColon()
        {
            if (!_writeIndent)
                _builder.Append(':');
            else
                _builder.Append(": ");
        }

        private void AppendIndent(int depth)
        {
            if (depth < _indents.Length)
            {
                _builder.Append(_indents[depth]);
            }
            else
            {
                _builder.Append(_indents[^1]);
                var remainIndents = depth - _indents.Length + 1;
                for (int i = 0; i < remainIndents; i++)
                    _builder.Append("  ");
            }
        }
    }
}
