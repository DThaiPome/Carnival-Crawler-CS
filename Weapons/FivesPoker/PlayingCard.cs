using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnivalCrawler.Weapons.FivesPoker
{
    public struct PlayingCard
    {
        public enum Suit
        {
            Spades = 0, 
            Clubs = 1, 
            Hearts = 2, 
            Diamonds = 3,
            Joker = 4
        }

        public Suit suit { get; private set; }
        public int rank { get; private set; }

        /// <summary>
        /// Create a playing card.
        /// </summary>
        /// <param name="suit">suit must be Spades, Clubs, Hearts, Diamonds, or Joker.</param>
        /// <param name="rank">rank must be within [1,13], unless suit is Joker.</param>
        public PlayingCard(Suit suit, int rank)
        {
            if (suit < 0 || suit > (Suit)4)
            {
                throw new ArgumentException("invalid suit");
            }
            if (suit != Suit.Joker && (rank < 1 || rank > 13))
            {
                throw new ArgumentException("invalid rank");
            }

            this.suit = suit;
            this.rank = rank;
        }

        /// <summary>
        /// String representation of card.
        /// </summary>
        /// <returns>rank + suit, or :) for joker.</returns>
        public override string ToString()
        {
            if (this.suit == Suit.Joker)
            {
                return this.SuitToString();
            } else
            {
                return this.RankToString() + this.SuitToString();
            }
        }

        /// <summary>
        /// Return the character for this suit, or :) for joker.
        /// </summary>
        /// <returns>one-character suit.</returns>
        private string SuitToString()
        {
            switch (this.suit)
            {
                case Suit.Spades:
                    return "♠";
                case Suit.Clubs:
                    return  "♣";
                case Suit.Hearts:
                    return "♡";
                case Suit.Diamonds:
                    return "♢";
                case Suit.Joker:
                    return ":)";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 1: A
        /// 10: T
        /// 11: J
        /// 12: K
        /// 13: Q
        /// All others are unchanged.
        /// </summary>
        /// <returns>one-character representation of card rank.</returns>
        private string RankToString()
        {
            switch(this.rank)
            {
                case 1:
                    return "A";
                case 10:
                    return "T";
                case 11:
                    return "J";
                case 12:
                    return "K";
                case 13:
                    return "Q";
                default:
                    return this.rank.ToString();
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() == typeof(PlayingCard))
            {
                return EqualsCard((PlayingCard)obj);
            }
            return false;
        }

        private bool EqualsCard(PlayingCard other)
        {
            return this.rank == other.rank
                && this.suit == other.suit;
        }

        /// <summary>
        /// Returns a predicate that returns true if the given playing card
        /// is of the given suit.
        /// </summary>
        /// <param name="suit">the suit to compare with.</param>
        /// <returns>a predicate that checks suit equivalence.</returns>
        public static Predicate<PlayingCard> CompareSuit(Suit suit)
        {
            return card =>
            {
                return card.suit == suit;
            };
        }

        /// <summary>
        /// Returns a predicate that returns true if the given playing card
        /// is of the given rank.
        /// </summary>
        /// <param name="rank">the rank to compare with.</param>
        /// <returns>a predicate that checks rank equivalence.</returns>
        public static Predicate<PlayingCard> CompareRank(int rank)
        {
            return card =>
            {
                return card.rank == rank;
            };
        }

        /// <summary>
        /// Returns a predicate that returns true if the given playing card
        /// equals the other given playing card.
        /// </summary>
        /// <param name="card">the card to compare with.</param>
        /// <returns>a prediacte that checks card equivalence.</returns>
        public static Predicate<PlayingCard> CompareCard(PlayingCard card)
        {
            return other =>
            {
                return other.Equals(card);
            };
        }
    }
}
