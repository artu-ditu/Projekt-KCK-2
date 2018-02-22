using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komunikacja_Człowiek_Komputer___Projekt_2.Klasy {
    class Club {
        public Club(int position, string name, int fixturesPlayed, int fixturesWon, int fixturesDrawn, int fixturesLost, int goalsScored, int goalsConceded, string goalDifference, int points)
        {
            Position = position;
            Name = name;
            FixturesPlayed = fixturesPlayed;
            FixturesWon = fixturesWon;
            FixturesDrawn = fixturesDrawn;
            FixturesLost = fixturesLost;
            GoalsScored = goalsScored;
            GoalsConceded = goalsConceded;
            GoalDifference = goalDifference;
            Points = points;
        }

        public int Position { get; set; }
        public string Name { get; set; }
        public int FixturesPlayed { get; set; }
        public int FixturesWon { get; set; }
        public int FixturesDrawn { get; set; }
        public int FixturesLost { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
        public string GoalDifference { get; set; }
        public int Points { get; set; }
    }
}
