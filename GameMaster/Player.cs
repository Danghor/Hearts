using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameMaster
{
    /// <summary>
    /// Used by the Game to keep track of the score and hand. This object is trusted, because only the GameMaster has access to it.
    /// </summary>
    internal class PlayerLog
    {
        internal IPlayer Player { get; set; }

        internal int Score { get; set; } = 0;

        ICollection<Card> hand;

        internal ICollection<Card> Hand
        {
            get => hand;
            set
            {
                hand = value;
                Player.TellHand(hand);
            }
        }

        internal Card PlayedCard { get; set; }

        internal async Task<Card> PlayCardAsync()
        {
            PlayedCard = await Player.PlayCardAsync();
            hand.Remove(PlayedCard);

            return PlayedCard;
        }

        internal IEnumerable<Card> PlayableCards(Suit? suit)
        {
            return suit == null ? Hand : Hand.Where(card => card.Suit == suit);
        }
    }
}
