using Azon.SDK.Contracts;
using static System.Net.Mime.MediaTypeNames;

namespace Azon.SDK.Components
{
    public class LinkButton(int id, string name, (double, double) position)
                : BaseButton(id, name, position), IDrawable, IParsable
    {
        public Uri Url { get; set; }

        public static Control From(string[] columns)
        {
            var id = Convert.ToInt32(columns[1]);
            var name = columns[2];
            var position = columns[3].Split(':');
            var xValue = Convert.ToDouble(position[0]);
            var yValue = Convert.ToDouble(position[1]);
            var url = new Uri(columns[4]);

            return new LinkButton(id, name, (xValue, yValue))
            {
                Url = url,
            };
        }

        public void Draw()
        {
            Console.WriteLine($"Link Button draw {Position.Item1}:{Position.Item2}");
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}|{base.ToString()}|{Url}";
        }
    }
}
