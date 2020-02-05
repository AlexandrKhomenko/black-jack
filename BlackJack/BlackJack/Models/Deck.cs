using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackJack.Models
{
    public class Deck
    {
        private Card[] cards;
        private const int SIZE_OF_SUIT = 13;
        private const int SIZE_OF_DECK = 52;
        private int cardIndex;


        public int getIndex()
        {
            return cardIndex;
        }

       

        public void LoadDeck()
        {
            cards = new Card[SIZE_OF_DECK];
            string[] allSuits = { "hearts", "spades", "diamonds", "clubs" };
            cardIndex = 0;
            int numberIndex = 1;
            int suitIndex = 0;

            for (int i = 0; i < SIZE_OF_DECK; i++)
            {
                if (numberIndex > SIZE_OF_SUIT)
                {
                    suitIndex++;
                    numberIndex = 1;
                }
                cards[i] = new Card(numberIndex, allSuits[suitIndex]);
                numberIndex++;
            }
            Shuffle();
        }
        public void Shuffle()
        {
            Random random = new Random();
            for (int i = 0; i < cards.Length; i++)
            {
                int randomIndex = random.Next(52);
                Card temp = cards[i];
                cards[i] = cards[randomIndex];
                cards[randomIndex] = temp;
            }
        }

        public Deck()
        {
            LoadDeck();
        }

        public Card GetCard()
        {
            if (cardIndex < cards.Length)
                return cards[++cardIndex];
            else
                throw new IndexOutOfRangeException("The deck is empty");
        }
    }
}