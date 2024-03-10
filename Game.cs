using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace prog201_cardgames
{
    internal class Game
    {
        //public Deck deck;
        public List<Card> playerHand;
        public List<Card> computerHand;
        public List<Card> discardPile;

        public Game(string suit1, string suit2)
        {
            //deck = new Deck(suit1, suit2);
            playerHand = new List<Card>();
            computerHand = new List<Card>();
            discardPile = new List<Card>();
        }

        public void ShowMainMenu()
        {
            Console.Clear();

            Console.WriteLine("Welcome to the Card Game!");
            Console.WriteLine("1. Show Full Deck");
            Console.WriteLine("2. Draw a Card");
            Console.WriteLine("3. Play High Card Low Card");
            Console.WriteLine("4. Play Same or Different");
            Console.WriteLine("5. Play Highest Match");
            Console.WriteLine("6. Exit");

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
                    PlayHighestMatch();
                    break;

                case 6:
                    Exit();
                    break;
            }
        }

        public int GetChoice(int min, int max)
        {
            int choice;
            do
            {
                Console.Write("\nSelection: ");
            } 

            while (!int.TryParse(Console.ReadLine(), out choice) || choice < min || choice > max);

            return choice;
        }

        public void ShowFullDeck()
        {
            Console.Clear();
            Deck deck;
            deck = new Deck("", "");
            deck.InitializeDeck();

            foreach (var card in deck.Cards)
            {
                Console.WriteLine($"{card.Name} {card.Art}");
                //Console.WriteLine(card.Name);
                //Console.WriteLine(card.Art);
                //Console.WriteLine();
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
            Deck deck;
            deck = new Deck("", "");
            deck.InitializeDeck();

            bool isShuffled = false;

            if (!isShuffled)
            {
                Console.WriteLine("Do you want to shuffle the deck before drawing a card?" +
                     "\n[1] YES" +
                     "\n[2] NO");
                int input = Convert.ToInt32(Console.ReadLine());
                if (input == 1)
                {
                    Console.Clear();
                    deck.Shuffle();
                    isShuffled = true;
                    Console.WriteLine("The deck has been shuffled.");
                }
            }
            //Console.WriteLine("Do you want to shuffle the deck before drawing a card?" +
            //    "\n[1] YES" +
            //    "\n[2] NO");
            //int input = Convert.ToInt32(Console.ReadLine());
            //if (input == 1)
            //{
            //    Console.Clear();
            //    deck.Shuffle();
            //    isShuffled = true;
            //    Console.WriteLine("The deck has been shuffled.");
            //}

            Card drawnCard = deck.DrawCard();

            if (isShuffled)
            {


                if (drawnCard != null)
                {
                    Console.WriteLine("Press ENTER to draw a card.");
                    Console.ReadKey();
                    playerHand.Add(drawnCard);
                    Console.WriteLine("\nYou have drawn a card.");
                    Console.WriteLine("Do you want to flip the card?" +
                        "\n[1] YES" +
                        "\n[2] NO");
                    int input = Convert.ToInt32(Console.ReadLine());

                    if (input == 1)
                    {
                        //display ASCII art of the drawn card
                        Console.WriteLine("\n" + drawnCard.Name);
                        Console.WriteLine(drawnCard.Art);
                    }

                    Console.WriteLine("\nDo you want to draw another card?" +
                        "\n[1] YES" +
                        "\n[2] NO");
                    input = Convert.ToInt32(Console.ReadLine());
                    if (input == 1)
                    {
                        DrawCard();
                    }
                    else
                    {
                        ShowMainMenu();
                    }

                }
                else
                {
                    Console.WriteLine("No more cards remain.");
                    Console.WriteLine("Do you want to start over?" +
                        "\n[1] YES" +
                        "\n[2] NO");
                    int input2 = Convert.ToInt32(Console.ReadLine());
                    if (input2 == 1)
                    {
                        playerHand.Clear();
                        deck.ReturnCardsToDeck();
                        Console.WriteLine("The deck has been reassembled.");
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

            else
            {
                if (drawnCard != null)
                {
                    Console.WriteLine("Press ENTER to draw a card.");
                    Console.ReadKey();
                    playerHand.Add(drawnCard);
                    Console.WriteLine("\nYou have drawn a card.");
                    Console.WriteLine("Do you want to flip the card?" +
                        "\n[1] YES" +
                        "\n[2] NO");
                    int input = Convert.ToInt32(Console.ReadLine());

                    if (input == 1)
                    {
                        //display ASCII art of the drawn card
                        Console.WriteLine("\n" + drawnCard.Name);
                        Console.WriteLine(drawnCard.Art);
                    }

                    Console.WriteLine("\nDo you want to draw another card?" +
                        "\n[1] YES" +
                        "\n[2] NO");
                    input = Convert.ToInt32(Console.ReadLine());
                    if (input == 1)
                    {
                        DrawCard();
                    }
                    else
                    {
                        ShowMainMenu();
                    }

                }
                else
                {
                    Console.WriteLine("No more cards remain.");
                    Console.WriteLine("Do you want to start over?" +
                        "\n[1] YES" +
                        "\n[2] NO");
                    int input2 = Convert.ToInt32(Console.ReadLine());
                    if (input2 == 1)
                    {
                        playerHand.Clear();
                        deck.ReturnCardsToDeck();
                        Console.WriteLine("The deck has been reassembled.");
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

        }

        public void PlayHighLow()
        {
            Console.Clear();
            Deck deck;
            deck = new Deck("", "");
            deck.InitializeDeck();

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
            Deck halfDeck;

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
            string[] suits = { "Fey", "Fiend", "Beast", "Celestial" };
            string Suit1 = suits[suitChoice1];
            string Suit2 = suits[suitChoice2];

            //create a new deck containing only the chosen suits
            halfDeck = new Deck(Suit1, Suit2);
            halfDeck.Shuffle();

            //draw a card for the player
            Console.Clear();
            Console.WriteLine("Press ENTER to draw a card.");
            Console.ReadKey();
            Card playerCard = halfDeck.DrawCard();
            Console.WriteLine("Your card:");
            Console.WriteLine(playerCard.Name);
            Console.WriteLine(playerCard.Art);

            //prompt the player to guess
            Console.WriteLine("\nIs the computer's card of the SAME or DIFFERENT suit than yours?");
            Console.WriteLine("[1] SAME");
            Console.WriteLine("[2] DIFFERENT");
            int guess = GetChoice(1, 2);

            //draw a card for the computer
            Card computerCard = halfDeck.DrawCard();
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
                halfDeck.ReturnCardsToDeck();
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

        public void PlayHighestMatch()
        {
            Console.Clear();

            Console.WriteLine("     HOW TO PLAY:" +
               "\nBoth you and the computer will be dealt 4 cards." +
               "\nThe goal is to collect four cards of the same suit with the highest possible values." +
               "\nThe player with the most matching suits wins." +
               "\nIf both players have the same number of matching suits," +
               "\n     the player with the highest score from their matching suits wins." +
               "\nIf neither player has a majority suit," +
               "\n     the player with the highest score wins.");
            Console.WriteLine("Press ENTER to start.");
            Console.ReadKey();
            Console.Clear();

            Deck matchDeck = new Deck("", "");
            matchDeck.InitializeDeck();
            matchDeck.Shuffle();
            int round = 0;
            bool playerTurn = true;

            //flip a coin to determine who goes first
            Console.WriteLine("First, let's flip a coin to see who goes first.");
            Console.WriteLine("Heads for player, tails for computer.");
            Console.WriteLine("Press ENTER to flip the coin.");
            Console.ReadKey();
            Console.WriteLine("Flipping the coin...");
            Console.ReadKey();
            bool coinToss = FlipCoin();
            Console.WriteLine("The coin landed on " + (coinToss ? "heads." : "tails."));
            if (coinToss) Console.WriteLine("You will go first.");
            else Console.WriteLine("The computer goes first.");
            Console.WriteLine("Press ENTER to continue.");
            Console.ReadKey();

            if (coinToss)
                playerTurn = true;
            else
                playerTurn = false;

            List<Card> playerHand = new List<Card>();
            List<Card> computerHand = new List<Card>();
            List<Card> discardPile = new List<Card>();

            //deal 4 cards to each player
            for (int i = 0; i < 8; i++)
            {
                if (playerTurn)
                    playerHand.Add(matchDeck.DrawCard());
                else
                    computerHand.Add(matchDeck.DrawCard());

                playerTurn = !playerTurn;
            }

            //place a card from the deck into the discard pile
            discardPile.Add(matchDeck.DrawCard());
            Console.WriteLine($"Top card of discard pile: {discardPile[0].Name}");

            //game loop for 10 rounds
            while (round <= 10)
            {
                Console.WriteLine($"\n[ROUND {round}]\n");

                if (playerTurn)
                {
                    Console.Clear();
                    Console.WriteLine("[PLAYER TURN]");
                    Console.WriteLine("The card on top of the discard pile is: " + discardPile[0].Name);
                    HandlePlayerTurn(matchDeck, playerHand, discardPile);

                }
                else
                {

                    Console.WriteLine("[COMPUTER TURN]");
                    Console.WriteLine("The card on top of the discard pile is: " + discardPile[0].Name);
                    HandleComputerTurn(matchDeck, computerHand, ref discardPile);
                    Console.WriteLine("Press ENTER to continue.");
                    Console.ReadKey();
                }

                playerTurn = !playerTurn;
                round++;
            }

            DetermineWinner(playerHand, computerHand);

            Console.WriteLine("\nDo you want to play again?");
            Console.WriteLine("[1] YES");
            Console.WriteLine("[2] NO");
            int response = GetChoice(1, 2);

            if (response == 1)
                PlayHighestMatch();
            else
                ShowMainMenu();
        }

        public bool FlipCoin()
        {
            Random random = new Random();
            //0 for player, 1 for computer
            return random.Next(2) == 0; 
        }

        public void HandlePlayerTurn(Deck matchDeck, List<Card> playerHand, List<Card> discardPile)
        {

            Console.WriteLine("Your hand:\n");
            ShowHand(playerHand);

            Console.WriteLine("Options:");
            Console.WriteLine("[1] Swap a card with the discard pile");
            Console.WriteLine("[2] Draw a card from the deck");

            int choice = GetChoice(1, 2);
            if (choice == 1)
            {

                if (discardPile.Count > 0)
                {
                    Console.Clear();
                    Console.WriteLine($"You add the {discardPile[0].Name} to your hand.");

                    //add the top card of the discard pile to the player's hand
                    playerHand.Add(discardPile[0]);

                    Console.WriteLine("Choose a card to discard (1-5):");
                    Console.WriteLine("Your hand:");
                    ShowHand(playerHand);

                    int discardedIndex = GetChoice(1, 5) - 1;
                    Card discardedCard = playerHand[discardedIndex];

                    //remove the discarded card from the player's hand
                    playerHand.RemoveAt(discardedIndex);

                    //put the discarded card onto the discard pile
                    discardPile.Insert(0, discardedCard);

                    Console.WriteLine($"You discarded the {discardedCard.Name}");
                    Console.WriteLine("Press ENTER to continue.");
                    Console.ReadKey();


                }
                else
                {
                    Console.WriteLine("The discard pile is empty. You must draw a card from the deck.");
                    HandlePlayerTurn(matchDeck, playerHand, discardPile);
                }
            }
            else if (choice == 2)
            {
                Console.Clear();

                //draw a card from the deck and discard a card
                Card drawnCard = matchDeck.DrawCard();
                playerHand.Add(drawnCard);

                Console.WriteLine($"You drew the {drawnCard.Name}");                
                Console.WriteLine("Your hand:");

                ShowHand(playerHand);

                Console.WriteLine("Choose a card to discard (1-5):");
                int discardedIndex = GetChoice(1, playerHand.Count) - 1;
                Card discardedCard = playerHand[discardedIndex];

                //remove the discarded card from the player's hand
                playerHand.RemoveAt(discardedIndex);

                //put the discarded card onto the discard pile
                discardPile.Insert(0, discardedCard);

                Console.WriteLine($"You discarded the {discardedCard.Name}");
                Console.WriteLine("Press ENTER to continue.");
                Console.ReadKey();

            }

        }

        public void HandleComputerTurn(Deck deck, List<Card> computerHand, ref List<Card> discardPile)
        {

            //check if there is a majority suit in the computer's hand
            var suitGroups = computerHand.GroupBy(card => card.Suit);
            var majoritySuitGroup = suitGroups.OrderByDescending(group => group.Count()).FirstOrDefault();

            //if there's a majority suit and it matches the card on top of the discard pile, add that card to the hand
            if (majoritySuitGroup.Key == discardPile[0].Suit)
            {
                computerHand.Add(discardPile[0]);
                Console.WriteLine($"Computer takes {discardPile[0].Name} from discard pile.");

                //check if there is a majority suit in the computer's hand after drawing
                var newSuitGroups = computerHand.GroupBy(card => card.Suit);
                var newMajoritySuitGroup = newSuitGroups.OrderByDescending(group => group.Count()).FirstOrDefault();

                //if there's a new majority suit, discard any card that doesn't match it
                if (newMajoritySuitGroup != null && newMajoritySuitGroup.Key != discardPile[0].Suit)
                {
                    var cardToDiscard = computerHand.FirstOrDefault(card => card.Suit != newMajoritySuitGroup.Key);
                    if (cardToDiscard != null)
                    {
                        //remove the discarded card from the computer's hand
                        computerHand.Remove(cardToDiscard);

                        //put the discarded card onto the discard pile
                        discardPile.Insert(0, cardToDiscard);

                        Console.WriteLine($"Computer discards the {cardToDiscard.Name}.");
                    }
                }
                else
                {
                    //if all cards are of the same suit, discard the lowest value card
                    if (newMajoritySuitGroup != null)
                    {
                        var lowestValueCard = computerHand.OrderBy(card => card.Value).First();

                        //remove the discarded card from the computer's hand
                        computerHand.Remove(lowestValueCard);

                        //put the discarded card onto the discard pile
                        discardPile.Insert(0, lowestValueCard);

                        Console.WriteLine($"Computer discards the {lowestValueCard.Name}.");
                    }
                }

            }
            else
            {
                //if there's no majority suit or it doesn't match the top card of the discard pile, draw a card
                Card drawnCard = deck.DrawCard();
                computerHand.Add(drawnCard);
                Console.WriteLine($"Computer draws a card from the deck.");

                //check if there is a majority suit in the computer's hand after drawing
                var newSuitGroups = computerHand.GroupBy(card => card.Suit);
                var newMajoritySuitGroup = newSuitGroups.OrderByDescending(group => group.Count()).FirstOrDefault();

                //if there's a new majority suit, discard any card that doesn't match it
                if (newMajoritySuitGroup != null && newMajoritySuitGroup.Key != discardPile[0].Suit)
                {
                    var cardToDiscard = computerHand.FirstOrDefault(card => card.Suit != newMajoritySuitGroup.Key);
                    if (cardToDiscard != null)
                    {

                        // remove the discarded card from the computer's hand
                        computerHand.Remove(cardToDiscard);

                        //put the discarded card onto the discard pile
                        discardPile.Insert(0, cardToDiscard);

                        Console.WriteLine($"Computer discards the {cardToDiscard.Name}.");
                    }
                }
                else
                {
                    //if all cards are of the same suit, discard the lowest value card
                    if (newMajoritySuitGroup != null)
                    {
                        var lowestValueCard = computerHand.OrderBy(card => card.Value).First();
                        
                        //remove the discarded card from the computer's hand
                        computerHand.Remove(lowestValueCard);

                        //put the discarded card onto the discard pile
                        discardPile.Insert(0, lowestValueCard);

                        Console.WriteLine($"Computer discards the {lowestValueCard.Name}.");
                    }
                }
            }

            Console.WriteLine("Press ENTER to continue.");

        }

        private void DetermineWinner(List<Card> playerHand, List<Card> computerHand)
        {
            //count the number of cards of each suit for both players
            Dictionary<Suit, int> playerSuitCounts = CountLikeSuits(playerHand);
            Dictionary<Suit, int> computerSuitCounts = CountLikeSuits(computerHand);

            //determine the highest count of cards of a single suit for each player
            int playerMaxSuitCount = playerSuitCounts.Values.Max();
            int computerMaxSuitCount = computerSuitCounts.Values.Max();

            //compare the highest count of cards of a single suit for each player
            if (playerMaxSuitCount > computerMaxSuitCount)
            {
                Console.WriteLine("The player had " + playerMaxSuitCount + " like-suit cards.");
                Console.WriteLine("The computer had " + computerMaxSuitCount + " like-suit cards.");
                Console.WriteLine("Player wins with the most matching suits.");
                ShowHand(playerHand);
            }
            else if (playerMaxSuitCount < computerMaxSuitCount)
            {
                Console.WriteLine("The player had " + playerMaxSuitCount + " like-suit cards.");
                Console.WriteLine("The computer had " + computerMaxSuitCount + " like-suit cards.");
                Console.WriteLine("Computer wins with the most matching suits.");
                ShowHand(computerHand);
            }
            else
            {
                //if both players have the same count of like-suits, calculate the score based on the value of like-suit cards
                int playerScore = CalculateScore(playerHand);
                int computerScore = CalculateScore(computerHand);

                //first handle if the count of like-suit cards is 0 for both
                //if both players have 0 like-suit cards, determine the winner based on the single highest value card
                if (playerScore == 0 && computerScore == 0)
                {
                    int playerHighestValue = playerHand.Max(card => card.Value);
                    int computerHighestValue = computerHand.Max(card => card.Value);

                    if (playerHighestValue > computerHighestValue)
                    {
                        Console.WriteLine("The player's highest value card is the " + playerHand.First(card => card.Value == playerHighestValue).Name);
                        Console.WriteLine("The computer's highest value card is the " + computerHand.First(card => card.Value == computerHighestValue).Name);
                        Console.WriteLine("Player wins with the highest value card.");
                        ShowHand(playerHand);
                    }
                    else if (playerHighestValue < computerHighestValue)
                    {
                        Console.WriteLine("The player's highest value card is the " + playerHand.First(card => card.Value == playerHighestValue).Name);
                        Console.WriteLine("The computer's highest value card is the " + computerHand.First(card => card.Value == computerHighestValue).Name);
                        Console.WriteLine("Computer wins with the highest value card.");
                        ShowHand(computerHand);
                    }
                    else
                    {
                        Console.WriteLine("The player's highest value card is the " + playerHand.First(card => card.Value == playerHighestValue).Name);
                        Console.WriteLine("The computer's highest value card is the " + computerHand.First(card => card.Value == computerHighestValue).Name);
                        Console.WriteLine("It's a tie!");
                        ShowHand(playerHand);
                        ShowHand(computerHand);
                    }
                }

                //if both players have like-suit cards, compare the scores
                else if (playerScore > computerScore)
                {
                    Console.WriteLine("PLAYER SCORE: " + playerScore);
                    Console.WriteLine("COMPUTER SCORE: " + computerScore);
                    Console.WriteLine("Player wins!");
                    ShowHand(playerHand);
                }
                else if (playerScore < computerScore)
                {
                    Console.WriteLine("PLAYER SCORE: " + playerScore);
                    Console.WriteLine("COMPUTER SCORE: " + computerScore);
                    Console.WriteLine("Computer wins!");
                    ShowHand(computerHand);
                }
                else
                {
                    Console.WriteLine("PLAYER SCORE: " + playerScore);
                    Console.WriteLine("COMPUTER SCORE: " + computerScore);
                    Console.WriteLine("Both players have the same score.");
                    Console.WriteLine("It's a tie!");
                    ShowHand(playerHand);
                    ShowHand(computerHand);
                }
            }
        }

        private Dictionary<Suit, int> CountLikeSuits(List<Card> hand)
        {
            //count the number of cards of each suit
            Dictionary<Suit, int> suitCounts = new Dictionary<Suit, int>();

            foreach (Card card in hand)
            {
                if (suitCounts.ContainsKey(card.Suit))
                {
                    suitCounts[card.Suit]++;
                }
                else
                {
                    suitCounts[card.Suit] = 1;
                }
            }

            //return the counts of like-suit cards
            return suitCounts;
        }

        private int CalculateScore(List<Card> hand)
        {
            //calculate the score based on the value of like-suit cards
            int score = 0;
            Dictionary<Suit, int> suitCounts = CountLikeSuits(hand);

            foreach (var suitCount in suitCounts)
            {
                //only count like-suit cards
                if (suitCount.Value > 1) 
                {
                    int suitScore = hand.Where(card => card.Suit == suitCount.Key).Sum(card => card.Value);
                    score += suitScore;
                }
            }

            return score;
        }


        //old method for determining the winner

        //private void DetermineWinner(List<Card> playerHand, List<Card> computerHand)
        //{
        //    //check if either player has a majority suit
        //    var playerSuitGroups = playerHand.GroupBy(card => card.Suit);
        //    var computerSuitGroups = computerHand.GroupBy(card => card.Suit);

        //    var playerMajoritySuitGroup = playerSuitGroups.OrderByDescending(group => group.Count()).FirstOrDefault();
        //    var computerMajoritySuitGroup = computerSuitGroups.OrderByDescending(group => group.Count()).FirstOrDefault();

        //    //if both players have a majority suit, compare the count of matching suits
        //    if (playerMajoritySuitGroup != null && computerMajoritySuitGroup != null)
        //    {
        //        if (playerMajoritySuitGroup.Count() > computerMajoritySuitGroup.Count())
        //        {
        //            Console.WriteLine("Player wins with the highest amount of matching suits.");
        //            ShowHand(playerHand);
        //        }
        //        else if (playerMajoritySuitGroup.Count() < computerMajoritySuitGroup.Count())
        //        {
        //            Console.WriteLine("Computer wins with the highest amount of matching suits.");
        //            ShowHand(computerHand);
        //        }
        //        else
        //        {
        //            //both players have the same amount of matching suits
        //            //add the values of matching suits
        //            int playerScore = playerHand.Where(card => card.Suit == playerMajoritySuitGroup.Key).Sum(card => card.Value);
        //            int computerScore = computerHand.Where(card => card.Suit == computerMajoritySuitGroup.Key).Sum(card => card.Value);

        //            if (playerScore > computerScore)
        //            {
        //                Console.WriteLine("PLAYER SCORE: " + playerScore);
        //                Console.WriteLine("COMPUTER SCORE: " + computerScore);
        //                Console.WriteLine("Player wins with the highest score of matching suits.");
        //                ShowHand(playerHand);
        //            }
        //            else if (playerScore < computerScore)
        //            {
        //                Console.WriteLine("PLAYER SCORE: " + playerScore);
        //                Console.WriteLine("COMPUTER SCORE: " + computerScore);
        //                Console.WriteLine("Computer wins with the highest score of matching suits.");
        //                ShowHand(computerHand);
        //            }
        //            else
        //            {
        //                Console.WriteLine("PLAYER SCORE: " + playerScore);
        //                Console.WriteLine("COMPUTER SCORE: " + computerScore);
        //                Console.WriteLine("Both players have the same score of matching suits.");
        //                Console.WriteLine("It's a tie!");
        //                ShowHand(playerHand);
        //                ShowHand(computerHand);
        //            }
        //        }
        //    }
        //    else if (playerMajoritySuitGroup != null && computerMajoritySuitGroup == null)
        //    {
        //        Console.WriteLine("The player had " + playerSuitGroups.Count() + playerMajoritySuitGroup.Key + "s.");
        //        Console.WriteLine("Player wins with the highest amount of matching suits.");
        //    }
        //    else if (playerMajoritySuitGroup == null && computerMajoritySuitGroup != null)
        //    {
        //        Console.WriteLine("The computer had " + computerSuitGroups.Count() + computerMajoritySuitGroup.Key + "s.");
        //        Console.WriteLine("Computer wins with the highest amount of matching suits.");
        //    }
        //    else
        //    {
        //        //if neither player has a majority suit, compare the value of the highest card
        //        int playerHighestValue = playerHand.Max(card => card.Value);
        //        int computerHighestValue = computerHand.Max(card => card.Value);

        //        if (playerHighestValue > computerHighestValue)
        //        {
        //            Console.WriteLine("The player's highest value card is: " + playerHighestValue);
        //            Console.WriteLine("The computer's highest value card is: " + computerHighestValue);
        //            Console.WriteLine("Player wins with the highest value card.");
        //            ShowHand(playerHand);
        //            ShowHand(computerHand);
        //        }
        //        else if (playerHighestValue < computerHighestValue)
        //        {
        //            Console.WriteLine("The player's highest value card is: " + playerHighestValue);
        //            Console.WriteLine("The computer's highest value card is: " + computerHighestValue);
        //            Console.WriteLine("Computer wins with the highest value card.");
        //            ShowHand(playerHand);
        //            ShowHand(computerHand);
        //        }
        //        else
        //        {
        //            Console.WriteLine("The player's highest value card is: " + playerHighestValue);
        //            Console.WriteLine("The computer's highest value card is: " + computerHighestValue);
        //            Console.WriteLine("It's a tie!");
        //            ShowHand(playerHand);
        //            ShowHand(computerHand);
        //        }
        //    }
        //}

        private void ShowHand(List<Card> playerHand)
        {

            foreach (var card in playerHand)
            {
                Console.WriteLine($"{card.Name} {card.Art}");
            }

        }


        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
