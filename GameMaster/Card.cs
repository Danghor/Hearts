using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameMaster
{
    public class Card
    {
        public Suit Suit { get; }
        public Rank Rank { get; }

        Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        internal static IList<Card> Deck()
        {
            return (from Suit suit in Enum.GetValues(typeof(Suit))
                    from Rank rank in Enum.GetValues(typeof(Rank))
                    select new Card(suit, rank)).ToList();
        }
    }
}