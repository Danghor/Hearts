using System;
using System.Collections.Generic;
using System.Text;

namespace GameMaster
{
    public interface IPlayer
    {
        void SetHand(IEnumerable<Card> hand);
        Card PlayCard();
    }
}
