namespace FearOfTheDark
{
    public class SmartGame
    {
        private List<AttackingCharacter> _characters;

        public SmartGame()
        {
            _characters = [];
        }

        public void AddCharacters(List<AttackingCharacter> characters)
        {
            _characters = characters;
        }

        public void FullAttack()
        {
            foreach (var character in _characters)
            {
                character.Attack();
            }
        }
    }
}
