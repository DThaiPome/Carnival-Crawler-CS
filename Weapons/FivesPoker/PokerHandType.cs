using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnivalCrawler.Weapons.FivesPoker
{
    /// <summary>
    /// Categories of poker hands, in order of rank.
    /// </summary>
    public enum PokerHandType
    {
        Junk = 0,
        Pair = 1,
        TwoPair = 2,
        Threes = 3,
        Fours = 4,
        FullHouse = 5,
        Flush = 6,
        Straight = 7,
        StraightFlush = 8,
        RoyalFlush = 9
    }
}
