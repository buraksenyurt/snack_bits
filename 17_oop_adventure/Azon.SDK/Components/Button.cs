using Azon.SDK.Contracts;

namespace Azon.SDK.Components
{
    public class Button(int id, string name, (double, double) position)
        : BaseButton(id, name, position), IDrawable
    {
        public string Text { get; set; }
        public string BackgroundColor { get; set; }

        public void Draw()
        {
            Console.WriteLine($"Button draw {Position.Item1}:{Position.Item2}");
        }
        public override string ToString()
        {
            return $"{this.GetType().Name}|{base.ToString()}|{Text}|{BackgroundColor}";
        }
    }
}
