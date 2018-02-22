using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komunikacja_Człowiek_Komputer___Projekt_2.Klasy {
    class Fixture {public Fixture(int gameweek, string date, string hour, string home, string visitor, string score)
        {
            Gameweek = gameweek;
            Date = date;
            Hour = hour;
            Home = home;
            Visitor = visitor;
            Score = score;
        }

        public int Gameweek { get; set; }
        public string Date { get; set; }
        public string Hour { get; set; }
        public string Home { get; set; }
        public string Visitor { get; set; }
        public string Score { get; set; }
    }
}
