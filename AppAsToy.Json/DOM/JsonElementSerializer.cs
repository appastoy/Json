using System.Text;

namespace AppAsToy.Json.DOM
{
    internal struct JsonElementSerializer
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

        private bool _writeIndented;
        private StringBuilder _builder;

        public JsonElementSerializer(bool writeIndented)
        {
            _writeIndented = writeIndented;
            _builder = new StringBuilder(4096);
        }

        public string Serialize(JsonElement element)
        {
            _builder.Clear();

            if (element is null)
                return "null";

            if (element is JsonArray array)
                SerializeArray(array, 0);
            else if (element is JsonObject @object)
                SerializeObject(@object, 0);
            else
                return element.ToString();

            return _builder.ToString();
        }

        private void SerializeElement(JsonElement? element, int depth)
        {
            if (element is null)
                _builder.Append("null");
            else if (element is JsonArray array)
                SerializeArray(array, depth);
            else if (element is JsonObject @object)
                SerializeObject(@object, depth);
            else
                _builder.Append(element.ToString());
        }

        private void SerializeArray(JsonArray array, int depth)
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

        private void SerializeObject(JsonObject @object, int depth)
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

        private void SerializeProperty(JsonProperty property, int depth)
        {
            _builder.Append(JsonString.ConvertToJson(property.Key));
            AppendColon();
            SerializeElement(property.Value, depth);
        }

        private void AppendLine(int depth)
        {
            if (!_writeIndented)
                return;

            _builder.AppendLine();
            AppendIndent(depth);
        }

        private void AppendComma(int depth)
        {
            if (!_writeIndented)
            {
                _builder.Append(',');
                return;
            }

            _builder.AppendLine(",");
            AppendIndent(depth);
        }

        private void AppendColon()
        {
            if (!_writeIndented)
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
