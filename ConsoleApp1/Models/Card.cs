using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ConsoleApp1.Models
{
    public class Card
    {
        public string ID;
        public string Name { get; private set; }
        public readonly double Damage;
        public string Spelltype { get; set; }
        public Card(string ID, string Name, double Damage, string Spelltype)
        {
            this.ID = ID;
            this.Name = Name;
            this.Damage = Damage;
            this.Spelltype = Spelltype;
        }


    }


}
