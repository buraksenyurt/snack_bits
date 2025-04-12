using System.Diagnostics;

namespace Application.Trace;

internal class Utility
{
    public static void LogStackTrace()
    {
        var tracer = new StackTrace(true);
        var frames = tracer.GetFrames();
        if (frames != null)
        {
            foreach (var frame in frames)
            {
                Console.WriteLine($"\t{frame.GetMethod()?.DeclaringType?.FullName}.{frame.GetMethod()?.Name} (Line {frame.GetFileLineNumber()})");
            }
        }
    }
}
