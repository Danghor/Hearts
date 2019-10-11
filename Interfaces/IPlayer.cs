using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameMaster
{
    public interface IPlayer
    {
        /// <summary>
        /// Tell the player the hand (i.e. the cards) they have.
        /// </summary>
        /// <param name="hand">The cards passed to the player by the game master.</param>
        void TellHand(IEnumerable<Card> hand);

        /// <summary>
        /// Ask the player to play a card now.
        /// </summary>
        /// <returns>A task that ultimately resolves to the card being played by the player.</returns>
        Task<Card> PlayCardAsync();

        void NotifyCardPlayed(Card card);
    }
}
