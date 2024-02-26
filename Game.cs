using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace prog201_cardgames
{
    internal class Game
    {
        public Deck deck;
        public List<Card> playerHand;

        public Game(string suit1, string suit2)
        {
            deck = new Deck(suit1, suit2);
            playerHand = new List<Card>();
        }

        public void ShowMainMenu()
        {
            Console.Clear();

            Console.WriteLine("Welcome to the Card Game!");
            Console.WriteLine("1. Show Full Deck");
            Console.WriteLine("2. Draw a Card");
            Console.WriteLine("3. Play High Card Low Card");
            Console.WriteLine("4. Play Same or Different");
            Console.WriteLine("5. Exit");

            int choice = GetChoice(1, 5);
            switch (choice)
            {
                case 1:
                    ShowFullDeck();
                    break;
                case 2:
                    DrawCard();
                    break;
                case 3:
                    PlayHighLow();
                    break;
                case 4:
                    PlaySameOrDifferent();
                    break;
                case 5:
                    Exit();
                    break;
            }
        }

        public int GetChoice(int min, int max)
        {
            int choice;
            do
            {
                Console.Write("\nSelect Game: ");
            } 

            while (!int.TryParse(Console.ReadLine(), out choice) || choice < min || choice > max);

            return choice;
        }

        public void ShowFullDeck()
        {
            Console.Clear();

            foreach (var card in deck.Cards)
            {
                Console.WriteLine($"{card.Name} {card.Art}");
            }

            Console.WriteLine("\nDo you want to shuffle the deck?" +
                "\n[1] YES" +
                "\n[2] NO");

            int response = Convert.ToInt32(Console.ReadLine());

            if (response == 1)
            {
                Console.Clear();
                deck.Shuffle();
                Console.WriteLine("The deck is now shuffled.");
                Console.WriteLine("Press ENTER to see the shuffled deck.");
                ShowFullDeck();
            }

            else
            {
                ShowMainMenu();
            }

        }

        private void DrawCard()
        {
            Console.Clear();
            Console.WriteLine("Do you want to shuffle the deck before drawing a card?" +
                "\n[1] YES" +
                "\n[2] NO");
            int response = Convert.ToInt32(Console.ReadLine());
            if (response == 1)
            {
                Console.Clear();
                deck.Shuffle();
                Console.WriteLine("Deck shuffled successfully.");
            }

            Card drawnCard = deck.DrawCard();
            if (drawnCard != null)
            {
                Console.WriteLine("Press ENTER to draw a card.");
                Console.ReadKey();
                playerHand.Add(drawnCard);
                Console.WriteLine("\nCard drawn successfully.");
                Console.WriteLine("Do you want to flip the card?" +
                    "\n[1] YES" +
                    "\n[2] NO");
                response = Convert.ToInt32(Console.ReadLine());

                if (response == 1)
                {
                    //display ASCII art of the drawn card
                    Console.WriteLine("\n" + drawnCard.Name);
                    Console.WriteLine(drawnCard.Art);
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("No more cards remain.");
                Console.WriteLine("Do you want to start over?" +
                    "\n[1] YES" +
                    "\n[2] NO");
                int response2 = Convert.ToInt32(Console.ReadLine());
                if (response2 == 1)
                {
                    playerHand.Clear();
                    deck.ReturnCardsToDeck();
                    Console.WriteLine("Deck reset successfully.");
                    Console.WriteLine("Press ENTER to continue.");
                    Console.ReadKey();
                    DrawCard();
                }
                else
                {
                    ShowMainMenu();
                }
            }
        }

        public void PlayHighLow()
        {
            Console.Clear();
            Console.WriteLine("     HOW TO PLAY:" +
                "\nBoth you and the computer will draw a card." +
                "\nYou will then guess if the computer's card is either HIGHER or LOWER than yours." +
                "\nIf you're correct, you win!" +
                "\n\nReady to play?" +
                "\n[1] YES" +
                "\n[2] NO");
            int response = Convert.ToInt32(Console.ReadLine());
            if (response == 1)
            {
                Console.Clear();
                deck.Shuffle();
                Card playerCard = deck.DrawCard();
                playerHand.Add(playerCard);
                Card computerCard = deck.DrawCard();

                //print out the values of player's and computer's cards for debugging
                Console.WriteLine($"Your card is: {playerCard.Name} with value {playerCard.Value}");
                Console.WriteLine($"Computer's card is: {computerCard.Name} with value {computerCard.Value}");

                Console.WriteLine("\nIs the computer's card higher or lower?" +
                    "\n[1] HIGHER" +
                    "\n[2] LOWER");
                int guess = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Press ENTER to see the computer's card.");
                Console.ReadKey();
                Console.WriteLine("Computer's card:");
                Console.WriteLine(computerCard.Art);

                //debugging comparison
                int comparison = CompareCards(playerCard, computerCard);
                Console.WriteLine($"Comparison result: {comparison}");

                if (guess == 1)
                    if (playerCard.Value < computerCard.Value)
                        Console.WriteLine("Congratulations! You win!");
                    else
                        Console.WriteLine("Sorry, you lose!");
                else if (guess == 2)
                    if (playerCard.Value > computerCard.Value)
                        Console.WriteLine("Congratulations! You win!");
                    else
                        Console.WriteLine("Sorry, you lose!");
                else
                    Console.WriteLine("\nEnter [1] for HIGHER" +
                        "\nEnter [2] for LOWEER");

                Console.WriteLine("\nDo you want to play again?" +
                    "\n[1] YES" +
                    "\n[2] NO");
                int response2 = Convert.ToInt32(Console.ReadLine());
                if (response2 == 1)
                {
                    playerHand.Clear();
                    deck.ReturnCardsToDeck();
                    PlayHighLow();
                }
                else if (response2 != 1)
                {
                    ShowMainMenu();
                }
            }
            else
            {
                ShowMainMenu();
            }
        }

        public void PlaySameOrDifferent()
        {
            Console.Clear();
            Console.WriteLine("Choose the two suits to play with:");
            Console.WriteLine("1. Fey");
            Console.WriteLine("2. Fiend");
            Console.WriteLine("3. Beast");
            Console.WriteLine("4. Celestial");

            Console.Write("Choose the first suit: ");
            int suitChoice1 = Convert.ToInt32(Console.ReadLine());

            Console.Write("Choose the second suit: ");
            int suitChoice2 = Convert.ToInt32(Console.ReadLine());
                if (suitChoice1 == suitChoice2)
                {
                    Console.WriteLine("You cannot choose the same suit twice.");
                    Console.WriteLine("Press ENTER to try again.");
                    Console.ReadKey();
                    PlaySameOrDifferent();
                }

            //get the names of the chosen suits
            string[] suits = { "", "Fey", "Fiend", "Beast", "Celestial" };
            string Suit1 = suits[suitChoice1];
            string Suit2 = suits[suitChoice2];

            //create a new deck containing only the chosen suits
            deck = new Deck(Suit1, Suit2);
            deck.Shuffle();

            //draw a card for the player
            Console.Clear();
            Console.WriteLine("Press ENTER to draw a card.");
            Console.ReadKey();
            Card playerCard = deck.DrawCard();
            Console.WriteLine("Your card:");
            Console.WriteLine(playerCard.Name);
            Console.WriteLine(playerCard.Art);

            //prompt the player to guess
            Console.WriteLine("\nIs the computer's card of the SAME or DIFFERENT suit than yours?");
            Console.WriteLine("[1] SAME");
            Console.WriteLine("[2] DIFFERENT");
            int guess = GetChoice(1, 2);

            //draw a card for the computer
            Card computerCard = deck.DrawCard();
            Console.WriteLine("\nComputer's card:");
            Console.WriteLine(computerCard.Name);
            Console.WriteLine(computerCard.Art);

            //evaluate the result
            bool isSame = playerCard.Suit == computerCard.Suit;
            bool guessCorrect = (guess == 1 && isSame) || (guess == 2 && !isSame);

            if (guessCorrect)
                Console.WriteLine("Congratulations! You win!");
            else
                Console.WriteLine("Sorry, you lose!");

            Console.WriteLine("\nPlay again?" +
                "\n[1] YES" +
                "\n[2] NO");
            int response = Convert.ToInt32(Console.ReadLine());
            if (response == 1)
            {
                playerHand.Clear();
                deck.ReturnCardsToDeck();
                PlaySameOrDifferent();
            }
            else
            {
                ShowMainMenu();
            }

        }

        //method for debugging high-low game
        //remove after debugging
        public int CompareCards(Card playerCard, Card computerCard)
        {

            //compare based on card values
            if (playerCard.Value > computerCard.Value)
                return 1;
            else if (playerCard.Value < computerCard.Value)
                return -1;
            else
                return 0;

        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
