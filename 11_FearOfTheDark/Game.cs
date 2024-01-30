namespace FearOfTheDark
{
    public class Game
    {
        private List<Character> _characters;

        public Game()
        {
            _characters = [];
        }

        public void AddCharacters(List<Character> characters)
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
