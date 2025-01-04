using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ConsoleApp1.Models
{
    public abstract class Card
    {
        public string ID;
        public string Name { get; private set; }
        public readonly int Damage;
        public Spelltype type { get; private set; }
        public Card(string ID, string Name, int Damage, Spelltype type)
        {
            this.ID = ID;
            this.Name = Name;
            this.Damage = Damage;
            this.type = type;
        }


    }
    public enum Spelltype
    {
        water,
        fire,
        normal
    }


}
