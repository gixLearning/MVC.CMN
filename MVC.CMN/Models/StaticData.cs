using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.CMN.Models
{
    public class StaticData
    {

        public static List<Newsitem> NewsBase = new List<Newsitem> {
        new Newsitem {Id = 1, Title = "The hottest titles this summer!", Content = "Blah blah blah", Author = "Jonatan Streith", Datestamp = "18-06-29, 10:22", Colorstyle = "success"},
        new Newsitem {Id = 2, Title = "Ball-shaped blocks? Minecraft's latest development will outrage the fans!", Content = "Es ist unser in die luft, und bald kommt die bären und löwen.", Author = "Jonatan Streith", Datestamp = "18-06-28, 13:12", Colorstyle = "danger"},
        new Newsitem {Id = 3, Title = "Is Pong coming back?", Content = "My hoovercraft is full of eels.", Author = "Jonatan Streith", Datestamp = "18-06-27, 19:56", Colorstyle = "info"},
        new Newsitem {Id = 4, Title = "EA refuses comments on allegations of insider trading, cannibalism", Content = "Ani-san no Raoul no sakana wo misete.", Author = "Jonatan Streith", Datestamp = "18-06-26, 10:35", Colorstyle = "warning"},
        new Newsitem {Id = 5, Title = "Latest CoD release revealed: more dakka!", Content = "Sodomy non sapiens.", Author = "Jonatan Streith", Datestamp = "18-06-20, 4:31", Colorstyle = "primary"}
        };


        public static Threaditem thread1 = new Threaditem()
        {
            Id = 1,
            Title = "Welcome to the forum!",
            Author = "Jonatan Streith",
            Posts = new List<Postitem>()
            {
            new Postitem() {Id=1, Author = "Jonatan Streith", BelongsToThread = thread1, Content = "Welcome, new user! Enjoy your stay, and obey the rules!"},
            new Postitem() {Id=2, Author = "Douchey Newguy", BelongsToThread = thread1, Content = "This place sucks. Where's the content?"},
            new Postitem() {Id=3, Author = "Jonatan Streith", BelongsToThread = thread1, Content = "New content will be added in time. This is just a placeholder. Also I'm talking to myself?"}
            }
        };







        public static Threaditem thread2 = new Threaditem()
        {
            Id = 2,
            Title = "User rules",
            Author = "Jonatan Streith",
            Posts = new List<Postitem>()
            {
            new Postitem() {Id=4, Author = "Jonatan Streith", BelongsToThread = thread1, Content = "User rules! That means you! You rule!"},
            new Postitem() {Id=5, Author = "Nitpick McAnalRetentive", BelongsToThread = thread1, Content = "Actually, that's not what 'user rules' is supposed to mean."},
            new Postitem() {Id=6, Author = "Jonatan Streith", BelongsToThread = thread1, Content = "Terms mean what I say they mean. Don't make me ban you for your offensive username."}

            }
        };

        public static List<Threaditem> Threads = new List<Threaditem>() { thread1, thread2 };

        public static Boarditem board1 = new Boarditem()
        {
            Id = 1,
            Title = "Main",
            Description = "The main board. Post generic content here.",
            Threads = new List<Threaditem>() { thread1, thread2 }
        };


        public static Boarditem board2 = new Boarditem()
        {
            Id = 2,
            Title = "Gaming",
            Description = "Game-related topics.",
            Threads = new List<Threaditem>() { }
        };

        public static Boarditem board3 = new Boarditem()
        {
            Id = 3,
            Title = "Social",
            Description = "Meet the community!",
            Threads = new List<Threaditem>() { }
        };




        public static List<Boarditem> Boards = new List<Boarditem>() { board1, board2, board3 };

    }
}