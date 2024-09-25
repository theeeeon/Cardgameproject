using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//morggen moodle forum fragen erkundigen benni
//methoden? was in abgabe? notation {get set}?

namespace ConsoleApp1.Classes
{
    abstract class Card
    {
        public string Name { get; private set; }
        public readonly int Damage;
        public Type Elementtype { get; private set; }


    }

    enum Type
    {
        water,
        fire,
        normal
    }
}
