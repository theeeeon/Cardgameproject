using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ConsoleApp1.Models
{
    abstract class Card
    {
        public string Name { get; private set; }
        public readonly int Damage;
        public Spelltype type { get; private set; }
        public Card(string Name, int Damage, Spelltype type)
        {
            this.Name = Name;
            this.Damage = Damage;
            this.type = type;
        }


    }
    enum Spelltype
    {
        water,
        fire,
        normal
    }


}
