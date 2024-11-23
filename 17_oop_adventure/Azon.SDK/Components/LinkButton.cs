using Azon.SDK.Contracts;
using static System.Net.Mime.MediaTypeNames;

namespace Azon.SDK.Components
{
    public class LinkButton(int id, string name, (double, double) position)
                : BaseButton(id, name, position), IDrawable
    {
        public Uri Url { get; set; }

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
