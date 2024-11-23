namespace Azon.SDK.Components
{
    public abstract class Control(int id, string name, (double, double) position)
    {
        protected int Id { get; set; } = id;
        protected string Name { get; set; } = name;
        protected (double, double) Position { get; set; } = position;

        public override string ToString()
        {
            return $"{Id}|{Name}|{Position.Item1}:{Position.Item2}";
        }
    }
}
