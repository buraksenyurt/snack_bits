using Azon.SDK.Contracts;

namespace Azon.SDK.Components
{
    public class GridBox(int id, string name, (double, double) position)
        : Control(id, name, position), IDrawable
    {
        public int ColumnCount { get; set; }
        public int RowCount { get; set; }
        public void Draw()
        {
            Console.WriteLine($"GridBox draw {ColumnCount}:{RowCount}");
        }
        public override string ToString()
        {
            return $"{this.GetType().Name}|{base.ToString()}|{ColumnCount}|{RowCount}";
        }
    }
}
