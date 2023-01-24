using AppAsToy.Json.DOM;

namespace AppAsToy.Json.Tests.DOM;
public class JsonObjectTests
{
    [Fact]
    public void ConstructWithParams()
    {
        JsonObject @object = new
        (
            new("a", 1),
            new("b", "a"),
            new("c", true),
            new("d", null),
            new("e", new JsonObject
            (
                new("ea", 2),
                new("eb", "b"),
                new("ec", false)
            )),
            new("f", new JsonArray
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
        JsonObject @object = new(new JsonProperty[]
        {
            new ("a", 1),
            new ("b", "a"),
            new ("c", true),
            new ("d", null),
            new("e", new JsonProperty[]
            {
                new("ea", 2),
                new("eb", "b"),
                new("ec", false)
            }),
            new("f", new JsonElement?[]
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
        JsonObject @object = new JsonProperty[]
        {
            new ("a", 1),
            new ("b", "a"),
            new ("c", true),
            new ("d", null),
            new("e", new JsonProperty[]
            {
                new("ea", 2),
                new("eb", "b"),
                new("ec", false)
            }),
            new("f", new JsonElement?[]
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
        JsonObject @object = new()
        {
            ["a"] = 1,
            ["b"] = "a",
            ["c"] = true,
            ["d"] = null,
            ["e"] = new JsonObject
            {
                ["ea"] = 2,
                ["eb"] = "b",
                ["ec"] = false
            },
            ["f"] = new JsonArray
            {
                [0] = 1,
                [1] = "a",
                [2] = true,
                [3] = null
            }
        };

        TestJsonObject(@object);
    }

    private static void TestJsonObject(JsonObject @object)
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
