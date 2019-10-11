using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameMaster
{
    public class Game
    {
        private readonly List<PlayerLog> playerLogs = new List<PlayerLog>();

        public Game(params IPlayer[] players)
        {
            if (players.Length != 4)
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

        public async Task StartGameAsync()
        {
            while (!playerLogs.Any(player => player.Score >= 100))
            {
                await StartRoundAsync().ConfigureAwait(false);
            }
        }

        public async Task StartRoundAsync()
        {
            var deck = Card.ShuffledDeck();

            foreach (PlayerLog playerLog in playerLogs)
            {
                var cardsPassed = deck.Take(13).ToList();
                playerLog.Hand = cardsPassed;

                foreach (Card card in cardsPassed)
                {
                    deck.Remove(card);
                }
            }

            var startPlayer = playerLogs.Single(p => p.Hand.Any(card => card.Rank == Rank.Two && card.Suit == Suit.Clubs));

            while (startPlayer.Hand.Count > 0)
            {
                var currentTrick = new List<Move>();

                PlayerLog currentPlayer = startPlayer;
                while (currentTrick.Count < 4)
                {
                    var cardPlayed = await currentPlayer.PlayCardAsync().ConfigureAwait(false);

                    foreach (IPlayer player in playerLogs.Select(l => l.Player))
                    {
                        player.NotifyCardPlayed(cardPlayed);
                    }

                    currentTrick.Add(new Move(currentPlayer, cardPlayed));

                    var currentPlayerIndex = playerLogs.IndexOf(currentPlayer);
                    currentPlayer = playerLogs[(currentPlayerIndex + 1) % 4];
                }

                var firstSuit = currentTrick.Single(move => move.Player == startPlayer).Card.Suit;

                startPlayer = (from move in currentTrick
                               orderby move.Card.Rank descending
                               where move.Card.Suit == firstSuit
                               select move.Player).First();

                startPlayer.Score += CalculateScore(currentTrick.Select(move => move.Card).ToList());
            }
        }

        private static int CalculateScore(ICollection<Card> trick)
        {
            var score = 0;

            if (trick.Any(card => card.Suit == Suit.Spades && card.Rank == Rank.Queen))
            {
                score += 13;
            }

            score += trick.Count(card => card.Suit == Suit.Hearts);

            return score;
        }
    }
}