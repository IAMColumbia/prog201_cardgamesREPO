using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog201_cardgames
{
    internal class Game
    {
        public Deck deck;
        public List<Card> playerHand;

        public Game()
        {
            deck = new Deck();
            playerHand = new List<Card>();
        }

        public void ShowMainMenu()
        {
            Console.Clear();

            Console.WriteLine("Welcome to the Card Game!");
            Console.WriteLine("1. Show Full Deck");
            Console.WriteLine("2. Draw a Card");
            Console.WriteLine("3. Play High Card Low Card");
            Console.WriteLine("4. Exit");

            int choice = GetChoice(1, 4);
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
            Console.WriteLine("Exiting game. Returning all cards to the deck.");
            playerHand.Clear();
            deck.ReturnCardsToDeck();
        }
    }
}
