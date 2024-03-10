using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog201_cardgames
{
    //i know how to use enums now, yippee :)
    public enum Suit
    {
        Fey,
        Fiend,
        Beast,
        Celestial
    }

    public class Card
    {
        public string Name { get; set; }
        public string Art { get; set; }
        public Suit Suit { get; set; }
        public int Value { get; set; }

        public Card(string name, string art, Suit suit)
        {
            Name = name;
            Art = art;
            Suit = suit;
            //old way of splicing the card name to get the suit
            //Suit = name.Split(' ')[2];
            Value = GetValue();
        }

        public int GetValue()
        {
            //extract face and value from card name
            string[] parts = Name.Split(' ');
            string face = parts[0];
            int value;

            //if the card is a face card, assign corresponding value
            switch (face)
            {
                case "Apprentice":
                    value = 11;
                    break;
                case "Anointed":
                    value = 12;
                    break;
                case "Ascended":
                    value = 13;
                    break;
                case "Avatar":
                    value = 14;
                    break;
                default:
                    //if not a face card, parse the numerical value
                    value = int.Parse(parts[0]);
                    break;
            }

            return value;
        }
    }
}
