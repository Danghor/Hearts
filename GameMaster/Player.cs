using System;
using System.Collections.Generic;

namespace GameMaster
{
    public abstract class Player
    {
        internal int Score { get; set; }

        internal ICollection<Card> Hand { get; set; }
    }
}
