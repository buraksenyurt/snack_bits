namespace FearOfTheDark
{
    public class SmartGame
    {
        private List<ICharacter> _characters;

        public SmartGame()
        {
            _characters = [];
        }

        public void AddCharacters(List<ICharacter> characters)
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
