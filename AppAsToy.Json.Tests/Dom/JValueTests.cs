namespace AppAsToy.Json.Tests;

public class JValueTests
{
    [Fact]
    public void Null()
    {
        JElement.Null.Should().NotBeNull();
        JElement.Null.Should().BeAssignableTo<JElement>();

        JElement.Null.IsNull.Should().BeTrue();
        (JElement.Null == null).Should().BeTrue();
        (JElement.Null != null).Should().BeFalse();
    }

    [Fact]
    public void Number()
    {
        JNumber.Zero.AsDouble.Should().Be(0.0d);
        var number1 = new JNumber(0.1d); number1.AsDouble.Should().Be(0.1d);
        var number2 = new JNumber(0.1f); number2.AsFloat.Should().Be(0.1f);
        var number3 = new JNumber(sbyte.MaxValue); number3.AsSByte.Should().Be(sbyte.MaxValue);
        var number4 = new JNumber(short.MaxValue); number4.AsShort.Should().Be(short.MaxValue);
        var number5 = new JNumber(int.MaxValue); number5.AsInt.Should().Be(int.MaxValue);
        var number6 = new JNumber(long.MaxValue >> 12); number6.AsLong.Should().Be(long.MaxValue >> 12);
        var number7 = new JNumber(byte.MaxValue); number7.AsByte.Should().Be(byte.MaxValue);
        var number8 = new JNumber(ushort.MaxValue); number8.AsUShort.Should().Be(ushort.MaxValue);
        var number9 = new JNumber(uint.MaxValue); number9.AsUInt.Should().Be(uint.MaxValue);
        var number10 = new JNumber(ulong.MaxValue >> 12); number10.AsULong.Should().Be(ulong.MaxValue >> 12);
        var number11 = new JNumber(1234m); number11.AsDecimal.Should().Be(1234m);

        ((object)(JElement)0.1d).Should().Be(number1);
        ((object)(JElement)0.1f).Should().Be(number2);
        ((object)(JElement)sbyte.MaxValue).Should().Be(number3);
        ((object)(JElement)short.MaxValue).Should().Be(number4);
        ((object)(JElement)int.MaxValue).Should().Be(number5);
        ((object)(JElement)(long.MaxValue >> 12)).Should().Be(number6);
        ((object)(JElement)byte.MaxValue).Should().Be(number7);
        ((object)(JElement)ushort.MaxValue).Should().Be(number8);
        ((object)(JElement)uint.MaxValue).Should().Be(number9);
        ((object)(JElement)(ulong.MaxValue >> 12)).Should().Be(number10);
        ((object)(JElement)1234m).Should().Be(number11);

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
        JString.Empty.RawValue.Should().BeEmpty();
        var str1 = new JString("abc"); str1.RawValue.Should().Be("abc");
        new JString("가나다").RawValue.Should().Be("가나다");
#pragma warning disable CS8625
        new Action(() => new JString(null)).Should().Throw<ArgumentNullException>();
#pragma warning restore CS8625

        ((object)(JElement)"abc").Should().Be(str1);

        str1.Equals("abc").Should().BeTrue();
        (str1 == "abc").Should().BeTrue();
        (str1 != "abc").Should().BeFalse();
    }

    [Fact]
    public void Bool()
    {
        JBool.True.RawValue.Should().BeTrue();
        JBool.False.RawValue.Should().BeFalse();

        ((object)(JElement)true).Should().Be(JBool.True);
        ((object)(JElement)false).Should().Be(JBool.False);

        JBool.True.Equals(true).Should().BeTrue();
        JBool.False.Equals(false).Should().BeTrue();
        (JBool.True == true).Should().BeTrue();
        (JBool.True != true).Should().BeFalse();
    }

    [Fact]
    public void String_DateTime()
    {
        var nowTicks = DateTime.Now.Ticks;
        var now = new DateTime(nowTicks - (nowTicks % TimeSpan.TicksPerSecond));
        var nowJson = JString.FromDateTime(now);
        nowJson.AsDateTime().HasValue.Should().BeTrue();
        new Func<DateTime>(() => nowJson.ToDateTime()).Should().NotThrow();

        nowJson.Equals(now).Should().BeTrue();
        (nowJson == now).Should().BeTrue();
        (nowJson != now).Should().BeFalse();
        nowJson.RawValue.Should().Be(now.ToString(@"yyyy\-MM\-dd HH\:mm\:ss"));
        ((object)(JElement)new DateTime(1,2,3,4,5,6)).Should().Be(new DateTime(1,2,3,4,5,6));
    }

    [Fact]
    public void String_DateTimeOffset()
    {
        var nowTicks = DateTime.Now.Ticks;
        var now = new DateTimeOffset(nowTicks - (nowTicks % TimeSpan.TicksPerSecond), DateTimeOffset.Now.Offset);
        var nowJson = JString.FromDateTimeOffset(now);
        nowJson.AsDateTimeOffset().HasValue.Should().BeTrue();
        new Func<DateTimeOffset>(() => nowJson.ToDateTimeOffset()).Should().NotThrow();

        nowJson.Equals(now).Should().BeTrue();
        (nowJson == now).Should().BeTrue();
        (nowJson != now).Should().BeFalse();
        nowJson.RawValue.Should().Be(now.ToString(@"yyyy\-MM\-dd HH\:mm\:ss K"));
        ((object)(JElement)new DateTimeOffset(1, 2, 3, 4, 5, 6, TimeSpan.FromHours(9))).Should().Be(new DateTimeOffset(1, 2, 3, 4, 5, 6, TimeSpan.FromHours(9)));
    }

    [Fact]
    public void String_TimeSpan()
    {
        var randomSpan = TimeSpan.FromTicks(new Random().Next() * TimeSpan.TicksPerSecond);
        var randomJson = JString.FromTimeSpan(randomSpan);

        randomJson.AsTimeSpan().HasValue.Should().BeTrue();
        new Func<TimeSpan>(() => randomJson.ToTimeSpan()).Should().NotThrow();
                
        randomJson.Equals(randomSpan).Should().BeTrue();
        (randomJson == randomSpan).Should().BeTrue();
        (randomJson != randomSpan).Should().BeFalse();
        randomJson.RawValue.Should().Be(randomSpan.ToString(@"d\.hh\:mm\:ss"));
        ((object)(JElement)randomJson).Should().Be(randomSpan);
    }

    [Fact]
    public void String_Guid()
    {
        var randomGuid = Guid.NewGuid();
        var randomJson = JString.FromGuid(randomGuid);

        randomJson.AsGuid().HasValue.Should().BeTrue();
        new Func<Guid>(() => randomJson.ToGuid()).Should().NotThrow();

        randomJson.Equals(randomGuid).Should().BeTrue();
        (randomJson == randomGuid).Should().BeTrue();
        (randomJson != randomGuid).Should().BeFalse();
        randomJson.RawValue.Should().Be(randomGuid.ToString("N"));
        ((object)(JElement)randomJson).Should().Be(randomGuid);
        randomJson.ToString().Should().Be($"\"{randomGuid.ToString("N")}\"");
    }

    [Fact]
    public void String_ByteArray()
    {
        var byteArray = new byte[] { 1, 2, 3, 4 };
        var byteArrayJson = JString.FromByteArray(byteArray);

        byteArrayJson.AsByteArray.Should().NotBeEmpty();
        new Func<byte[]>(() => byteArrayJson.ToByteArray).Should().NotThrow();

        byteArrayJson.Equals(byteArrayJson).Should().BeTrue();
        (byteArrayJson == byteArray).Should().BeTrue();
        (byteArrayJson != byteArray).Should().BeFalse();
        byteArrayJson.RawValue.Should().Be(Convert.ToBase64String(byteArray));
        ((object)(JElement)byteArrayJson).Should().Be(byteArray);
    }
}