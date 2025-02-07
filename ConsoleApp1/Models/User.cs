﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class User
    {
        public string Username { get; private set; } = "";
        public string Password { get; private set; } = "";
        public int ELO { get; private set; }
        public int Money { get; private set; }
        private List<Card> Deck = new List<Card>();
        private List<Card> Stack = new List<Card>();
        private List<List<Card>> Packages = new List<List<Card>>();

        public User(string username, string password)
        {
            ELO = 100;
            Money = 20;
            Username = username;
            Password = password;
        }

    }
}
