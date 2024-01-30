namespace Paranoid
{
    public interface IUser
    {
        void Add(RequestForChange rfc);
        void Vote(RequestForChange rfc);
    }

    public class Editor
        : IUser
    {
        public void Add(RequestForChange rfc)
        {
            Console.WriteLine($"'{rfc.Title}' eklendi.");
        }

        public void Vote(RequestForChange rfc)
        {
            rfc.VoteCount++;
            Console.WriteLine($"'{rfc.Title}' oy aldı. Toplam {rfc.VoteCount}");
        }
    }

    public class Subscriber
        : IUser
    {
        public void Add(RequestForChange rfc)
        {
            Console.WriteLine($"'{rfc.Title}' eklendi.");
        }

        public void Vote(RequestForChange rfc)
        {
            rfc.VoteCount++;
            Console.WriteLine($"'{rfc.Title}' oy aldı. Toplam {rfc.VoteCount}");
        }
    }
}
