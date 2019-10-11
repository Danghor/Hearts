using GameMaster;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtificialIntelligence
{
    public class AiPlayer : IPlayer
    {
        ICollection<Card> Hand { get; set; }

        readonly IList<Card> currentTrick = new List<Card>();

        public Task<Card> PlayCardAsync()
        {
            Card card;

            if (currentTrick.Count == 0)
            {
                Card twoOfClubs = Hand.SingleOrDefault(c => c.Rank == Rank.Two && c.Suit == Suit.Clubs);
                card = twoOfClubs ?? Hand.First();
            }
            else
            {
                var cardsOfValidSuit = from c in Hand
                                       where c.Suit == currentTrick.First().Suit
                                       select c;

                card = cardsOfValidSuit.FirstOrDefault() ?? Hand.First();
            }

            return Task.FromResult(card);
        }

        public void TellHand(IEnumerable<Card> hand)
        {
            Hand = hand.ToList();
        }

        public void NotifyCardPlayed(Card card)
        {
            if (currentTrick.Count == 4)
            {
                currentTrick.Clear();
            }

            currentTrick.Add(card);
        }
    }
}
