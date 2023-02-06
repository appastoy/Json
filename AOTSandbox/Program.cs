namespace AOTSandbox;
internal static class Program
{
    [STAThread]
    static void Main()
    {
        try
        {
            Console.WriteLine("List<string>");

            var list = TypeBuilder.Build<string>();
            list.Add("abc");
            Console.WriteLine(string.Join(Environment.NewLine, list.Select(i => i.ToString())));
        }
        finally
        {
            Console.ReadKey();
        }
        try
        {
            Console.WriteLine("List<byte>");

            var list = TypeBuilder.Build<byte>();
            list.Add(0);
            Console.WriteLine(string.Join(Environment.NewLine, list.Select(i => i.ToString())));
        }
        finally
        {
            Console.ReadKey();
        }
    }
}
