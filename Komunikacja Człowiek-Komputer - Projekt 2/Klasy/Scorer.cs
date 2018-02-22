using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komunikacja_Człowiek_Komputer___Projekt_2.Klasy {
    class Scorer {
        public Scorer(int position, string name, int age, string club, int goals)
        {
            Position = position;
            Name = name;
            Age = age;
            Club = club;
            Goals = goals;
        }

        public int Position { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Club { get; set; }
        public int Goals { get; set; }
    }
}
