using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1.Classes
{
    class MonsterCard : Card
    {
        public Monstertype type { get; private set; }
        public MonsterCard(string Name, int Damage, Monstertype type) : base(Name, Damage)
        {
            this.type = type;
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
