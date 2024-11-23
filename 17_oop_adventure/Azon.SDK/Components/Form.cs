using Azon.SDK.Contracts;

namespace Azon.SDK.Components
{
    public class Form(IPersistence persistance)
    {
        private readonly List<Control> controls = [.. persistance.Load()];
        private readonly IPersistence _persistance = persistance;

        public void AddControls(params Control[] items)
        {
            controls.AddRange(items);
        }
        public List<Control> GetControls()
        {
            return controls;
        }

        public void Draw()
        {
            foreach (var control in controls)
            {
                if (control is IDrawable drawable)
                {
                    drawable.Draw();
                }
            }
        }
        public void Save()
        {
            _ = _persistance.Save([.. controls]);
            //TODO@buraksenyurt If this is not works it needs to logging
        }
    }
}