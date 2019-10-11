using ArtificialIntelligence;

namespace GameMaster
{
    public class AiGame
    {
        readonly AiPlayer player1 = new AiPlayer();
        readonly AiPlayer player2 = new AiPlayer();
        readonly AiPlayer player3 = new AiPlayer();

        readonly IPlayer humanPlayer;

        public AiGame(IPlayer humanPlayer)
        {
            this.humanPlayer = humanPlayer;
        }

        Game NewGame()
        {
            Game game = new Game(player1, player2, player3, humanPlayer);

            return game;
        }
    }
}
