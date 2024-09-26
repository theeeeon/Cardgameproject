using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes
{
    class User
    {
        public string Username { get; private set; }
        private string Password;
        public int ELO { get; private set; }
        public int Money { get; private set; }
        public List<Card> Deck { get; private set; }
        public List<Card> Stack { get; private set; }

        public User(string username, string password)
        {
            ELO = 100;
            Money = 20;
            this.Username = username;
            this.Password = password;
        }

    }
}
