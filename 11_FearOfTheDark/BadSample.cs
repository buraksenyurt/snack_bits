namespace FearOfTheDark
{
    public class Character
    {
        public virtual void Attack()
        {
            Console.WriteLine("Karakter saldırıyor");
        }
    }

    public class Warrior
        : Character
    {
        public override void Attack()
        {
            Console.WriteLine("Savaşçı kılıçla karşı saldırıya geçti");
        }
    }

    public class Wizzard
        : Character
    {
        public override void Attack()
        {
            Console.WriteLine("Büyücü sihirli gücüyle karşı saldırıya geçti");
        }
    }

    public class Archer
        : Character
    {
    }
}
