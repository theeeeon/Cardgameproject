using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1.Models
{
    public class MonsterCard : Card
    {
        public Monstertype mtype { get; private set; }
        public MonsterCard(string ID, string Name, int Damage, Monstertype mtype, Spelltype stype) : base(ID, Name, Damage, stype)
        {
            this.mtype = mtype;
        }

    }

    public enum Monstertype
    {
        Dragon,
        Fireelf,
        Knight,
        Ork,
        Kraken,
        Wizard,
        Goblin,
        Spell
    }
}
