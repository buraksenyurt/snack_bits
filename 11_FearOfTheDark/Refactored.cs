namespace FearOfTheDark
{
    public interface ICharacter
    {
        void Attack();
    }

    public class OrcWarrior
        : ICharacter
    {
        public void Attack()
        {
            Console.WriteLine("Savaşçı kılıçla karşı saldırıya geçti");
        }
    }

    public class OrcWizzard
        : ICharacter
    {
        public void Attack()
        {
            Console.WriteLine("Büyücü sihirli gücüyle karşı saldırıya geçti");
        }
    }

    public class OrcArcher
        : ICharacter
    {
        public void Attack()
        {
            Console.WriteLine("Okçu ok fırlatarak saldırıya geçti");
        }
    }
}
