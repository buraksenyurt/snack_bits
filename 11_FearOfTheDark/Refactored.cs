namespace FearOfTheDark
{
    public abstract class AttackingCharacter
    {
        public abstract void Attack();
    }

    public class OrcWarrior
        : AttackingCharacter
    {
        public override void Attack()
        {
            Console.WriteLine("Savaşçı kılıçla karşı saldırıya geçti");
        }
    }

    public class OrcWizzard
        : AttackingCharacter
    {
        public override void Attack()
        {
            Console.WriteLine("Büyücü sihirli gücüyle karşı saldırıya geçti");
        }
    }

    public class OrcArcher
        : AttackingCharacter
    {
        public override void Attack()
        {
            Console.WriteLine("Okçu ok fırlatarak saldırıya geçti");
        }
    }
}
