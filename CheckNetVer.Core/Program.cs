using System.Text;
using System.Text.RegularExpressions;

namespace CheckNetVer.Core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new();
            string dir = Directory.GetCurrentDirectory();
            List<string> files = Directory.GetFiles(dir, "*.dll", SearchOption.AllDirectories).ToList();
            files.AddRange(Directory.GetFiles(dir, "*.exe", SearchOption.AllDirectories).ToList());
            foreach (string file in files)
            {
                string ver = GetTargetFramework(file);
                if(!string.IsNullOrWhiteSpace(ver))
                    sb.AppendLine($"{file};{ver}");

            }
            File.WriteAllText(Path.Combine(dir,"FrameworkReport.csv"), sb.ToString());
        }

        private static readonly Regex CompiledNetCoreRegex = new Regex(@".NETCoreApp,Version=v[0-9\.]+", RegexOptions.Compiled);
        private static readonly Regex CompiledNetFrameworkRegex = new Regex(@".NETFramework,Version=v[0-9\.]+", RegexOptions.Compiled);

        // You can define other methods, fields, classes and namespaces here
        public static string GetTargetFramework(string file)
        {
            string contents = File.ReadAllText(file);

            Match match = CompiledNetCoreRegex.Match(contents);
            if (match.Success)
                return match.Value;

            match = CompiledNetFrameworkRegex.Match(contents);
            return match.Success ? match.Value : null;
        }
    }
}