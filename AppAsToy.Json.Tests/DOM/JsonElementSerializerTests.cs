using AppAsToy.Json.DOM;

namespace AppAsToy.Json.Tests.DOM;

public class JsonElementSerializerTests
{
    [Fact]
    public void SerializeNull()
    {
        JsonElement.Null.ToString().Should().Be("null");
    }

    [Fact]
    public void SerializeBool()
    {
        JsonBool.True.ToString().Should().Be("true");
        JsonBool.False.ToString().Should().Be("false");
    }

    [Fact]
    public void SerializeString()
    {
        JsonString.Empty.ToString().Should().Be("\"\"");
        new JsonString("abc").ToString().Should().Be("\"abc\"");
        new JsonString("가나다").ToString().Should().Be("\"가나다\"");
        new JsonString("\"/\\\b\f\n\r\t").ToString().Should().Be("\"\\\"\\/\\\\\\b\\f\\n\\r\\t\"");
    }

    [Fact]
    public void SerializeNumber() 
    {
        JsonNumber.Zero.ToString().Should().Be("0");
        new JsonNumber(sbyte.MaxValue).ToString().Should().Be(sbyte.MaxValue.ToString());
        new JsonNumber(short.MaxValue).ToString().Should().Be(short.MaxValue.ToString());
        new JsonNumber(int.MaxValue).ToString().Should().Be(int.MaxValue.ToString());
        new JsonNumber(long.MaxValue >> 12).ToString().Should().Be((long.MaxValue >> 12).ToString());
        new JsonNumber(byte.MaxValue).ToString().Should().Be(byte.MaxValue.ToString());
        new JsonNumber(ushort.MaxValue).ToString().Should().Be(ushort.MaxValue.ToString());
        new JsonNumber(uint.MaxValue).ToString().Should().Be(uint.MaxValue.ToString());
        new JsonNumber(ulong.MaxValue >> 12).ToString().Should().Be((ulong.MaxValue >> 12).ToString());
        new JsonNumber(0.1f).ToString("F1").Should().Be("0.1");
        new JsonNumber(0.123d).ToString().Should().Be("0.123");
        new JsonNumber(0.1234m).ToString().Should().Be("0.1234");
    }

    [Fact]
    public void SerializeArray() 
    {
        JsonArray.Empty.ToString().Should().Be("[]");
        var array = new JsonArray(1, "a", true, null);
        array.ToString(writeIndented: false).Should().Be("[1,\"a\",true,null]");
        array.ToString(/*writeIndented: true*/).Should().Be(
@"[
  1,
  ""a"",
  true,
  null
]");
    }

    [Fact]
    public void SerializeObject() 
    {
        JsonObject.Empty.ToString().Should().Be("{}");
        var @object = new JsonObject(new("a", 1), new("b", "a"), new("c", true), new("d", null));
        @object.ToString(writeIndented: false).Should().Be("{\"a\":1,\"b\":\"a\",\"c\":true,\"d\":null}");
        @object.ToString(/*writeIndented: true*/).Should().Be(
@"{
  ""a"": 1,
  ""b"": ""a"",
  ""c"": true,
  ""d"": null
}");
    }

    [Fact]
    public void SerializeArrayInObject() 
    {
        var @object = new JsonObject
        (
            new("a", 1),
            new("b", "a"),
            new("c", true),
            new("d", null),
            new("e", new JsonArray
            (
                2, "b", false
            ))
        );
        @object.ToString(writeIndented: false).Should().Be(
            "{\"a\":1,\"b\":\"a\",\"c\":true,\"d\":null,\"e\":[2,\"b\",false]}");
        @object.ToString(/*writeIndented: true*/).Should().Be(
@"{
  ""a"": 1,
  ""b"": ""a"",
  ""c"": true,
  ""d"": null,
  ""e"": [
    2,
    ""b"",
    false
  ]
}");
    }

    [Fact]
    public void SerializeObjectInArray()
    {
        var @object = new JsonArray
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
            )
        );
        @object.ToString(writeIndented: false).Should().Be(
            "[1,\"a\",true,null,{\"a\":2,\"b\":\"b\",\"c\":false}]");
        @object.ToString(/*writeIndented: true*/).Should().Be(
@"[
  1,
  ""a"",
  true,
  null,
  {
    ""a"": 2,
    ""b"": ""b"",
    ""c"": false
  }
]");
    }
}
