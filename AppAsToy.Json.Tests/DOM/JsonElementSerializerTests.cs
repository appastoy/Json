using AppAsToy.Json.DOM;

namespace AppAsToy.Json.Tests.DOM;

public class JsonElementSerializerTests
{
    [Fact]
    public void Serialize_Null()
    {
        JsonElement.Null.ToString().Should().Be("null");
    }

    [Fact]
    public void Serialize_Bool()
    {
        JsonBool.True.ToString().Should().Be("true");
        JsonBool.False.ToString().Should().Be("false");
    }

    [Fact]
    public void Serialize_String()
    {
        JsonString.Empty.ToString().Should().Be("\"\"");
        new JsonString("abc").ToString().Should().Be("\"abc\"");
        new JsonString("가나다").ToString().Should().Be("\"가나다\"");
        new JsonString("\"/\\\b\f\n\r\t").ToString().Should().Be("\"\\\"\\/\\\\\\b\\f\\n\\r\\t\"");
    }

    [Fact]
    public void Serialize_String_DateTime()
    {
        var now = DateTime.Now;
        JsonString.FromDateTime(now).ToString().Should().Be($"\"{now.ToString(@"yyyy\-MM\-dd HH\:mm\:ss")}\"");
    }

    [Fact]
    public void Serialize_String_DateTimeOffset()
    {
        var now = DateTimeOffset.Now;
        JsonString.FromDateTimeOffset(now).ToString().Should().Be($"\"{now.ToString(@"yyyy\-MM\-dd HH\:mm\:ss K")}\"");
    }

    [Fact]
    public void Serialize_String_TimeSpan()
    {
        var span = new TimeSpan(123, 23, 59, 58);
        JsonString.FromTimeSpan(span).ToString().Should().Be($"\"{span.ToString(@"d\.hh\:mm\:ss")}\"");
    }

    [Fact]
    public void Serialize_String_Guid()
    {
        var guid = Guid.NewGuid();
        JsonString.FromGuid(guid).ToString().Should().Be($"\"{guid.ToString()}\"");
    }

    [Fact]
    public void Serialize_String_ByteArray()
    {
        var byteArray = new byte[] { 1, 2, 3, 4 };
        JsonString.FromByteArray(byteArray).ToString().Should().Be($"\"{Convert.ToBase64String(byteArray)}\"");
    }

    [Fact]
    public void Serialize_Number() 
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
    public void Serialize_Array() 
    {
        JsonArray.Empty.ToString().Should().Be("[]");
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
        array.ToString(writeIndented: false).Should().Be("[1,\"a\",true,null,{\"a\":2,\"b\":\"b\",\"c\":false},[3,\"c\",true]]");
        array.ToString(/*writeIndented: true*/).Should().Be(
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
]");
    }

    [Fact]
    public void Serialize_Object() 
    {
        JsonObject.Empty.ToString().Should().Be("{}");
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
        @object.ToString(writeIndented: false).Should().Be("{\"a\":1,\"b\":\"a\",\"c\":true,\"d\":null,\"e\":[2,\"b\",false],\"f\":{\"_a\":3,\"_b\":\"c\",\"_c\":true}}");
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
  ],
  ""f"": {
    ""_a"": 3,
    ""_b"": ""c"",
    ""_c"": true
  }
}");
    }
}
