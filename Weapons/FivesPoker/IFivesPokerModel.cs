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
        /// If player has no cards, draw five cards for the player.
        /// If a joker is drawn, shuffle the deck and draw an extra card.
        /// If the player already has cards, do nothing.
        /// </summary>
        void DrawHand();

        /// <summary>
        /// If player has cards and has not yet exchanged, then for
        /// each boolean isExchanged[i] that is true, discard the ith
        /// card and draw another card from the deck
        /// </summary>
        /// <param name="isExchanged"></param>
        /// <returns></returns>
        bool[] ExchangeCards(bool[] isExchanged);

        /// <summary>
        /// Gets how many joker cards were drawn in the last set of draws.
        /// </summary>
        /// <returns>number of joker cards drawn since calling DrawHand or
        /// ExchangeCards</returns>
        int GetJokersDrawn();
    }
}
