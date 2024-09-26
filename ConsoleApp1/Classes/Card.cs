using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ConsoleApp1.Classes
{
    abstract class Card
    {
        public string Name { get; private set; }
        public readonly int Damage;
        public Card(string Name, int Damage)
        {
            this.Name = Name;
            this.Damage = Damage;
        }


    }
}
