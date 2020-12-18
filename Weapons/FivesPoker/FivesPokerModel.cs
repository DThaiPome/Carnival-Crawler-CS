using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnivalCrawler.Weapons.FivesPoker
{
    public class FivesPokerModel : IFivesPokerModel
    {
        private Queue<PlayingCard> drawPile;
        private List<PlayingCard> discardPile;

        private PlayingCard[] hand;

        int jokersDrawn;

        public FivesPokerModel()
        {
            this.drawPile = new Queue<PlayingCard>();
            this.discardPile = new List<PlayingCard>();
            this.InitCards();

            this.hand = new PlayingCard[5];
            this.jokersDrawn = 0;
        }

        private void InitCards()
        {
            for (int suit = 0; suit < 4; suit++)
            {
                for (int rank = 1; rank <= 13; rank++)
                {
                    this.discardPile.Add(new PlayingCard((PlayingCard.Suit)suit, rank));
                }
            }
            int jokerCount = 2;
            for(int i = 0; i < jokerCount; i++)
            {
                this.discardPile.Add(new PlayingCard(PlayingCard.Suit.Joker, 0));
            }
            this.ShuffleDiscardIntoDraw();
        }

        private void ShuffleDiscardIntoDraw()
        {
            foreach(PlayingCard card in this.discardPile)
            {
                this.drawPile.Enqueue(card);
            }
            this.discardPile.Clear();
            this.ShuffleDrawPile();
        }

        private void ShuffleDrawPile()
        {
            List<PlayingCard> cards = new List<PlayingCard>();
            while (this.drawPile.Count > 0)
            {
                cards.Add(this.drawPile.Dequeue());
            }
            PlayingCard[] cardArray = cards.ToArray();

            Random rand = new Random();
            for (int i = 0; i < cardArray.Length; i++)
            {
                int i1 = rand.Next(cardArray.Length);
                int i2 = rand.Next(cardArray.Length);
                PlayingCard t = cardArray[i1];
                cardArray[i1] = cardArray[i2];
                cardArray[i2] = t;
            }

            foreach(PlayingCard card in cardArray)
            {
                this.drawPile.Enqueue(card);
            }
        }

        public void DrawHand()
        {
            this.jokersDrawn = 0;
            for(int i = 0; i < this.hand.Length; i++)
            {
                this.hand[i] = this.DrawCard();
            }
        }

        private PlayingCard DrawCard()
        {
            List<PlayingCard> jokers = new List<PlayingCard>();
            PlayingCard card = this.DrawCardConsideringJokers(jokers);
            foreach(PlayingCard joker in jokers)
            {
                this.jokersDrawn++;
                this.discardPile.Add(joker);
            }
            this.ShuffleDiscardIntoDraw();
            return card;
        }

        private PlayingCard DrawCardConsideringJokers(List<PlayingCard> jokersList)
        {
            if (this.drawPile.Count > 0)
            {
                PlayingCard card = this.drawPile.Dequeue();
                if (card.suit == PlayingCard.Suit.Joker)
                {
                    jokersList.Add(card);
                    return this.DrawCard();
                }
                else
                {
                    return card;
                }
            } else
            {
                this.ShuffleDiscardIntoDraw();
                return this.DrawCard();
            }
        }

        public void ExchangeCards(bool[] isExchanged)
        {
            this.jokersDrawn = 0;
            for(int i = 0; i < this.hand.Length; i++)
            {
                if (isExchanged[i])
                {
                    this.discardPile.Add(this.hand[i]);
                    this.hand[i] = this.DrawCard();
                }
            }
        }

        public int GetJokersDrawn()
        {
            return this.jokersDrawn;
        }

        public IPokerHand GetFinalHand()
        {
            IPokerHand finalHand = new PokerHand(this.hand);
            return finalHand;
        }

        public void ResetHand()
        {
            throw new NotImplementedException();
        }
    }
}
