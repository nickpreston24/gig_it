using System.Reflection;
using System.Text;
using System.Text.Json;
using CodeMechanic.Diagnostics;

namespace gig_it.Models;

public static class Resources
{
    public static class Embedded
    {
        public static string TestTxt
        {
            get
            {
                var info = Assembly.GetExecutingAssembly().GetName();
                var name = info.Name;
                using var stream = Assembly
                    .GetExecutingAssembly()
                    .GetManifestResourceStream($"{name}.Embedded.test.txt")!;
                using var streamReader = new StreamReader(stream, Encoding.UTF8);
                return streamReader.ReadToEnd();
            }
        }

        public static Person Person
        {
            get
            {
                var info = Assembly.GetExecutingAssembly().GetName();
                var name = info.Name;
                Console.WriteLine("ass name:>> " + name);
                using var stream = Assembly
                    .GetExecutingAssembly()
                    .GetManifestResourceStream($"{name}.Embedded.person.json")!;
                stream.Dump("stream");
                return JsonSerializer.Deserialize<Person>(stream)!;
            }
        }
    }
}
