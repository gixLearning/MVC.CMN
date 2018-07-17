using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.CMN.Models
{
    public class StaticData
    {

        public static List<FakeGame> fakegames = new List<FakeGame>() {
            new FakeGame() {filename = "helicopter.png", description = "<i>Planet of the Helicopter in Africa</i> is the latest release in the popular 'Planet of...' series, and it does not disappoint! Filled with action from beginning to end, this full-immersion RTS action game..."},
            new FakeGame() {filename = "dubstephero.jpg", description = "Now you can be the musical prodigy of the decade, and with no talent or effort required! Command thousands of exciting soundbites that..."},
            new FakeGame() {filename = "lanoire.jpg", description = "Who is responsible for the gristly murders plaguing the strets of L.A.? Why is everything so Noire? How many cars can you steal while bretending to be a law-abiding officer? Most importantly, DO NOT..."},
            new FakeGame() {filename = "minecraft.png", description = "NEW RELEASE! Now with better, more realistic graphics, more blocks, more critters, and more? Can you uncover the truth of the dreaded 'negative world'? Subscribe now for..."},
            new FakeGame() {filename = "saintsrow.jpg", description = "The Saints are back, and wilder than ever!"}



        };

        public static FakeGame GetGame()
        {
            Random randomPick = new Random();         

            return fakegames[randomPick.Next(0, 5)];
        }







    }
}