using Azon.SDK.Components;
using Azon.SDK.Contracts;
using System.Text;

namespace Azon.Persistence
{
    public class CsvPersistence
        : IFilePersistence
    {
        public string FileName { get; set; } = Path.Combine(Environment.CurrentDirectory, "Form.dat");

        public string FileType => "Csv";

        public Control[] Load()
        {
            if (!File.Exists(FileName))
            {
                return [];
            }

            var controls = new List<Control>();
            var lines = File.ReadAllLines(FileName);
            foreach (var line in lines)
            {

                var columns = line.Split('|');
                var controlType = columns[0];

                var id = Convert.ToInt32(columns[1]);
                var name = columns[2];
                var position = columns[3].Split(':');
                var xValue = Convert.ToDouble(position[0]);
                var yValue = Convert.ToDouble(position[1]);

            }

            return [.. controls];
        }

        public bool Save(Control[] controls)
        {
            var builder = new StringBuilder();
            foreach (var control in controls)
            {
                builder.AppendLine(control.ToString());
            }
            File.WriteAllText(FileName, builder.ToString());
            return true;
        }
    }
}
