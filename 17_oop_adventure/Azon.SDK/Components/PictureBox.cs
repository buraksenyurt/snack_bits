using Azon.SDK.Contracts;
using static System.Net.Mime.MediaTypeNames;

namespace Azon.SDK.Components
{
    public class PictureBox(int id, string name, (double, double) position)
        : Control(id, name, position), IDrawable
    {
        public string ImagePath { get; set; }

        public void Draw()
        {
            Console.WriteLine("Picture Box draw işlemi");
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}|{base.ToString()}|{ImagePath}";
        }
    }
}
