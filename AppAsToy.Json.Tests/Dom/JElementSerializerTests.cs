namespace AppAsToy.Json.Tests;

public class JElementSerializerTests
{
    [Fact]
    public void Serialize_Null()
    {
        JElement.Null.ToString().Should().Be("null");
    }

    [Fact]
    public void Serialize_Bool()
    {
        JBool.True.ToString().Should().Be("true");
        JBool.False.ToString().Should().Be("false");
    }

    [Fact]
    public void Serialize_String()
    {
        JString.Empty.ToString().Should().Be("\"\"");
        new JString("abc").ToString().Should().Be("\"abc\"");
        new JString("가나다").ToString().Should().Be("\"가나다\"");
        new JString("\"/\\\b\f\n\r\t").ToString().Should().Be("\"\\\"\\/\\\\\\b\\f\\n\\r\\t\"");
    }

    [Fact]
    public void Serialize_String_DateTime()
    {
        var now = DateTime.Now;
        JString.FromDateTime(now).ToString().Should().Be($"\"{now.ToString(@"yyyy\-MM\-dd HH\:mm\:ss")}\"");
    }

    [Fact]
    public void Serialize_String_DateTimeOffset()
    {
        var now = DateTimeOffset.Now;
        JString.FromDateTimeOffset(now).ToString().Should().Be($"\"{now.ToString(@"yyyy\-MM\-dd HH\:mm\:ss K")}\"");
    }

    [Fact]
    public void Serialize_String_TimeSpan()
    {
        var span = new TimeSpan(123, 23, 59, 58);
        JString.FromTimeSpan(span).ToString().Should().Be($"\"{span.ToString(@"d\.hh\:mm\:ss")}\"");
    }

    [Fact]
    public void Serialize_String_Guid()
    {
        var guid = Guid.NewGuid();
        JString.FromGuid(guid).ToString().Should().Be($"\"{guid.ToString("N")}\"");
    }

    [Fact]
    public void Serialize_String_ByteArray()
    {
        var byteArray = new byte[] { 1, 2, 3, 4 };
        JString.FromByteArray(byteArray).ToString().Should().Be($"\"{Convert.ToBase64String(byteArray)}\"");
    }

    [Fact]
    public void Serialize_Number() 
    {
        JNumber.Zero.ToString().Should().Be("0");
        new JNumber(sbyte.MaxValue).ToString().Should().Be(sbyte.MaxValue.ToString());
        new JNumber(short.MaxValue).ToString().Should().Be(short.MaxValue.ToString());
        new JNumber(int.MaxValue).ToString().Should().Be(int.MaxValue.ToString());
        new JNumber(long.MaxValue >> 12).ToString().Should().Be((long.MaxValue >> 12).ToString());
        new JNumber(byte.MaxValue).ToString().Should().Be(byte.MaxValue.ToString());
        new JNumber(ushort.MaxValue).ToString().Should().Be(ushort.MaxValue.ToString());
        new JNumber(uint.MaxValue).ToString().Should().Be(uint.MaxValue.ToString());
        new JNumber(ulong.MaxValue >> 12).ToString().Should().Be((ulong.MaxValue >> 12).ToString());
        new JNumber(0.1f).ToString("F1").Should().Be("0.1");
        new JNumber(0.123d).ToString().Should().Be("0.123");
        new JNumber(0.1234m).ToString().Should().Be("0.1234");
    }

    [Fact]
    public void Serialize_Array() 
    {
        JArray.Empty.ToString().Should().Be("[]");
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
        array.Serialize(writeIndent: false).Should().Be("[1,\"a\",true,null,{\"a\":2,\"b\":\"b\",\"c\":false},[3,\"c\",true]]");
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
        JObject.Empty.ToString().Should().Be("{}");
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
        @object.Serialize(writeIndent: false).Should().Be("{\"a\":1,\"b\":\"a\",\"c\":true,\"d\":null,\"e\":[2,\"b\",false],\"f\":{\"_a\":3,\"_b\":\"c\",\"_c\":true}}");
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
