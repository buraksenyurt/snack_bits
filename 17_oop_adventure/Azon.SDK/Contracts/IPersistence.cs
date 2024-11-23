using Azon.SDK.Components;

namespace Azon.SDK.Contracts
{
    public interface IPersistence
    {
        bool Save(Control[] controls);
        Control[] Load();
    }
}
