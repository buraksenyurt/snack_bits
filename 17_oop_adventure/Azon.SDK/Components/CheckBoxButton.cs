using Azon.SDK.Contracts;

namespace Azon.SDK.Components
{
    public class CheckBoxButton
        : BaseButton, IDrawable, IParsable
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

        public static Control From(string[] columns)
        {
            var id = Convert.ToInt32(columns[1]);
            var name = columns[2];
            var position = columns[3].Split(':');
            var xValue = Convert.ToDouble(position[0]);
            var yValue = Convert.ToDouble(position[1]);
            var text = columns[4];
            var isChecked = Convert.ToBoolean(columns[5]);

            return new CheckBoxButton(id, name, (xValue, yValue))
            {
                Text = text,
                IsChecked = isChecked
            };
        }
    }
}
