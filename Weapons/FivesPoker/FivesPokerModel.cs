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

        public FivesPokerModel()
        {
            this.drawPile = new Queue<PlayingCard>();
            this.discardPile = new List<PlayingCard>();
            this.InitCards();
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
            throw new NotImplementedException();
        }

        public bool[] ExchangeCards(bool[] isExchanged)
        {
            throw new NotImplementedException();
        }

        public int GetJokersDrawn()
        {
            throw new NotImplementedException();
        }
    }
}
