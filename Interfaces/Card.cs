using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

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

        public static IList<Card> ShuffledDeck()
        {
            var deck = (from Suit suit in Enum.GetValues(typeof(Suit))
                        from Rank rank in Enum.GetValues(typeof(Rank))
                        select new Card(suit, rank)).ToList();

            Shuffle(deck);

            return deck;
        }

        private static void Shuffle<T>(IList<T> list)
        {
            using (var provider = new RNGCryptoServiceProvider())
            {
                int n = list.Count;
                while (n > 1)
                {
                    byte[] box = new byte[1];
                    do provider.GetBytes(box);
                    while (box[0] >= n * (byte.MaxValue / n));
                    int k = (box[0] % n);
                    n--;
                    T value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
            }
        }
    }
}