using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameMaster;

namespace ArtificialIntelligence
{
    public class AiPlayer : IPlayer
    {
        ICollection<Card> Hand { get; set; }

        public Task<Card> PlayCardAsync()
        {
            // play random legal card
            throw new NotImplementedException();
        }

        public void TellHand(IEnumerable<Card> hand)
        {
            Hand = hand.ToList();
        }

        public void OnGameCardPlayed(object sender, CardPlayedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
