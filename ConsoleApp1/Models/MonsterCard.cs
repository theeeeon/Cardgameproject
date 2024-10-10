using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1.Classes
{
    class MonsterCard : Card
    {
        public Monstertype mtype { get; private set; }
        public MonsterCard(string Name, int Damage, Monstertype mtype, Spelltype stype) : base(Name, Damage, stype)
        {
            this.mtype = mtype;
        }

    }

    enum Monstertype
    {
        Dragon,
        Fireelf,
        Knight,
        Ork,
        Kraken,
        Wizard, 
        Goblin
    }
}
