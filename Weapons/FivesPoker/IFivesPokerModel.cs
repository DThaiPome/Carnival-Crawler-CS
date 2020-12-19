using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnivalCrawler.Weapons.FivesPoker
{
    /// <summary>
    /// A model for a short round of fives poker with no opponent.
    /// </summary>
    public interface IFivesPokerModel
    {
        /// <summary>
        /// If player has no cards, draw five cards for the player. Shuffle
        /// when jokers are encountered.
        /// If the player already has cards, throw an error.
        /// </summary>
        void DrawHand();

        /// <summary>
        /// If player has cards and has not yet exchanged, then for
        /// each boolean isExchanged[i] that is true, discard the ith
        /// card and draw another card from the deck.
        /// If cards have already been exchanged, throw an error.
        /// </summary>
        /// <param name="isExchanged"></param>
        void ExchangeCards(bool[] isExchanged);

        /// <summary>
        /// If cards have been drawn or exchanged, retrieve the current hand. Otherwise,
        /// throw an error.
        /// </summary>
        /// <returns>the current hand, in an array of cards</returns>
        PlayingCard[] GetCurrentHand();

        /// <summary>
        /// If player has exchanged cards, then return the final hand. Otherwise,
        /// throw an error
        /// </summary>
        /// <returns>The final hand</returns>
        IPokerHand GetFinalHand();

        /// <summary>
        /// Clears the current hand.
        /// </summary>
        void ResetHand();

        /// <summary>
        /// Gets how many joker cards were drawn in the last set of draws.
        /// </summary>
        /// <returns>number of joker cards drawn since calling DrawHand or
        /// ExchangeCards</returns>
        int GetJokersDrawn();
    }
}
