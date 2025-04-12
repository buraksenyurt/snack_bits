using MethodDecorator.Fody.Interfaces;
using System.Diagnostics;
using System.Reflection;

namespace Application.Trace;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class MethodTraceAttribute : Attribute, IMethodDecorator
{
    public void Init(object instance, MethodBase method, object[] args)
    {
        Debug.WriteLine($"[TRACE] Initializing: {method.DeclaringType.FullName}.{method.Name}");
    }

    public void OnEntry()
    {
        Debug.WriteLine($"[TRACE] Entering method.");
    }

    public void OnExit()
    {
        Debug.WriteLine($"[TRACE] Exiting method.");
    }

    public void OnException(Exception exception)
    {
        Debug.WriteLine($"[TRACE] Exception: {exception.Message}");
    }
}