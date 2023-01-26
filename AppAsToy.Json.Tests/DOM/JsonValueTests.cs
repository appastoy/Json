using AppAsToy.Json.DOM;
using System.Text.RegularExpressions;

namespace AppAsToy.Json.Tests.DOM;

public class JsonValueTests
{
    [Fact]
    public void Null()
    {
        JsonElement.Null.Should().NotBeNull();
        JsonElement.Null.Should().BeAssignableTo<JsonElement>();

        JsonElement.Null.IsNull.Should().BeTrue();
        (JsonElement.Null == null).Should().BeTrue();
        (JsonElement.Null != null).Should().BeFalse();
    }

    [Fact]
    public void Number()
    {
        JsonNumber.Zero.AsDouble.Should().Be(0.0d);
        var number1 = new JsonNumber(0.1d); number1.AsDouble.Should().Be(0.1d);
        var number2 = new JsonNumber(0.1f); number2.AsFloat.Should().Be(0.1f);
        var number3 = new JsonNumber(sbyte.MaxValue); number3.AsSByte.Should().Be(sbyte.MaxValue);
        var number4 = new JsonNumber(short.MaxValue); number4.AsShort.Should().Be(short.MaxValue);
        var number5 = new JsonNumber(int.MaxValue); number5.AsInt.Should().Be(int.MaxValue);
        var number6 = new JsonNumber(long.MaxValue >> 12); number6.AsLong.Should().Be(long.MaxValue >> 12);
        var number7 = new JsonNumber(byte.MaxValue); number7.AsByte.Should().Be(byte.MaxValue);
        var number8 = new JsonNumber(ushort.MaxValue); number8.AsUShort.Should().Be(ushort.MaxValue);
        var number9 = new JsonNumber(uint.MaxValue); number9.AsUInt.Should().Be(uint.MaxValue);
        var number10 = new JsonNumber(ulong.MaxValue >> 12); number10.AsULong.Should().Be(ulong.MaxValue >> 12);
        var number11 = new JsonNumber(1234m); number11.AsDecimal.Should().Be(1234m);

        ((object)(JsonElement)0.1d).Should().Be(number1);
        ((object)(JsonElement)0.1f).Should().Be(number2);
        ((object)(JsonElement)sbyte.MaxValue).Should().Be(number3);
        ((object)(JsonElement)short.MaxValue).Should().Be(number4);
        ((object)(JsonElement)int.MaxValue).Should().Be(number5);
        ((object)(JsonElement)(long.MaxValue >> 12)).Should().Be(number6);
        ((object)(JsonElement)byte.MaxValue).Should().Be(number7);
        ((object)(JsonElement)ushort.MaxValue).Should().Be(number8);
        ((object)(JsonElement)uint.MaxValue).Should().Be(number9);
        ((object)(JsonElement)(ulong.MaxValue >> 12)).Should().Be(number10);
        ((object)(JsonElement)1234m).Should().Be(number11);

        number1.Equals(0.1d).Should().BeTrue();
        number2.Equals(0.1f).Should().BeTrue();
        number3.Equals(sbyte.MaxValue).Should().BeTrue();
        number4.Equals(short.MaxValue).Should().BeTrue();
        number5.Equals(int.MaxValue).Should().BeTrue();
        number6.Equals(long.MaxValue >> 12).Should().BeTrue();
        number7.Equals(byte.MaxValue).Should().BeTrue();
        number8.Equals(ushort.MaxValue).Should().BeTrue();
        number9.Equals(uint.MaxValue).Should().BeTrue();
        number10.Equals(ulong.MaxValue >> 12).Should().BeTrue();
        number11.Equals(1234m).Should().BeTrue();

        (number1 == 0.1d).Should().BeTrue();
        (number2 == 0.1f).Should().BeTrue();
        (number3 == sbyte.MaxValue).Should().BeTrue();
        (number4 == short.MaxValue).Should().BeTrue();
        (number5 == int.MaxValue).Should().BeTrue();
        (number6 == long.MaxValue >> 12).Should().BeTrue();
        (number7 == byte.MaxValue).Should().BeTrue();
        (number8 == ushort.MaxValue).Should().BeTrue();
        (number9 == uint.MaxValue).Should().BeTrue();
        (number10 == ulong.MaxValue >> 12).Should().BeTrue();
        (number11 == 1234m).Should().BeTrue();

        (number1 != 0.1d).Should().BeFalse();
        (number2 != 0.1f).Should().BeFalse();
        (number3 != sbyte.MaxValue).Should().BeFalse();
        (number4 != short.MaxValue).Should().BeFalse();
        (number5 != int.MaxValue).Should().BeFalse();
        (number6 != long.MaxValue >> 12).Should().BeFalse();
        (number7 != byte.MaxValue).Should().BeFalse();
        (number8 != ushort.MaxValue).Should().BeFalse();
        (number9 != uint.MaxValue).Should().BeFalse();
        (number10 != ulong.MaxValue >> 12).Should().BeFalse();
        (number11 != 1234m).Should().BeFalse();
    }

    [Fact]
    public void String()
    {
        JsonString.Empty.RawValue.Should().BeEmpty();
        var str1 = new JsonString("abc"); str1.RawValue.Should().Be("abc");
        new JsonString("가나다").RawValue.Should().Be("가나다");
#pragma warning disable CS8625
        new Action(() => new JsonString(null)).Should().Throw<ArgumentNullException>();
#pragma warning restore CS8625

        ((object)(JsonElement)"abc").Should().Be(str1);

        str1.Equals("abc").Should().BeTrue();
        (str1 == "abc").Should().BeTrue();
        (str1 != "abc").Should().BeFalse();
    }

    [Fact]
    public void Bool()
    {
        JsonBool.True.RawValue.Should().BeTrue();
        JsonBool.False.RawValue.Should().BeFalse();

        ((object)(JsonElement)true).Should().Be(JsonBool.True);
        ((object)(JsonElement)false).Should().Be(JsonBool.False);

        JsonBool.True.Equals(true).Should().BeTrue();
        JsonBool.False.Equals(false).Should().BeTrue();
        (JsonBool.True == true).Should().BeTrue();
        (JsonBool.True != true).Should().BeFalse();
    }

    [Fact]
    public void String_DateTime()
    {
        var nowTicks = DateTime.Now.Ticks;
        var now = new DateTime(nowTicks - (nowTicks % TimeSpan.TicksPerSecond));
        var nowJson = JsonString.FromDateTime(now);
        nowJson.AsDateTime.HasValue.Should().BeTrue();
        new Func<DateTime>(() => nowJson.ToDateTime).Should().NotThrow();

        nowJson.Equals(now).Should().BeTrue();
        (nowJson == now).Should().BeTrue();
        (nowJson != now).Should().BeFalse();
        nowJson.RawValue.Should().Be(now.ToString(@"yyyy\-MM\-dd HH\:mm\:ss"));
        ((object)(JsonElement)new DateTime(1,2,3,4,5,6)).Should().Be(new DateTime(1,2,3,4,5,6));
    }

    [Fact]
    public void String_DateTimeOffset()
    {
        var nowTicks = DateTime.Now.Ticks;
        var now = new DateTimeOffset(nowTicks - (nowTicks % TimeSpan.TicksPerSecond), DateTimeOffset.Now.Offset);
        var nowJson = JsonString.FromDateTimeOffset(now);
        nowJson.AsDateTimeOffset.HasValue.Should().BeTrue();
        new Func<DateTimeOffset>(() => nowJson.ToDateTimeOffset).Should().NotThrow();

        nowJson.Equals(now).Should().BeTrue();
        (nowJson == now).Should().BeTrue();
        (nowJson != now).Should().BeFalse();
        nowJson.RawValue.Should().Be(now.ToString(@"yyyy\-MM\-dd HH\:mm\:ss K"));
        ((object)(JsonElement)new DateTimeOffset(1, 2, 3, 4, 5, 6, TimeSpan.FromHours(9))).Should().Be(new DateTimeOffset(1, 2, 3, 4, 5, 6, TimeSpan.FromHours(9)));
    }

    [Fact]
    public void String_TimeSpan()
    {
        var randomSpan = TimeSpan.FromTicks(new Random().Next() * TimeSpan.TicksPerSecond);
        var randomJson = JsonString.FromTimeSpan(randomSpan);

        randomJson.AsTimeSpan.HasValue.Should().BeTrue();
        new Func<TimeSpan>(() => randomJson.ToTimeSpan).Should().NotThrow();
                
        randomJson.Equals(randomSpan).Should().BeTrue();
        (randomJson == randomSpan).Should().BeTrue();
        (randomJson != randomSpan).Should().BeFalse();
        randomJson.RawValue.Should().Be(randomSpan.ToString(@"d\.hh\:mm\:ss"));
        ((object)(JsonElement)randomJson).Should().Be(randomSpan);
    }

    [Fact]
    public void String_Guid()
    {
        var randomGuid = Guid.NewGuid();
        var randomJson = JsonString.FromGuid(randomGuid);

        randomJson.AsGuid.HasValue.Should().BeTrue();
        new Func<Guid>(() => randomJson.ToGuid).Should().NotThrow();

        randomJson.Equals(randomGuid).Should().BeTrue();
        (randomJson == randomGuid).Should().BeTrue();
        (randomJson != randomGuid).Should().BeFalse();
        randomJson.RawValue.Should().Be(randomGuid.ToString());
        ((object)(JsonElement)randomJson).Should().Be(randomGuid);
        randomJson.ToString().Should().Be($"\"{randomGuid.ToString()}\"");
    }

    [Fact]
    public void String_ByteArray()
    {
        var byteArray = new byte[] { 1, 2, 3, 4 };
        var byteArrayJson = JsonString.FromByteArray(byteArray);

        byteArrayJson.AsByteArray.Should().NotBeEmpty();
        new Func<byte[]>(() => byteArrayJson.ToByteArray).Should().NotThrow();

        byteArrayJson.Equals(byteArrayJson).Should().BeTrue();
        (byteArrayJson == byteArray).Should().BeTrue();
        (byteArrayJson != byteArray).Should().BeFalse();
        byteArrayJson.RawValue.Should().Be(Convert.ToBase64String(byteArray));
        ((object)(JsonElement)byteArrayJson).Should().Be(byteArray);
    }
}