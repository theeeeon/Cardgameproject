using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes
{
    class SpellCard : Card
    {
        public Spelltype type { get; private set; }
        public SpellCard(string Name, int Damage, Spelltype type) : base(Name, Damage)
        {
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
