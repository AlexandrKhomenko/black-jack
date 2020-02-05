using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackJack.Models
{
   public class Card : IComparable<Card>
    {
        private int cardValue;
        private string cardSuit;                      
        private string cardImage;
        private int number;

        public Card(int number, string cardSuit)
        {
            this.cardSuit = cardSuit;
            this.Number = number;
            GetImage();
        }

        public int CardValue
        {
            get
            {
                return cardValue;
            }

            set
            {
                cardValue = value;
            }
        }

        public string CardImage
        {
            get { return cardImage; }
            set { cardImage = value; }
        }
        public string faceDown() 
        {
            return "<img class='card' src='img/deck/faceDown.png' />";
        }
        public string CardSuit
        {
            get { return cardSuit; }
            set { cardSuit = value; }
        }
        public void GetImage()
        {
            string url = cardValue.ToString();
            switch (CardSuit)
            {
                case "diamonds":
                    url += "D.png";
                    break;
                case "hearts":
                    url += "H.png";
                    break;
                case "clubs":
                    url += "C.png";
                    break;
                case "spades":
                    url += "S.png";
                    break;
            }
            url = "img/deck/" + url;

            CardImage = $"<img class='card' src='{url}' />";
        }
    
        public int Number
        {
            get { return number; }
            set
            {
                number = value;
                if (number > 10)
                {
                    cardValue = 10;
                }
                else
                {
                    cardValue = number;
                }

            }
        }

        public int CompareTo(Card otherCard)
        {
            return this.Number.CompareTo(otherCard.Number);
        }
    }
}