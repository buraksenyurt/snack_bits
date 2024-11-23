using static System.Net.Mime.MediaTypeNames;

namespace Azon.SDK.Components
{
    public class HiddenButton
        : BaseButton
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
    }
}
