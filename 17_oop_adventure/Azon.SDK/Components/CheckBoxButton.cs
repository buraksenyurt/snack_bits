using Azon.SDK.Contracts;

namespace Azon.SDK.Components
{
    public class CheckBoxButton
        : BaseButton, IDrawable
    {
        public string Text { get; set; }
        public bool IsChecked { get; set; }

        public CheckBoxButton(int id, string name, (double, double) position)
            : base(id, name, position)
        {
        }

        public void Draw()
        {
            Console.WriteLine($"CheckBox Button draw {Position.Item1}:{Position.Item2}");
        }
        public override string ToString()
        {
            return $"{this.GetType().Name}|{base.ToString()}|{Text}|{IsChecked}";
        }
    }
}
