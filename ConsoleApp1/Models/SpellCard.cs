using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class SpellCard : Card
    {
        public SpellCard(string ID,string Name, int Damage, Spelltype type) : base(ID, Name, Damage, type)
        {

        }
    }
}
