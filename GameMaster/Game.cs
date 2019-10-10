using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ArtificialIntelligence;

namespace GameMaster
{
    public class Game
    {
        public event EventHandler<CardPlayedEventArgs> CardPlayed;
        public event EventHandler<TrickTakenEventArgs> TrickTaken;
        public event EventHandler<RoundFinishedEventArgs> RoundFinished;
        public event EventHandler GameOver;

        PlayerLog[] playerLogs = new PlayerLog[4];

        public Game(IEnumerable<IPlayer> players)
        {
            if (players.Count() != 4)
            {
                throw new ArgumentException("Exactly 4 players must play.", nameof(players));
            }

            const int count = 0;
            foreach (IPlayer player in players)
            {
                playerLogs[count] = new PlayerLog
                {
                    Player = player
                };
            }
        }

        public void StartGame()
        {
            var deck = Card.Deck();
            Shuffle(deck);

            foreach (PlayerLog playerLog in playerLogs)
            {
                var cardsPassed = deck.Take(13);
                playerLog.Hand = cardsPassed.ToList();

                foreach (Card card in cardsPassed)
                {
                    deck.Remove(card);
                }
            }

            while (!playerLogs.Any(player => player.Score >= 100))
            {
                StartRound();
            }

            GameOver?.Invoke(this, EventArgs.Empty);
        }

        public void StartRound()
        {
            var startPlayer = playerLogs.Single(p => p.Hand.Any(card => card.Rank == Rank.Two && card.Suit == Suit.Clubs));

            while (startPlayer.Hand.Count > 0)
            {
                var cardPlayed = startPlayer.PlayCard();

                CardPlayed?.Invoke(this, new CardPlayedEventArgs(cardPlayed));

                var currentTrick = new List<Card>();
                currentTrick.Add(cardPlayed);

                if (currentTrick.Count == 4)
                {
                    //var trickTaker = from 
                }
            }

            RoundFinished?.Invoke(this, null);
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