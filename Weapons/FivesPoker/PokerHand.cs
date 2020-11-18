using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnivalCrawler.Weapons.FivesPoker
{
    public class PokerHand : IPokerHand
    {
        private PlayingCard[] cards;

        private PokerHandType handType;
        private bool[] cardsInvolved;

        public PokerHand(PlayingCard[] cards)
        {
            if (cards.Length != 5)
            {
                throw new ArgumentException("must be a deck of 5 cards");
            }
            this.cards = cards;

            this.cardsInvolved = new bool[5];
            this.handType = this.FindHandType();
        }

        private PokerHandType FindHandType()
        {
            Tuple<Func<List<PlayingCard>, bool>, PokerHandType>[] checks =
            {
                GetCheckTypePair(CheckForRoyalFlush, PokerHandType.RoyalFlush),
                GetCheckTypePair(CheckForStraightFlush, PokerHandType.StraightFlush),
                GetCheckTypePair(CheckForStraight, PokerHandType.Straight),
                GetCheckTypePair(CheckForFullHouse, PokerHandType.FullHouse),
                GetCheckTypePair(CheckForFours, PokerHandType.Fours),
                GetCheckTypePair(CheckForThrees, PokerHandType.Threes),
                GetCheckTypePair(CheckForTwoPair, PokerHandType.TwoPair),
                GetCheckTypePair(CheckForPair, PokerHandType.Pair),
                GetCheckTypePair(CheckForFlush, PokerHandType.Flush),
            };

            foreach(Tuple<Func<List<PlayingCard>, bool>, PokerHandType> pair in checks)
            {
                Func<List<PlayingCard>, bool> check = pair.Item1;
                List<PlayingCard> cardsInvolved = new List<PlayingCard>();
                if (check(cardsInvolved))
                {
                    this.IncludeCardsInHand(pair.Item2, cardsInvolved);   
                    return pair.Item2;
                }
            }

            return PokerHandType.Junk;
        }

        //The checks aren't guaranteed to include every card, so
        private void IncludeCardsInHand(PokerHandType type, List<PlayingCard> cardsInvolved)
        {
            PokerHandType[] rankTypes =
            {
                PokerHandType.Pair,
                PokerHandType.TwoPair,
                PokerHandType.Threes,
                PokerHandType.Fours
            };
            if (rankTypes.Contains(type))
            {
                this.IncludeCardsOfRank(cardsInvolved);
            } else
            {
                this.IncludeAllCards();
            }
        }

        private void IncludeCardsOfRank(List<PlayingCard> cardsInvolved)
        {
            for (int i = 0; i < this.cards.Length; i++)
            {
                PlayingCard card = this.cards[i];
                if (cardsInvolved.Exists(PlayingCard.CompareRank(card.rank)))
                {
                    this.cardsInvolved[i] = true;
                }
            }
        }

        private void IncludeAllCards()
        {
            for(int i = 0; i < this.cardsInvolved.Length; i++)
            {
                this.cardsInvolved[i] = true;
            }
        }

        private static Tuple<Func<List<PlayingCard>, bool>, PokerHandType> GetCheckTypePair(Func<List<PlayingCard>, bool> check, PokerHandType type)
        {
            return new Tuple<Func<List<PlayingCard>, bool>, PokerHandType>(check, type);
        }

        //Assume there is nothing greater than two pair
        private bool CheckForPair(List<PlayingCard> cardsInHand)
        {
            for(int i = 0; i < this.cards.Length; i++)
            {
                PlayingCard card = this.cards[i];
                if (this.CheckForOtherPairMember(card, i))
                {
                    cardsInHand.Add(card);
                    return true;
                }
            }
            return false;
        }

        private bool CheckForOtherPairMember(PlayingCard firstMember, int firstMemberIndex)
        {
            for(int i = 0; i < this.cards.Length; i++)
            {
                if (i == firstMemberIndex)
                {
                    continue;
                } 

                PlayingCard card = this.cards[i];
                if (card.rank == firstMember.rank)
                {
                    return true;
                }
            }
            return false;
        }

        //Assume nothing greater than threes
        private bool CheckForTwoPair(List<PlayingCard> cardsInHand)
        {
            for(int i = 0; i < this.cards.Length; i++)
            {
                PlayingCard card = this.cards[i];
                if (cardsInHand.Exists(PlayingCard.CompareRank(card.rank)))
                {
                    continue;
                }
                if (CheckForOtherPairMember(card, i))
                {
                    cardsInHand.Add(card);
                }
                if (cardsInHand.Count == 2)
                {
                    return true;
                }
            }
        }

        private bool CheckForThrees(List<PlayingCard> cardsInHand)
        {

        }

        private bool CheckForFours(List<PlayingCard> cardsInHand)
        {

        }

        private bool CheckForFullHouse(List<PlayingCard> cardsInHand)
        {

        }

        private bool CheckForFlush(List<PlayingCard> cardsInHand)
        {

        }

        private bool CheckForStraight(List<PlayingCard> cardsInHand)
        {

        }

        private bool CheckForStraightFlush(List<PlayingCard> cardsInHand)
        {

        }

        private bool CheckForRoyalFlush(List<PlayingCard> cardsInHand)
        {

        }

        public PlayingCard GetCardAt(int index)
        {
            if (index < 0 || index >= cards.Length)
            {
                throw new ArgumentException("out of range");
            } else
            {
                return this.cards[index];
            }
        }

        public PokerHandType GetHandType()
        {
            return this.handType;
        }

        public bool IsCardInHand(int index)
        {
            if (index < 0 || index >= this.cardsInvolved.Length)
            {
                throw new ArgumentException("out of range");
            }
            return this.cardsInvolved[index];
        }
    }
}
