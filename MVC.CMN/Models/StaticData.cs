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

    }
}