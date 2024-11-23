using Azon.SDK.Contracts;

namespace Azon.SDK.Components
{
    public class Label(int id, string name, (double, double) position)
        : Control(id, name, position), IDrawable
    {
        public string Text { get; set; }

        public void Draw()
        {
            Console.WriteLine("Label çizilecek");
        }
        public override string ToString()
        {
            return $"{this.GetType().Name}|{base.ToString()}|{Text}";
        }
    }
}
