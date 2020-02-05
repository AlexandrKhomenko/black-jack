using BlackJack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BlackJack
{
    public partial class play : System.Web.UI.Page
    {
        List<Card> dealerCards;
        List<Card> playerCards;
        Deck deck;
        double playerMoney;
        double bet;
        int totalCardsDealer;
        int totalCardsPlayer;
        int playerCardIndex;
        int dealerCardIndex;
        protected void Page_Load(object sender, EventArgs e)
        {
            deck = (Deck)Application["deck"];
            dealerCards = (List<Card>)Application["dealerCards"];
            playerCards = (List<Card>)Application["playerCards"];
            totalCardsDealer = Application["totalCardsDealer"] != null ? (int)Application["totalCardsDealer"] : 0;
            totalCardsPlayer = Application["totalCardsPlayer"] != null ? (int)Application["totalCardsPlayer"] : 0;
            playerCardIndex = Application["playerCardIndex"] != null ? (int)Application["playerCardIndex"] : 1;
            dealerCardIndex = Application["dealerCardIndex"]  != null ? (int)Application["dealerCardIndex"] : 1;
            bet = Application["bet"] != null ? double.Parse(Application["bet"].ToString()) : 0;           
            lblPlayerMoney.Text = Application["playerMoney"].ToString();
            playerMoney = double.Parse(Application["playerMoney"].ToString());
            btnHit.Enabled = false;
            btnStand.Enabled = false;
            

        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            int st;
            if (int.TryParse(txtAmountToBet.Text, out st) && int.Parse(txtAmountToBet.Text) < 101 && int.Parse(txtAmountToBet.Text) > 0)
            {
                
                deck.LoadDeck();
                bet = double.Parse(txtAmountToBet.Text);
                Application["bet"] = bet;

                dealerCards = new List<Card>();
                playerCards = new List<Card>();
                dealerCards.Add(deck.GetCard());
                playerCards.Add(deck.GetCard());
                playerCardIndex = 1;
                Application["playerCardIndex"] = playerCardIndex;
                dealerCards.Add(deck.GetCard());
                playerCards.Add(deck.GetCard());

                Application["dealerCards"] = dealerCards;
                Application["playerCards"] = playerCards;
                Application["totalCardsDealer"] = dealerCards[0].CardValue + dealerCards[1].CardValue;
                AceValuePlayer(playerCards);
                AceValueDealer(dealerCards);

                playerPlace.InnerHtml = playerCards[0].CardImage;
                playerPlace.InnerHtml += playerCards[1].CardImage;
                dealerPlace.InnerHtml = dealerCards[0].CardImage;
                dealerPlace.InnerHtml = dealerCards[1].CardImage;
                dealerPlace.InnerHtml += dealerCards[1].faceDown();

                btnHit.Enabled = true;
                btnStand.Enabled = true;
               

                if (totalCardsPlayer == 21)
                    {
                        playerMoney += (bet * 1.5);
                        Application["playerMoney"] = playerMoney;
                        lblPlayerMoney.Text = playerMoney.ToString();
                        txtAmountToBet.Text = 0.ToString();
                        lblInfo.Text = $"You win! You got {totalCardsPlayer}.";
                    }
                    if (totalCardsDealer == 21)
                    {
                        playerMoney -= (bet * 1.5);
                        Application["playerMoney"] = playerMoney;
                        lblPlayerMoney.Text = playerMoney.ToString();
                        txtAmountToBet.Text = 0.ToString();
                        lblInfo.Text = $"You lose! Dealer got {totalCardsDealer}.";
                    }
                
            }
        }

        private void AceValuePlayer(List<Card> hand)
        {
            int handTotalPlayer = 0;
            totalCardsPlayer = 0;

            foreach (Card card in playerCards)
            {
                handTotalPlayer += card.CardValue;
            }

            foreach (Card card in hand)
            {
                if (card.Number == 1 && (handTotalPlayer + 10) < 22)
                {
                    card.CardValue = 11;
                    totalCardsPlayer += card.CardValue;
                }
                else if (card.CardValue == 11 && (handTotalPlayer - 10) < 22)
                {
                    card.CardValue = 1;
                    totalCardsPlayer += card.CardValue;
                }
                else
                {
                    totalCardsPlayer += card.CardValue;
                }
            }
            Application["totalCardsPlayer"] = totalCardsPlayer;
        }

        private void AceValueDealer(List<Card> hand)
        {
            int handTotalDealer = 0;
            totalCardsDealer = 0;

            foreach (Card card in dealerCards)
            {
                handTotalDealer += card.CardValue;
            }

            foreach (Card card in hand)
            {
                if (card.Number == 1 && (handTotalDealer + 10) < 22)
                {
                    card.CardValue = 11;
                    totalCardsDealer += card.CardValue;
                }
                else if (card.CardValue == 11 && (handTotalDealer - 10) < 22)
                {
                    card.CardValue = 1;
                    totalCardsDealer += card.CardValue;
                }
                else
                {
                    totalCardsDealer += card.CardValue;
                }
            }
            Application["totalCardsDealer"] = totalCardsDealer;
        }

        protected void btnHit_Click(object sender, EventArgs e)
        {
            playerCards.Add(deck.GetCard());
            playerCardIndex++;
            Application["playerCardIndex"] = playerCardIndex;
            playerPlace.InnerHtml += playerCards[playerCardIndex].CardImage;

            AceValuePlayer(playerCards);
            Application["totalCardsPlayer"] = totalCardsPlayer;
            if (totalCardsPlayer > 21)
            {
                lblInfo.Text = "You have more than 21! You lose!";
                txtAmountToBet.Text = 0.ToString();
                playerMoney -= bet;
                Application["playerMoney"] = playerMoney;
                lblPlayerMoney.Text = playerMoney.ToString();
                btnHit.Enabled = false;
                btnStand.Enabled = false;
            }

        }

        private void DealerTurn() 
        {
            if (totalCardsDealer < 17)
            {
                dealerCards.Add(deck.GetCard());
                dealerCardIndex++;
                Application["totalCardsDealer"] = totalCardsDealer;
                dealerPlace.InnerHtml += dealerCards[dealerCardIndex].CardImage;
                AceValueDealer(dealerCards);
                Application["totalCardsDealer"] = totalCardsDealer;
                DealerTurn();
               
            }
        }
        protected void btnStand_Click(object sender, EventArgs e)
        {

            btnHit.Enabled = false;
            btnStand.Enabled = false;

            DealerTurn();
            if (totalCardsDealer > 21)
            {
                playerMoney += bet;
                Application["playerMoney"] = playerMoney;
                lblPlayerMoney.Text = playerMoney.ToString();
                txtAmountToBet.Text = 0.ToString(); 
                lblInfo.Text = $"You win! Dealer got {totalCardsDealer}. You got {totalCardsPlayer}.";
            }
            else if (totalCardsPlayer > totalCardsDealer)
            {
                playerMoney += bet;
                Application["playerMoney"] = playerMoney;
                lblPlayerMoney.Text = playerMoney.ToString();
                txtAmountToBet.Text = 0.ToString(); 
                lblInfo.Text = $"You win! Dealer got {totalCardsDealer}. You got {totalCardsPlayer}.";

            }
            else if (totalCardsDealer > totalCardsPlayer)
            {
                playerMoney -= bet;
                Application["playerMoney"] = playerMoney;
                lblPlayerMoney.Text = playerMoney.ToString();
                txtAmountToBet.Text = 0.ToString(); 
                lblInfo.Text = $"You lose! Dealer got {totalCardsDealer}. You got {totalCardsPlayer}";
            }
            else if (totalCardsDealer == totalCardsPlayer)
            {
                Application["playerMoney"] = playerMoney;
                lblPlayerMoney.Text = playerMoney.ToString();
                txtAmountToBet.Text = 0.ToString(); 
                lblInfo.Text = $"You have the same amount of points as dealer! Dealer got {totalCardsDealer}. You got {totalCardsPlayer}";
            }
        }

        protected void btnRestart_Click(object sender, EventArgs e)
        {
            lblPlayerMoney.Text = 500.ToString();
            Application["playerMoney"] = 500;
            playerMoney = 500;
            txtAmountToBet.Text = 0.ToString();
            btnStart.Enabled = true;
        }
    }
}