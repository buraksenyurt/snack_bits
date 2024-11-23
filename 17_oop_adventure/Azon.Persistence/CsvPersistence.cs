using Azon.SDK.Components;
using Azon.SDK.Contracts;
using System.Reflection;
using System.Text;

namespace Azon.Persistence
{
    public class CsvPersistence
        : IFilePersistence
    {
        public string FileName { get; set; } = Path.Combine(Environment.CurrentDirectory, "Form.dat");

        public string FileType => "Csv";

        private static readonly Dictionary<string, Type> controlMap = new()
        {
            { "Button", typeof(Button) },
            { "HiddenButton", typeof(HiddenButton) },
            { "CheckBoxButton", typeof(CheckBoxButton) },
            { "LinkButton", typeof(LinkButton) },
            { "GridBox", typeof(GridBox) },
            { "PictureBox", typeof(PictureBox) },
            { "Label", typeof(Label) }
        };

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

                if (!controlMap.TryGetValue(controlType, out var type))
                {
                    //TODO@buraksenyurt Needed logging
                    continue;
                }

                var parserMethod = type.GetMethod("From", BindingFlags.Public | BindingFlags.Static);
                if (parserMethod == null)
                {
                    //TODO@buraksenyurt Needed logging
                    continue;
                }

                var control = parserMethod.Invoke(null, [columns]);
                if (control is Control validControl)
                {
                    controls.Add(validControl);
                }
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
