using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

/*
 * Card Games
 * Oliver Hustis, 3/10/2024
 * Columbia College Chicago
 * Credits:
 * Fisher-Yates Shuffle from Prog201 class demo
 * Rework of Deck initialization from Mack Pearson-Muggli - tutor session on 3/7/2024
 * ChatGBT to spot-check errors
*/

namespace prog201_cardgames
{
    internal class Program
    {

    static void Main(string[] args)
        {
            Game game = new Game("", "");
            while (true)
            {
                game.ShowMainMenu();
            }
        }
    }
}
