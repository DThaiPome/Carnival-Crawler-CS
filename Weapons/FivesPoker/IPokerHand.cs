using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnivalCrawler.Weapons.FivesPoker
{
    public interface IPokerHand
    {
        /// <summary>
        /// Returns the type of the hand present.
        /// </summary>
        /// <returns>a type of poker hand.</returns>
        PokerHandType GetHandType();

        /// <summary>
        /// Checks if a card of the given index is involved in
        /// the hand.
        /// </summary>
        /// <returns>true if this card is part of the hand, false
        /// otherwise.</returns>
        bool IsCardInHand(int index);

        /// <summary>
        /// Gets the [index]th card of the hand.
        /// </summary>
        /// <param name="index">the 0 based index of the card</param>
        /// <returns>the card at this position, or else throws an error.</returns>
        PlayingCard GetCardAt(int index);
    }
}
