using Sdk;

namespace AzonWorks;
public class Game
    : IEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = "Unknown Name";
    public double Point { get; set; }
    public Game(int id, string title, double point)
    {
        Id = id;
        Title = title;
        Point = point;
    }
    public override string ToString() => $"{Id}|{Title}|{Point}";
}