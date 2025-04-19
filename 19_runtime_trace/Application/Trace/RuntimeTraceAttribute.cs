using MethodDecorator.Fody.Interfaces;
using System.Reflection;

namespace Application.Trace;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class MethodTraceAttribute : Attribute, IMethodDecorator
{
    public static ILogger Logger { get; set; } = new SerilogAdapter();

    public void Init(object instance, MethodBase method, object[] args)
    {
        Logger.Info($"[TRACE] Initializing: {method.DeclaringType.FullName}.{method.Name}");
    }

    public void OnEntry()
    {
        Logger.Info($"[TRACE] Entering method.");
    }

    public void OnExit()
    {
        Logger.Info($"[TRACE] Exiting method.");
    }

    public void OnException(Exception exception)
    {
        Logger.Error($"[TRACE] Exception occurred.", exception);
    }
}