using Azon.SDK.Contracts;
using static System.Net.Mime.MediaTypeNames;

namespace Azon.SDK.Components
{
    public class HiddenButton
        : BaseButton, IParsable
    {
        public string Value { get; set; }
        public HiddenButton(int id, string name, (double, double) position)
            : base(id, name, position)
        {
        }
        public override string ToString()
        {
            return $"{this.GetType().Name}|{base.ToString()}|{Value}";
        }

        public static Control From(string[] columns)
        {
            var id = Convert.ToInt32(columns[1]);
            var name = columns[2];
            var position = columns[3].Split(':');
            var xValue = Convert.ToDouble(position[0]);
            var yValue = Convert.ToDouble(position[1]);
            var value = columns[4];

            return new HiddenButton(id, name, (xValue, yValue))
            {
                Value = value
            };
        }
    }
}
