using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ArtificialIntelligence;

namespace GameMaster
{
    public class Game
    {
        public event EventHandler<CardPlayedEventArgs> CardPlayed;
        public event EventHandler<TrickTakenEventArgs> TrickTaken;
        public event EventHandler<RoundFinishedEventArgs> RoundFinished;
        public event EventHandler GameOver;

        List<PlayerLog> playerLogs = new List<PlayerLog>();

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

        public async void StartGameAsync()
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

            while (!playerLogs.Any(player => player.Score >= 100))
            {
                await StartRoundAsync();
            }

            GameOver?.Invoke(this, new GameOverEventArgs(playerLogs.Select(playerLog => new Tuple<IPlayer, int>(playerLog.Player, playerLog.Score))));
        }

        public async Task StartRoundAsync()
        {
            var startPlayer = playerLogs.Single(p => p.Hand.Any(card => card.Rank == Rank.Two && card.Suit == Suit.Clubs));

            while (startPlayer.Hand.Count > 0)
            {
                var currentTrick = new List<Move>();

                PlayerLog currentPlayer = startPlayer;
                while (currentTrick.Count < 4)
                {
                    var cardPlayed = await currentPlayer.PlayCardAsync();

                    CardPlayed?.Invoke(this, new CardPlayedEventArgs(cardPlayed));

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

                TrickTaken?.Invoke(this, new TrickTakenEventArgs(startPlayer.Player));
            }

            RoundFinished?.Invoke(this, null);
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