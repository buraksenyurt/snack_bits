using Azon.SDK.Contracts;

namespace Azon.SDK.Components
{
    public class Label(int id, string name, (double, double) position)
        : Control(id, name, position), IDrawable, IParsable
    {
        public string Text { get; set; }

        public static Control From(string[] columns)
        {
            var id = Convert.ToInt32(columns[1]);
            var name = columns[2];
            var position = columns[3].Split(':');
            var xValue = Convert.ToDouble(position[0]);
            var yValue = Convert.ToDouble(position[1]);
            var text = columns[4];

            return new Label(id, name, (xValue, yValue))
            {
                Text = text
            };
        }

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
