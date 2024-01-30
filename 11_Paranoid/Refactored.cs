namespace Paranoid
{
    public interface IEditor
    {
        void Add(RequestForChange rfc);
        void Update(RequestForChange rfc);
    }

    public interface IReader
    {
        void Vote(RequestForChange rfc);
    }

    public class Author
        : IEditor
    {
        public void Add(RequestForChange rfc)
        {
            Console.WriteLine($"'{rfc.Title}' eklendi.");
        }

        public void Update(RequestForChange rfc)
        {
            Console.WriteLine($"'{rfc.Title}' düzenlendi.");
        }
    }

    public class User
        : IReader
    {
        public void Vote(RequestForChange rfc)
        {
            rfc.VoteCount++;
            Console.WriteLine($"'{rfc.Title}' oy aldı. Toplam {rfc.VoteCount}");
        }
    }
}
