using System.Reflection;
using System;
using System.Linq;
using System.IO;
using System.Text;
using Domain;
public class Program
{
    private static void Main(string[] args)
    {
        var fields = typeof(ProcessType)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly);

        var builder = new StringBuilder();
        builder.AppendLine($"public enum {nameof(ProcessType)}Enum");
        builder.AppendLine("{");
        foreach (var field in fields)
        {
            builder.AppendLine($"\t{field.Name} = {typeof(ProcessType).GetField(field.Name).GetValue(null)},");
        }
        builder.AppendLine("}");

        File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "Output.cs"), builder.ToString());
    }
}

namespace Domain
{
    public static class ProcessType
    {
        public const int Student = 1001;
        public const int Beginner = 1005;
        public const int Premium = 7007;
        public const int Employee = 9042;
    }
}



