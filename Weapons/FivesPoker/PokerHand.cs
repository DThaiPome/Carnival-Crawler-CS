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
                if (this.CheckForMatchingRanks(card, i, 1))
                {
                    cardsInHand.Add(card);
                    return true;
                }
            }
            return false;
        }

        private bool CheckForMatchingRanks(PlayingCard firstMember, int firstMemberIndex, int targetCount)
        {
            int count = 0;
            for(int i = 0; i < this.cards.Length; i++)
            {
                if (i == firstMemberIndex)
                {
                    continue;
                }

                PlayingCard card = this.cards[i];
                if (card.rank == firstMember.rank)
                {
                    count++;
                }
            }
            return count == targetCount;
        }

        //Assume nothing greater than threes
        private bool CheckForTwoPair(List<PlayingCard> cardsInHand)
        {
            int pairCount = 0;
            for(int i = 0; i < this.cards.Length; i++)
            {
                PlayingCard card = this.cards[i];
                if (cardsInHand.Exists(PlayingCard.CompareRank(card.rank)))
                {
                    continue;
                }
                if (CheckForMatchingRanks(card, i, 1))
                {
                    cardsInHand.Add(card);
                    pairCount++;
                }
                if (pairCount == 2)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckForThrees(List<PlayingCard> cardsInHand)
        {
            for (int i = 0; i < this.cards.Length; i++)
            {
                PlayingCard card = this.cards[i];
                if (this.CheckForMatchingRanks(card, i, 2))
                {
                    cardsInHand.Add(card);
                    return true;
                }
            }
            return false;
        }

        private bool CheckForFours(List<PlayingCard> cardsInHand)
        {
            for (int i = 0; i < this.cards.Length; i++)
            {
                PlayingCard card = this.cards[i];
                if (this.CheckForMatchingRanks(card, i, 3))
                {
                    cardsInHand.Add(card);
                    return true;
                }
            }
            return false;
        }

        private bool CheckForFullHouse(List<PlayingCard> cardsInHand)
        {
            return CheckForThrees(cardsInHand) && CheckForPair(cardsInHand);
        }

        private bool CheckForFlush(List<PlayingCard> cardsInHand)
        {
            PlayingCard.Suit suit = this.cards[0].suit;
            for(int i = 1; i < this.cards.Length; i++)
            {
                PlayingCard card = this.cards[i];
                if (card.suit != suit)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckForStraight(List<PlayingCard> cardsInHand)
        {
            List<PlayingCard> cardList = this.cards.ToList();
            SortCardListByRank(cardList);
            for(int i = 1; i < this.cards.Length; i++)
            {
                PlayingCard card = cardList[i];
                PlayingCard prevCard = cardList[i - 1];
                if (card.rank != prevCard.rank + 1)
                {
                    return false;
                }
            }
            return true;
        }

        private void SortCardListByRank(List<PlayingCard> cardList)
        {
            cardList.Sort(PlayingCard.RankComparison());
        }

        private bool CheckForStraightFlush(List<PlayingCard> cardsInHand)
        {
            return CheckForFlush(cardsInHand) && CheckForStraight(cardsInHand);
        }

        private bool CheckForRoyalFlush(List<PlayingCard> cardsInHand)
        {
            if (this.CheckForFlush(cardsInHand))
            {
                List<PlayingCard> cardList = this.cards.ToList();
                int[] ranks = { 1, 10, 11, 12, 13 };
                foreach(int i in ranks)
                {
                    if (!cardList.Exists(PlayingCard.CompareRank(i)))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
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
