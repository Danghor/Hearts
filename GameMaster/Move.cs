using System;
using System.Collections.Generic;
using System.Text;

namespace GameMaster
{
    internal class Move
    {
        internal PlayerLog Player { get; }
        internal Card Card { get; }

        public Move(PlayerLog player, Card card)
        {
            Player = player;
            Card = card;
        }
    }
}
