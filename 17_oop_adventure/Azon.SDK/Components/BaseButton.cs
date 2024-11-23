namespace Azon.SDK.Components
{
    public abstract class BaseButton(int id, string name, (double, double) position)
        : Control(id, name, position)
    {
        protected bool CurvedCorner { get; set; }
    }
}
