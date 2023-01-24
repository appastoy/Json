using AppAsToy.Json.DOM;

namespace AppAsToy.Json.Tests.DOM;
public class JsonArrayTests
{
    [Fact]
    public void ConstructWithParams()
    {
        JsonArray array = new
        (
            1,
            "a",
            true,
            null,
            new JsonArray
            (
                2,
                "b",
                false
            ),
            new JsonObject
            (
                new("a", 1),
                new("b", "a"),
                new("c", true),
                new("d", null)
            )
        );

        TestJsonArray(array);
    }

    [Fact]
    public void ConstructWithEnumerables()
    {
        JsonArray array = new(new JsonElement?[]
        {
            1,
            "a",
            true,
            null,
            new JsonElement?[]
            {
                2,
                "b",
                false
            },
            new JsonProperty[]
            {
                new("a", 1),
                new("b", "a"),
                new("c", true),
                new("d", null)
            }
        });

        TestJsonArray(array);
    }

    [Fact]
    public void CastFromEnumerables()
    {
        JsonArray array = new JsonElement?[]
        {
            1,
            "a",
            true,
            null,
            new JsonElement?[]
            {
                2,
                "b",
                false
            },
            new JsonProperty[]
            {
                new("a", 1),
                new("b", "a"),
                new("c", true),
                new("d", null)
            }
        };

        TestJsonArray(array);
    }

    [Fact]
    public void InitializeByProperty()
    {
        JsonArray array = new()
        {
            [0] = 1,
            [1] = "a",
            [2] = true,
            [3] = null,
            [4] = new JsonArray
            {
                [0] = 2,
                [1] = "b",
                [2] = false
            },
            [5] = new JsonObject
            {
                ["a"] = 1,
                ["b"] = "a",
                ["c"] = true,
                ["d"] = null
            }
        };

        TestJsonArray(array);
    }

    private static void TestJsonArray(JsonArray array)
    {
        array.Count.Should().Be(6);
        (array[0] == 1).Should().BeTrue();
        (array[1] == "a").Should().BeTrue();
        (array[2] == true).Should().BeTrue();
        (array[3] == null).Should().BeTrue();

        var subArray = array[4]!;
        subArray.Count.Should().Be(3);
        (subArray[0] == 2).Should().BeTrue();
        (subArray[1] == "b").Should().BeTrue();
        (subArray[2] == false).Should().BeTrue();

        var subObject = array[5]!;
        subObject.Count.Should().Be(4);
        (subObject["a"] == 1).Should().BeTrue();
        (subObject["b"] == "a").Should().BeTrue();
        (subObject["c"] == true).Should().BeTrue();
        (subObject["d"] == null).Should().BeTrue();
    }
}
