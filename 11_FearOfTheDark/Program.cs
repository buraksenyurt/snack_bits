namespace FearOfTheDark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Bad Sample

            var game = new Game();
            game.AddCharacters([
                new Warrior(),
                new Warrior(),
                new Wizzard(),
                new Archer()
            ]);
            game.FullAttack();

            #endregion

            #region Refactored Sample

            var newGame = new SmartGame();
            newGame.AddCharacters([
               new OrcWarrior(),
               new OrcWarrior(),
               new OrcWizzard(),
               new OrcArcher()
            ]);
            newGame.FullAttack();

            #endregion
        }
    }
}
