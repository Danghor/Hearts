using System;
using System.Collections.Generic;
using System.Text;

namespace GameMaster
{
    public class CardPlayedEventArgs : EventArgs
    {
        public Card Card { get; }

        public CardPlayedEventArgs(Card card)
        {
            Card = card;
        }
    }
}
