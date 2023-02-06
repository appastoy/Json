namespace AOTSandbox;
internal static class TypeBuilder
{
    internal static class PreventAOT<T>
    {

        internal static void Ensure() 
        {
            if (!typeof(List<T>).IsClass)
                throw new Exception();
        }
    }

    public static IList<T>? Build<T>()
    {
        PreventAOT<T>.Ensure();
        var newType = typeof(List<>).MakeGenericType(typeof(T));
        return Activator.CreateInstance(newType) as IList<T>;
    }
}
