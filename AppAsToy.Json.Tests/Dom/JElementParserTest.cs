namespace AppAsToy.Json.Tests;
public sealed class JElementParserTest
{
    [Fact]
    public void InvalidParse_Null()
    {
        new Action(() => JElement.Parse(null)).Should().Throw<ArgumentNullException>();
        new Action(() => JElement.TryParse(null, out _)).Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void InvalidParse_Empty()
    {
        new Action(() => JElement.Parse("")).Should().Throw<ArgumentException>();
        JElement.TryParse("", out _).Should().BeFalse();
    }

    [Fact]
    public void InvalidParse_Character()
    {
        new Action(() => JElement.Parse("!")).Should().Throw<JElementParseException>();
        new Action(() => JElement.Parse("a")).Should().Throw<JElementParseException>();
        new Action(() => JElement.Parse("}")).Should().Throw<JElementParseException>();
        new Action(() => JElement.Parse("]")).Should().Throw<JElementParseException>();
    }

    [Fact]
    public void ParseValue_Null()
    {
        ((object)JElement.Parse("null")).Should().Be(JElement.Null);

        new Action(() => JElement.Parse("nulll")).Should().Throw<JElementParseException>().And.Column.Should().Be(1);
        JElement.TryParse("NULL", out _).Should().BeFalse();
        JElement.TryParse("nul", out _).Should().BeFalse();
    }

    [Fact]
    public void ParseValue_Bool()
    {
        ((object)JElement.Parse("true")).Should().Be(JBool.True);
        ((object)JElement.Parse("false")).Should().Be(JBool.False);
        
        JElement.TryParse("TRUE", out _).Should().BeFalse();
        JElement.TryParse("FALSE", out _).Should().BeFalse();
        JElement.TryParse("tr", out _).Should().BeFalse();
        new Action(() => JElement.Parse("falses")).Should().Throw<JElementParseException>().And.Column.Should().Be(1);
        new Action(() => JElement.Parse("truee")).Should().Throw<JElementParseException>().And.Column.Should().Be(1);
    }

    [Fact]
    public void ParseValue_String()
    {
        ((object)JElement.Parse("\"\"")).Should().Be(JString.Empty);
        ((object)JElement.Parse("\"abc\"")).Should().Be("abc");
        JElement.Parse("\"abc\"").ToStringValue.Should().Be("abc");
        JElement.Parse("\"\\/\\\\\\\"\\b\\f\\r\\n\\t\"").ToStringValue.Should().Be("/\\\"\b\f\r\n\t");
        JElement.Parse("\"\\uFFFF\"").ToStringValue.Should().Be("\uFFFF");

        new Action(() => JElement.Parse("\"\\A")).Should().Throw<JElementParseException>();
        new Action(() => JElement.Parse("\"\\uFF\"")).Should().Throw<JElementParseException>();
    }

    [Fact]
    public void ParseValue_String_DateTime()
    {
        ((object)JElement.Parse("\"2000-01-01 23:59:59\"")).Should().Be(new DateTime(2000, 1, 1, 23, 59, 59));
        JElement.Parse("\"2000-01-01 23:59:59\"").ToDateTime().Should().Be(new DateTime(2000, 1, 1, 23, 59, 59));
    }

    [Fact]
    public void ParseValue_String_DateTimeOffset()
    {
        ((object)JElement.Parse("\"2000-01-01 23:59:59 +09:00\"")).Should().Be(new DateTimeOffset(2000, 1, 1, 23, 59, 59, TimeSpan.FromHours(9)));
        JElement.Parse("\"2000-01-01 23:59:59 +09:00\"").ToDateTimeOffset().Should().Be(new DateTimeOffset(2000, 1, 1, 23, 59, 59, TimeSpan.FromHours(9)));
    }

    [Fact]
    public void ParseValue_String_TimeSpan()
    {
        ((object)JElement.Parse("\"123.23:59:58\"")).Should().Be(new TimeSpan(123,23,59,58));
        JElement.Parse("\"123.23:59:58\"").ToTimeSpan().Should().Be(new TimeSpan(123, 23, 59, 58));
    }

    [Fact]
    public void ParseValue_String_Guid()
    {
        var guid = Guid.NewGuid();
        ((object)JElement.Parse($"\"{guid.ToString("N")}\"")).Should().Be(guid);
        JElement.Parse($"\"{guid.ToString("N")}\"").ToGuid().Should().Be(guid);
    }

    [Fact]
    public void ParseValue_String_ByteArray()
    {
        var byteArray = new byte[] { 1, 2, 3, 4 };
        ((object)JElement.Parse($"\"{Convert.ToBase64String(byteArray)}\"")).Should().Be(byteArray);
        JElement.Parse($"\"{Convert.ToBase64String(byteArray)}\"").ToByteArray.Should().BeEquivalentTo(byteArray);
    }

    [Fact]
    public void ParseValue_Number()
    {
        ((object)JElement.Parse("0")).Should().Be(JNumber.Zero);
        ((object)JElement.Parse("1")).Should().Be(1);
        ((object)JElement.Parse("0.1")).Should().Be(0.1);
        ((object)JElement.Parse("10")).Should().Be(10);
        ((object)JElement.Parse("-1")).Should().Be(-1);
        ((object)JElement.Parse("-0.1")).Should().Be(-0.1);
        ((object)JElement.Parse("-10")).Should().Be(-10);
        ((object)JElement.Parse("1e1")).Should().Be(1e1);
        ((object)JElement.Parse("-1e1")).Should().Be(-1e1);
        ((object)JElement.Parse("0.1e-4")).Should().Be(0.1e-4);
        ((object)JElement.Parse("-0.1e-4")).Should().Be(-0.1e-4);
        ((object)JElement.Parse("10e+11")).Should().Be(10e+11);
        ((object)JElement.Parse("-10e+11")).Should().Be(-10e+11);

        new Action(() => JElement.Parse("-")).Should().Throw<JElementParseException>();
        new Action(() => JElement.Parse("0.1e+")).Should().Throw<JElementParseException>();
        new Action(() => JElement.Parse("1e")).Should().Throw<JElementParseException>();
        new Action(() => JElement.Parse("1e-")).Should().Throw<JElementParseException>();
    }

    [Fact]
    public void ParseArray()
    {
        ((object)JElement.Parse("[]")).Should().Be(JArray.Empty);
        ((object)JElement.Parse("[ ]")).Should().Be(JArray.Empty);
        ((object)JElement.Parse("[\t]")).Should().Be(JArray.Empty);
        ((object)JElement.Parse(@"[
]")).Should().Be(JArray.Empty);

        var array = new JArray
        (
            1,
            "a",
            true,
            null,
            new JObject
            (
                new("a", 2),
                new("b", "b"),
                new("c", false)
            ),
            new JArray
            (
                3,
                "c",
                true
            )
        );
        ((object)JElement.Parse("[1,\"a\",true,null,{\"a\":2,\"b\":\"b\",\"c\":false},[3,\"c\",true]]")).Should().Be(array);
        ((object)JElement.Parse(
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

        new Action(() => JElement.Parse("[1,\"a\",true,null")).Should().Throw<JElementParseException>();
        new Action(() => JElement.Parse("[1,\"a\",true,null,]")).Should().NotThrow();
    }

    [Fact]
    public void ParseObject()
    {
        ((object)JElement.Parse("{}")).Should().Be(JObject.Empty);
        ((object)JElement.Parse("{ }")).Should().Be(JObject.Empty);
        ((object)JElement.Parse("{\t}")).Should().Be(JObject.Empty);
        ((object)JElement.Parse(@"{
}")).Should().Be(JObject.Empty);

        var @object = new JObject
        (
            new("a", 1),
            new("b", "a"),
            new("c", true),
            new("d", null),
            new("e", new JArray
            (
                2, "b", false
            )),
            new("f", new JObject
            (
                new("_a", 3),
                new("_b", "c"),
                new("_c", true)
            ))
        );
        ((object)JElement.Parse("{\"a\":1,\"b\":\"a\",\"c\":true,\"d\":null,\"e\":[2,\"b\",false],\"f\":{\"_a\":3,\"_b\":\"c\",\"_c\":true}}")).Should().Be(@object);
        ((object)JElement.Parse(
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

        new Action(() => JElement.Parse("{\"a\":1,\"b\":\"a\",\"c\":true,\"d\":null")).Should().Throw<JElementParseException>();
        new Action(() => JElement.Parse("{\"a\":1,\"b\":\"a\",\"c\":true,\"d\":null,}")).Should().NotThrow();
    }
}
