using Azon.SDK.Contracts;

namespace Azon.SDK.Components
{
    public class Button(int id, string name, (double, double) position)
        : BaseButton(id, name, position), IDrawable, IParsable
    {
        public string Text { get; set; }
        public string BackgroundColor { get; set; }

        public static Control From(string[] columns)
        {
            var id = Convert.ToInt32(columns[1]);
            var name = columns[2];
            var position = columns[3].Split(':');
            var xValue = Convert.ToDouble(position[0]);
            var yValue = Convert.ToDouble(position[1]);
            var text = columns[4];
            var backgroundColor = columns[5];

            return new Button(id, name, (xValue, yValue))
            {
                BackgroundColor = backgroundColor,
                Text = text
            };
        }

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
