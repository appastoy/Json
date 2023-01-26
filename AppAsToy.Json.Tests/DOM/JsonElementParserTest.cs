
using AppAsToy.Json.DOM;

namespace AppAsToy.Json.Tests.DOM;
public sealed class JsonElementParserTest
{
    [Fact]
    public void InvalidParse_Null()
    {
        new Action(() => JsonElement.Parse(null)).Should().Throw<ArgumentNullException>();
        JsonElement.TryParse(null, out _).Should().BeFalse();
    }

    [Fact]
    public void InvalidParse_Empty()
    {
        new Action(() => JsonElement.Parse("")).Should().Throw<ArgumentException>();
        JsonElement.TryParse("", out _).Should().BeFalse();
    }

    [Fact]
    public void InvalidParse_Character()
    {
        new Action(() => JsonElement.Parse("!")).Should().Throw<JsonElementParsingException>();
        new Action(() => JsonElement.Parse("a")).Should().Throw<JsonElementParsingException>();
        new Action(() => JsonElement.Parse("}")).Should().Throw<JsonElementParsingException>();
        new Action(() => JsonElement.Parse("]")).Should().Throw<JsonElementParsingException>();
    }

    [Fact]
    public void ParseValue_Null()
    {
        ((object)JsonElement.Parse("null")).Should().Be(JsonElement.Null);

        new Action(() => JsonElement.Parse("nulll")).Should().Throw<JsonElementParsingException>().And.Column.Should().Be(1);
        JsonElement.TryParse("NULL", out _).Should().BeFalse();
        JsonElement.TryParse("nul", out _).Should().BeFalse();
    }

    [Fact]
    public void ParseValue_Bool()
    {
        ((object)JsonElement.Parse("true")).Should().Be(JsonBool.True);
        ((object)JsonElement.Parse("false")).Should().Be(JsonBool.False);
        
        JsonElement.TryParse("TRUE", out _).Should().BeFalse();
        JsonElement.TryParse("FALSE", out _).Should().BeFalse();
        JsonElement.TryParse("tr", out _).Should().BeFalse();
        new Action(() => JsonElement.Parse("falses")).Should().Throw<JsonElementParsingException>().And.Column.Should().Be(1);
        new Action(() => JsonElement.Parse("truee")).Should().Throw<JsonElementParsingException>().And.Column.Should().Be(1);
    }

    [Fact]
    public void ParseValue_String()
    {
        ((object)JsonElement.Parse("\"\"")).Should().Be(JsonString.Empty);
        ((object)JsonElement.Parse("\"abc\"")).Should().Be("abc");
        JsonElement.Parse("\"abc\"").ToStringValue.Should().Be("abc");
        JsonElement.Parse("\"\\/\\\\\\\"\\b\\f\\r\\n\\t\"").ToStringValue.Should().Be("/\\\"\b\f\r\n\t");
        JsonElement.Parse("\"\\uFFFF\"").ToStringValue.Should().Be("\uFFFF");

        new Action(() => JsonElement.Parse("\"\\A")).Should().Throw<JsonElementParsingException>();
        new Action(() => JsonElement.Parse("\"\\uFF\"")).Should().Throw<JsonElementParsingException>();
    }

    [Fact]
    public void ParseValue_String_DateTime()
    {
        ((object)JsonElement.Parse("\"2000-01-01 23:59:59\"")).Should().Be(new DateTime(2000, 1, 1, 23, 59, 59));
        JsonElement.Parse("\"2000-01-01 23:59:59\"").ToDateTime.Should().Be(new DateTime(2000, 1, 1, 23, 59, 59));
    }

    [Fact]
    public void ParseValue_String_DateTimeOffset()
    {
        ((object)JsonElement.Parse("\"2000-01-01 23:59:59 +09:00\"")).Should().Be(new DateTimeOffset(2000, 1, 1, 23, 59, 59, TimeSpan.FromHours(9)));
        JsonElement.Parse("\"2000-01-01 23:59:59 +09:00\"").ToDateTimeOffset.Should().Be(new DateTimeOffset(2000, 1, 1, 23, 59, 59, TimeSpan.FromHours(9)));
    }

    [Fact]
    public void ParseValue_String_TimeSpan()
    {
        ((object)JsonElement.Parse("\"123.23:59:58\"")).Should().Be(new TimeSpan(123,23,59,58));
        JsonElement.Parse("\"123.23:59:58\"").ToTimeSpan.Should().Be(new TimeSpan(123, 23, 59, 58));
    }

    [Fact]
    public void ParseValue_String_Guid()
    {
        var guid = Guid.NewGuid();
        ((object)JsonElement.Parse($"\"{guid.ToString()}\"")).Should().Be(guid);
        JsonElement.Parse($"\"{guid.ToString()}\"").ToGuid.Should().Be(guid);
    }

    [Fact]
    public void ParseValue_String_ByteArray()
    {
        var byteArray = new byte[] { 1, 2, 3, 4 };
        ((object)JsonElement.Parse($"\"{Convert.ToBase64String(byteArray)}\"")).Should().Be(byteArray);
        JsonElement.Parse($"\"{Convert.ToBase64String(byteArray)}\"").ToByteArray.Should().BeEquivalentTo(byteArray);
    }

    [Fact]
    public void ParseValue_Number()
    {
        ((object)JsonElement.Parse("0")).Should().Be(JsonNumber.Zero);
        ((object)JsonElement.Parse("1")).Should().Be(1);
        ((object)JsonElement.Parse("0.1")).Should().Be(0.1);
        ((object)JsonElement.Parse("10")).Should().Be(10);
        ((object)JsonElement.Parse("-1")).Should().Be(-1);
        ((object)JsonElement.Parse("-0.1")).Should().Be(-0.1);
        ((object)JsonElement.Parse("-10")).Should().Be(-10);
        ((object)JsonElement.Parse("1e1")).Should().Be(1e1);
        ((object)JsonElement.Parse("-1e1")).Should().Be(-1e1);
        ((object)JsonElement.Parse("0.1e-4")).Should().Be(0.1e-4);
        ((object)JsonElement.Parse("-0.1e-4")).Should().Be(-0.1e-4);
        ((object)JsonElement.Parse("10e+11")).Should().Be(10e+11);
        ((object)JsonElement.Parse("-10e+11")).Should().Be(-10e+11);

        new Action(() => JsonElement.Parse("-")).Should().Throw<JsonElementParsingException>();
        new Action(() => JsonElement.Parse("0.1e+")).Should().Throw<JsonElementParsingException>();
        new Action(() => JsonElement.Parse("1e")).Should().Throw<JsonElementParsingException>();
        new Action(() => JsonElement.Parse("1e-")).Should().Throw<JsonElementParsingException>();
    }

    [Fact]
    public void ParseArray()
    {
        ((object)JsonElement.Parse("[]")).Should().Be(JsonArray.Empty);
        ((object)JsonElement.Parse("[ ]")).Should().Be(JsonArray.Empty);
        ((object)JsonElement.Parse("[\t]")).Should().Be(JsonArray.Empty);
        ((object)JsonElement.Parse(@"[
]")).Should().Be(JsonArray.Empty);

        var array = new JsonArray
        (
            1,
            "a",
            true,
            null,
            new JsonObject
            (
                new("a", 2),
                new("b", "b"),
                new("c", false)
            ),
            new JsonArray
            (
                3,
                "c",
                true
            )
        );
        ((object)JsonElement.Parse("[1,\"a\",true,null,{\"a\":2,\"b\":\"b\",\"c\":false},[3,\"c\",true]]")).Should().Be(array);
        ((object)JsonElement.Parse(
@"[
  1,
  ""a"",
  true,
  null,
  {
    ""a"": 2,
    ""b"": ""b"",
    ""c"": false
  },
  [
    3,
    ""c"",
    true
  ]
]")).Should().Be(array);

        new Action(() => JsonElement.Parse("[1,\"a\",true,null")).Should().Throw<JsonElementParsingException>();
        new Action(() => JsonElement.Parse("[1,\"a\",true,null,]")).Should().NotThrow();
    }

    [Fact]
    public void ParseObject()
    {
        ((object)JsonElement.Parse("{}")).Should().Be(JsonObject.Empty);
        ((object)JsonElement.Parse("{ }")).Should().Be(JsonObject.Empty);
        ((object)JsonElement.Parse("{\t}")).Should().Be(JsonObject.Empty);
        ((object)JsonElement.Parse(@"{
}")).Should().Be(JsonObject.Empty);

        var @object = new JsonObject
        (
            new("a", 1),
            new("b", "a"),
            new("c", true),
            new("d", null),
            new("e", new JsonArray
            (
                2, "b", false
            )),
            new("f", new JsonObject
            (
                new("_a", 3),
                new("_b", "c"),
                new("_c", true)
            ))
        );
        ((object)JsonElement.Parse("{\"a\":1,\"b\":\"a\",\"c\":true,\"d\":null,\"e\":[2,\"b\",false],\"f\":{\"_a\":3,\"_b\":\"c\",\"_c\":true}}")).Should().Be(@object);
        ((object)JsonElement.Parse(
@"{
  ""a"": 1,
  ""b"": ""a"",
  ""c"": true,
  ""d"": null,
  ""e"": [
    2,
    ""b"",
    false
  ],
  ""f"": {
    ""_a"": 3,
    ""_b"": ""c"",
    ""_c"": true
  }
}")).Should().Be(@object);

        new Action(() => JsonElement.Parse("{\"a\":1,\"b\":\"a\",\"c\":true,\"d\":null")).Should().Throw<JsonElementParsingException>();
        new Action(() => JsonElement.Parse("{\"a\":1,\"b\":\"a\",\"c\":true,\"d\":null,}")).Should().NotThrow();
    }
}
