using Azon.Persistence;
using Azon.SDK.Components;

namespace Azon.Editor
{
    internal class Program
    {
        static void Main()
        {
            var saveButton = new Button(1, "btnSave", (1, 0))
            {
                Text = "Save",
                BackgroundColor = "Gray"
            };
            var closeButton = new Button(2, "btnClose", (1, 1))
            {
                Text = "Close",
                BackgroundColor = "Red"
            };
            var isActiveButton = new CheckBoxButton(3, "chkIsActive", (10, 1))
            {
                Text = "Is Active Profile"
            };
            var lnkCompany = new LinkButton(3, "lnkCompany", (100, 50))
            {
                Url = new Uri("https://www.buraksenyurt.com")
            };
            var summary = new GridBox(4, "grdProducts", (0, 90))
            {
                RowCount = 3,
                ColumnCount = 3
            };
            var photoBox = new PictureBox(5, "pcbProfilePhoto", (5, 5))
            {
                ImagePath = "assets/photo.png"
            };
            var hiddenButton = new HiddenButton(6, "hdnAmount", (0, 0))
            {
                Value = "1000"
            };

            var csvPersistance = new CsvPersistence();
            var mainForm = new Form(csvPersistance);
            mainForm.AddControls(saveButton, closeButton, hiddenButton, isActiveButton, lnkCompany, summary, photoBox);
            mainForm.Draw();
            mainForm.Save();
        }
    }
}
