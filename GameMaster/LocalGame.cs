using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ArtificialPlayer;

namespace GameMaster
{
    public class LocalGame
    {
        Player[] players;

        public LocalGame()
        {
            var deck = Card.Deck();

            Shuffle(deck);

            while(deck.Any())
            {

            }
        }

        static void Shuffle<T>(IList<T> list)
        {
            using (var provider = new RNGCryptoServiceProvider())
            {
                int n = list.Count;
                while (n > 1)
                {
                    byte[] box = new byte[1];
                    do provider.GetBytes(box);
                    while (!(box[0] < n * (byte.MaxValue / n)));
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