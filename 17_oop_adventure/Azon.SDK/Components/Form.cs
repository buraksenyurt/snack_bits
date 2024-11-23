using Azon.SDK.Contracts;

namespace Azon.SDK.Components
{
    public class Form
    {
        private readonly List<Control> controls = [];
        private readonly IPersistence _persistance;
        public Form(IPersistence persistance)
        {
            _persistance = persistance;
            controls = persistance.Load().ToList();
        }
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
            var result = _persistance.Save([.. controls]);
        }
    }
}