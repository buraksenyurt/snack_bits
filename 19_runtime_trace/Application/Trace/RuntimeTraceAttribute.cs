using MethodDecorator.Fody.Interfaces;
using Serilog;
using System.Reflection;

namespace Application.Trace;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class MethodTraceAttribute : Attribute, IMethodDecorator
{
    public void Init(object instance, MethodBase method, object[] args)
    {
        Log.Information($"[TRACE] Initializing: {method.DeclaringType.FullName}.{method.Name}");
    }

    public void OnEntry()
    {
        Log.Information($"[TRACE] Entering method.");
    }

    public void OnExit()
    {
        Log.Information($"[TRACE] Exiting method.");
    }

    public void OnException(Exception exception)
    {
        Log.Information($"[TRACE] Exception: {exception.Message}");
    }
}