using System;
using System.Collections.Generic;
using System.Text;
using ArtificialIntelligence;

namespace GameMaster
{
    public class AiGame
    {
        AiPlayer player1 = new AiPlayer();
        AiPlayer player2 = new AiPlayer();
        AiPlayer player3 = new AiPlayer();

        IPlayer humanPlayer;

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
