using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace prog201_cardgames
{
    public class Deck
    {
        private List<Card> cards;
        private List<Card> drawnCards;
        private Random random;

        public List<Card> Cards { get { return cards; } }

        public Deck(string suit1, string suit2)
        {
            cards = new List<Card>();
            drawnCards = new List<Card>();
            random = new Random();

            //card art was not displaying correctly due to deck initialization in the constructor
            //because of the use of both a full deck and a half deck, program would skip lines of the ASCII art
            //tutor session with Mack Pearson-Muggli helped resolve this issue, and card art now displays correctly
            //kept for reference
            //InitializeDeck();
            //InitializeHalfDeck(suit1, suit2);
        }

        public void InitializeDeck()
        {
            cards = new List<Card>();
            drawnCards = new List<Card>();

            string[] suits = { "Fey", "Fiend", "Beast", "Celestial" };
            string[] values = { "2 ", "3 ", "4 ",  "5 ", "6 ", "7 ", "8 ", "9 ", "10" };
            string[] faces = { "Apprentice", "Anointed", "Ascended", "Avatar" };

            foreach (string suit in suits)
            {
                foreach (string value in values)
                {
                    string name = $"{value} of {suit}\n";
                    string art = GetCardArt(value, suit, "");
                    cards.Add(new Card(name, art, (Suit)Enum.Parse(typeof(Suit), suit)));
                }

                foreach (string face in faces)
                {
                    string faceName = $"{face} of {suit} \n";
                    string faceArt = GetCardArt("", suit, face);
                    cards.Add(new Card(faceName, faceArt, (Suit)Enum.Parse(typeof(Suit), suit)));
                }
            }
        }

        public void InitializeHalfDeck(string suit1, string suit2)
        {
            cards.Clear();
            drawnCards.Clear();

            //suit1 = Game.PlaySameOrDifferent.suitChoice1;
            //suit2 = Game.PlaySameOrDifferent.suitChoice2;
            string[] values = { "2 ", "3 ", "4 ", "5 ", "6 ", "7 ", "8 ", "9 ", "10" };
            string[] faces = { "Apprentice", "Anointed", "Ascended", "Avatar" };

            foreach (string suit in new List<string> { suit1, suit2 })
            {
                foreach (string value in values)
                {
                    string name = $"{value} of {suit}\n";
                    string art = GetCardArt(value, suit, "");
                    cards.Add(new Card(name, art, (Suit)Enum.Parse(typeof(Suit), suit)));
                }

                foreach (string face in faces)
                {
                    string faceName = $"{face} of {suit}\n";
                    string faceArt = GetCardArt("", suit, face);
                    cards.Add(new Card(faceName, faceArt, (Suit)Enum.Parse(typeof(Suit), suit)));
                }
            }
        }

        public string GetCardArt(string value, string suit, string face)
        {
            string cardArt = "";

            //ASCII art for the card value
            if (value.Length == 2)
            {
                cardArt += "_________\n";
                cardArt += $"| {value}      |\n";
            }
            else
            {
                cardArt += "_________\n";
            }

            //ASCII art for the face card
            if (face != "")
            {
                string faceArt = "";
                if (face == "Apprentice")
                    faceArt = $"| aa      |\n";
                else if (face == "Anointed")
                    faceArt = $"| aA      |\n";
                else if (face == "Ascended")
                    faceArt = $"| Aa      |\n";
                else if (face == "Avatar")
                    faceArt = $"| AA      |\n";

                //append faceArt to cardArt
                cardArt += faceArt;
            }

            //ASCII art for the suit symbol
            switch (suit)
            {
                case "Fey":
                    cardArt += "|         |\n";
                    cardArt += "|    Æ    |\n";
                    cardArt += "|   ÆÆÆ   |\n";
                    cardArt += "|         |\n";
                    break;
                case "Fiend":
                    cardArt += "|         |\n";
                    cardArt += "|    ƒ    |\n";
                    cardArt += "|    ƒ    |\n";
                    cardArt += "|         |\n";
                    break;
                case "Beast":
                    cardArt += "|         |\n";
                    cardArt += "|    ß    |\n";
                    cardArt += "|    ß    |\n";
                    cardArt += "|         |\n";
                    break;
                case "Celestial":
                    cardArt += "|         |\n";
                    cardArt += "|    Ç    |\n";
                    cardArt += "|   ÇÇÇ   |\n";
                    cardArt += "|         |\n";
                    break;
            }

            cardArt += "|_________|\n";

            return cardArt;
        }

        //Fisher-Yates Shuffle from Prog201 class demo
        public void Shuffle()
        {
            int n = cards.Count;
            for (int i = 0; i < (n - 1); i++)
            {
                int r = i + random.Next(n - i);
                Card temp = cards[r];
                cards[r] = cards[i];
                cards[i] = temp;
            }
        }

        public Card DrawCard()
        {
            if (cards.Count == 0)
                return null;

            Card drawnCard = cards[0];
            drawnCards.Add(drawnCard);
            cards.RemoveAt(0);
            return drawnCard;
        }

        public void ReturnCardsToDeck()
        {
            cards.AddRange(drawnCards);
            drawnCards.Clear();
        }
    }
}
