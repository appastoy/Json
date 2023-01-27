using System;

namespace AppAsToy.Json;

public static class JConvert
{
    public static string Serialize<T>(T obj, JConvertOption? option = null)
        where T : class
    {
        if (obj == null)
            return "null";

        return "";
    }

    public static string SerializeValue<T>(T obj, JConvertOption? option = null)
        where T : struct
    {
        return "";
    }

    public static string Serialize(object obj, JConvertOption? option = null)
    {
        if (obj == null)
            return "null";

        return "";
    }

    public static T Deserialize<T>(string json, JConvertOption? option = null)
        where T : class
    {
        if (json == null)
            throw new ArgumentNullException(nameof(json));

        return null;
    }

    public static bool TryDeserialize<T>(string json, out T? value)
        where T : class
    {
        return TryDeserialize(json, JConvertOption.Default, out value);
    }

    public static bool TryDeserialize<T>(string json, JConvertOption option, out T? value)
        where T : class
    {
        if (json == null)
            throw new ArgumentNullException(nameof(json));

        try
        {
            value = Deserialize<T>(json, option);
            return true;
        }
        catch
        {
            value = default;
            return false;
        }
    }

    public static T DeserializeValue<T>(string json, JConvertOption? option = null)
        where T : struct
    {
        if (json == null)
            throw new ArgumentNullException(nameof(json));

        return default;
    }

    public static bool TryDeserializeValue<T>(string json, out T value)
        where T : struct
    {
        return TryDeserializeValue(json, JConvertOption.Default, out value);
    }

    public static bool TryDeserializeValue<T>(string json, JConvertOption option, out T value)
        where T : struct
    {
        if (json == null)
            throw new ArgumentNullException(nameof(json));

        try
        {
            value = DeserializeValue<T>(json, option);
            return true;
        }
        catch
        {
            value = default;
            return false;
        }
    }

    public static object Deserialize(string json, Type type, JConvertOption? option = null)
    {
        if (json == null)
            throw new ArgumentNullException(nameof(json));

        return default;
    }

    public static bool TryDeserialize(string json, Type type, out object? value)
    {
        return TryDeserialize(json, type, JConvertOption.Default, out value);
    }

    public static bool TryDeserialize(string json, Type type, JConvertOption option, out object? value)
    {
        if (json == null)
            throw new ArgumentNullException(nameof(json));

        try
        {
            value = Deserialize(json, type, option);
            return true;
        }
        catch
        {
            value = default;
            return false;
        }
    }
}
