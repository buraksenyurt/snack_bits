using Azon.SDK.Components;

namespace Azon.SDK.Contracts
{
    public interface IParsable
    {
        static abstract Control From(string[] columns);
    }
}
