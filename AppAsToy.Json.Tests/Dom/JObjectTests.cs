namespace AppAsToy.Json.Tests;
public class JObjectTests
{
    [Fact]
    public void ConstructWithParams()
    {
        JObject @object = new
        (
            new("a", 1),
            new("b", "a"),
            new("c", true),
            new("d", null),
            new("e", new JObject
            (
                new("ea", 2),
                new("eb", "b"),
                new("ec", false)
            )),
            new("f", new JArray
            (
                1,
                "a",
                true,
                null
            ))
        );

        TestJsonObject(@object);
    }

    [Fact]
    public void ConstructWithEnumerables()
    {
        JObject @object = new(new JProperty[]
        {
            new ("a", 1),
            new ("b", "a"),
            new ("c", true),
            new ("d", null),
            new("e", new JProperty[]
            {
                new("ea", 2),
                new("eb", "b"),
                new("ec", false)
            }),
            new("f", new JElement?[]
            {
                1,
                "a",
                true,
                null
            })
        });

        TestJsonObject(@object);
    }

    [Fact]
    public void CastFromEnumerables()
    {
        JObject @object = new JProperty[]
        {
            new ("a", 1),
            new ("b", "a"),
            new ("c", true),
            new ("d", null),
            new("e", new JProperty[]
            {
                new("ea", 2),
                new("eb", "b"),
                new("ec", false)
            }),
            new("f", new JElement?[]
            {
                1,
                "a",
                true,
                null
            })
        };

        TestJsonObject(@object);
    }

    [Fact]
    public void InitializeByProperty()
    {
        JObject @object = new()
        {
            ["a"] = 1,
            ["b"] = "a",
            ["c"] = true,
            ["d"] = null,
            ["e"] = new JObject
            {
                ["ea"] = 2,
                ["eb"] = "b",
                ["ec"] = false
            },
            ["f"] = new JArray
            {
                [0] = 1,
                [1] = "a",
                [2] = true,
                [3] = null
            }
        };

        TestJsonObject(@object);
    }

    private static void TestJsonObject(JObject @object)
    {
        @object.Count.Should().Be(6);
        (@object["a"] == 1).Should().BeTrue();
        (@object["b"] == "a").Should().BeTrue();
        (@object["c"] == true).Should().BeTrue();
        (@object["d"] == null).Should().BeTrue();

        var subObject = @object["e"]!;
        (subObject["ea"] == 2).Should().BeTrue();
        (subObject["eb"] == "b").Should().BeTrue();
        (subObject["ec"] == false).Should().BeTrue();

        var subArray = @object["f"]!;
        (subArray[0] == 1).Should().BeTrue();
        (subArray[1] == "a").Should().BeTrue();
        (subArray[2] == true).Should().BeTrue();
        (subArray[3] == null).Should().BeTrue();
    }
}
