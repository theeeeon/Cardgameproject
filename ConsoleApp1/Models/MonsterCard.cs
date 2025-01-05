using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1.Models
{
    public class MonsterCard : Card
    {
        public string mtype { get; private set; }
        public MonsterCard(string ID, string Name, double Damage, string mtype, string stype) : base(ID, Name, Damage, stype)
        {
            this.mtype = mtype;
        }

    }

}
