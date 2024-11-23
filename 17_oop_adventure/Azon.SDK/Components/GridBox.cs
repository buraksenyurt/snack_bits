using Azon.SDK.Contracts;

namespace Azon.SDK.Components
{
    public class GridBox(int id, string name, (double, double) position)
        : Control(id, name, position), IDrawable, IParsable
    {
        public int ColumnCount { get; set; }
        public int RowCount { get; set; }

        public static Control From(string[] columns)
        {
            var id = Convert.ToInt32(columns[1]);
            var name = columns[2];
            var position = columns[3].Split(':');
            var xValue = Convert.ToDouble(position[0]);
            var yValue = Convert.ToDouble(position[1]);
            var columnCount = Convert.ToInt32(columns[4]);
            var rowCount = Convert.ToInt32(columns[5]);

            return new GridBox(id, name, (xValue, yValue))
            {
                ColumnCount = columnCount,
                RowCount = rowCount,
            };
        }

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
