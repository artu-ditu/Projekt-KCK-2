using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Komunikacja_Człowiek_Komputer___Projekt_2.Klasy {
    class Database {public Database () {
            Schedule = new List<Fixture>();
            Table = new List<Club>();
            Scorers = new List<Scorer>();
            Assistants = new List<Assistant>();
        }

        public Database(List<Fixture> schedule, List<Club> table, List<Scorer> scorers, List<Assistant> assistants)
        {
            Schedule = schedule;
            Table = table;
            Scorers = scorers;
            Assistants = assistants;
        }

        public void ReadDatabase()
        {
            string line;
            #region wczytywanie tabeli
            StreamReader file = new StreamReader("../../tabela.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] words = line.Split(';');
                Table.Add(new Club(Int32.Parse(words[0]), words[1], Int32.Parse(words[2]), Int32.Parse(words[3]),
                    Int32.Parse(words[4]), Int32.Parse(words[5]), Int32.Parse(words[6]), Int32.Parse(words[7]), words[8], Int32.Parse(words[9])));
            }
            file.Close();
            #endregion
            #region wczytywanie terminarza 
            file = new System.IO.StreamReader("../../terminarz.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] words = line.Split(';');
                Schedule.Add(new Fixture(Int32.Parse(words[0]), words[1], words[2], words[3], words[4], words[5]));
            }
            file.Close();
            #endregion
            #region wczytywanie strzelców
            file = new System.IO.StreamReader("../../strzelcy.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] words = line.Split(';');
                Scorers.Add(new Scorer(Int32.Parse(words[0]), words[1], Int32.Parse(words[2]), words[3], Int32.Parse(words[4])));
            }
            file.Close();
            #endregion
            #region wczytywanie asystentów
            file = new System.IO.StreamReader("../../asystenci.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] words = line.Split(';');
                Assistants.Add(new Assistant(Int32.Parse(words[0]), words[1], Int32.Parse(words[2]), words[3], Int32.Parse(words[4])));
            }
            file.Close();
            #endregion
        }

        public void WriteTablePolish(string theme)
        {
            Console.SetCursorPosition((Console.WindowWidth - "Tabela ligi".Length) / 2, 5);
            Console.WriteLine("Tabela ligi");
            Console.SetCursorPosition(7,7);
            Console.WriteLine("{0,3}\t{1,-20}{2,6}{3,6}{4,6}{5,6}{6,6}{7,6}{8,6}{9,6}",
                    "#", "Drużyna", "M", "W", "R", "P", "GZ", "GS", "RG", "P");
            for (int i = 0; i < Table.Count(); i++)
            {
                Console.SetCursorPosition(7, 9+i);
                if (Table[i].Position < 5) Console.BackgroundColor = ConsoleColor.Green;
                else if (Table[i].Position == 5) Console.BackgroundColor = ConsoleColor.DarkGreen;
                else if (Table[i].Position > 17) Console.BackgroundColor = ConsoleColor.Red;
                else Console.BackgroundColor = ConsoleColor.Gray;
                Console.WriteLine("{0,3}\t{1,-20}{2,6}{3,6}{4,6}{5,6}{6,6}{7,6}{8,6}{9,6}",
                    Table[i].Position, Table[i].Name, Table[i].FixturesPlayed, Table[i].FixturesWon, Table[i].FixturesDrawn,
                    Table[i].FixturesLost, Table[i].GoalsScored, Table[i].GoalsConceded, Table[i].GoalDifference, Table[i].Points);
            }
            
            if (theme=="dark") {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White; }

            Console.SetCursorPosition(7, 30);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write('█');
            Console.Write('█');
            if (theme=="dark") Console.ForegroundColor = ConsoleColor.White;
            else Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" - premiowane miejscem w Lidze Mistrzów");

            Console.SetCursorPosition(7, 31);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write('█');
            Console.Write('█');
            if (theme=="dark") Console.ForegroundColor = ConsoleColor.White;
            else Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" - premiowane miejscem w Lidze Europy");

            Console.SetCursorPosition(7, 32);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write('█');
            Console.Write('█');
            if (theme=="dark") Console.ForegroundColor = ConsoleColor.White;
            else Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" - spadek do Championship");
        }

        public void WriteTableEnglish(string theme)
        {

            Console.SetCursorPosition((Console.WindowWidth - "League table".Length) / 2, 5);
            Console.WriteLine("League table");
            Console.SetCursorPosition(7,7);
            Console.WriteLine("{0,3}\t{1,-20}{2,6}{3,6}{4,6}{5,6}{6,6}{7,6}{8,6}{9,6}",
                    "#", "Team", "M", "W", "D", "L", "GS", "GL", "GD", "P");
            for (int i = 0; i < Table.Count(); i++)
            {
                Console.SetCursorPosition(7, 9+i);
                if (Table[i].Position < 5) Console.BackgroundColor = ConsoleColor.Green;
                else if (Table[i].Position == 5) Console.BackgroundColor = ConsoleColor.DarkGreen;
                else if (Table[i].Position > 17) Console.BackgroundColor = ConsoleColor.Red;
                else Console.BackgroundColor = ConsoleColor.Gray;
                Console.WriteLine("{0,3}\t{1,-20}{2,6}{3,6}{4,6}{5,6}{6,6}{7,6}{8,6}{9,6}",
                    Table[i].Position, Table[i].Name, Table[i].FixturesPlayed, Table[i].FixturesWon, Table[i].FixturesDrawn,
                    Table[i].FixturesLost, Table[i].GoalsScored, Table[i].GoalsConceded, Table[i].GoalDifference, Table[i].Points);
            }
            if (theme=="dark") {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White; }

            Console.SetCursorPosition(7, 30);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write('█');
            Console.Write('█');
            if (theme=="dark") Console.ForegroundColor = ConsoleColor.White;
            else Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" - UEFA Champions League");

            Console.SetCursorPosition(7, 31);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write('█');
            Console.Write('█');
            if (theme=="dark") Console.ForegroundColor = ConsoleColor.White;
            else Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" - Europa League");

            Console.SetCursorPosition(7, 32);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write('█');
            Console.Write('█');
            if (theme=="dark") Console.ForegroundColor = ConsoleColor.White;
            else Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(" - Relegation to Championship");
        }

        public void WriteSchedulePolish(int gameweek)
        {
            Console.SetCursorPosition((Console.WindowWidth - "Kolejka X".Length) / 2, 5);
            Console.WriteLine("Kolejka {0}", gameweek);
            Console.WriteLine();
            Console.WriteLine("{0,20} {1,-10}{2,20}{3,4}{4,5}{5,4}{6,-20}",
                    "Data", "", "Gospodarz", "", "Wynik", "", "Gość");
            Console.WriteLine();
            for (int i = 0; i < Schedule.Count(); i++)
            {
                if (Schedule[i].Gameweek == gameweek) Console.WriteLine("{0,20} {1,-10}{2,20}{3,4}{4,5}{5,4}{6,-20}",
                                                    Schedule[i].Date, Schedule[i].Hour, Schedule[i].Home, "", Schedule[i].Score + " ", "", Schedule[i].Visitor);
            }
        }

        public void WriteScheduleEnglish(int gameweek)
        {
            Console.SetCursorPosition((Console.WindowWidth - "Gameweek X".Length) / 2, 5);
            Console.WriteLine("Gameweek {0}", gameweek);
            Console.WriteLine();
            Console.WriteLine("{0,20} {1,-10}{2,20}{3,4}{4,5}{5,4}{6,-20}",
                    "Date", "", "Home", "", "Score", "", "Visitor");
            Console.WriteLine();
            for (int i = 0; i < Schedule.Count(); i++)
            {
                if (Schedule[i].Gameweek == gameweek) Console.WriteLine("{0,20} {1,-10}{2,20}{3,4}{4,5}{5,4}{6,-20}",
                                                    Schedule[i].Date, Schedule[i].Hour, Schedule[i].Home, "", Schedule[i].Score + " ", "", Schedule[i].Visitor);
            }
        }

        public void WriteScorersPolish()
        {
            Console.SetCursorPosition((Console.WindowWidth - "Tabela strzelców".Length) / 2, 5);
            Console.WriteLine("Tabela strzelców");
            Console.WriteLine();
            Console.WriteLine("{0,20}\t{1,-20}{2,6}\t{3,-20}{4,6}",
                    "#", "Strzelec", "Wiek", "Drużyna", "Gole");
            Console.WriteLine();
            for (int i = 0; i < Scorers.Count(); i++)
            {
                Console.WriteLine("{0,20}\t{1,-20}{2,6}\t{3,-20}{4,6}",
                    Scorers[i].Position, Scorers[i].Name, Scorers[i].Age, Scorers[i].Club, Scorers[i].Goals);
            }
        }

        public void WriteAssistantsPolish()
        {
            Console.SetCursorPosition((Console.WindowWidth - "Tabela asystentów".Length) / 2, Console.CursorTop);
            Console.WriteLine("Tabela asystentów");
            Console.WriteLine();
            Console.WriteLine("{0,20}\t{1,-20}{2,6}\t{3,-20}{4,6}",
                    "#", "Asystent", "Wiek", "Drużyna", "Asysty");
            Console.WriteLine();
            for (int i = 0; i < Scorers.Count(); i++)
            {
                Console.WriteLine("{0,20}\t{1,-20}{2,6}\t{3,-20}{4,6}",
                    Assistants[i].Position, Assistants[i].Name, Assistants[i].Age, Assistants[i].Club, Assistants[i].Assists);
            }

        }

        public void WriteScorersEnglish()
        {
            Console.SetCursorPosition((Console.WindowWidth - "Top scorers".Length) / 2, 5);
            Console.WriteLine("Top scorers");
            Console.WriteLine();
            Console.WriteLine("{0,20}\t{1,-20}{2,6}\t{3,-20}{4,6}",
                    "#", "Scorer", "Age", "Team", "Goals");
            Console.WriteLine();
            for (int i = 0; i < Scorers.Count(); i++)
            {
                Console.WriteLine("{0,20}\t{1,-20}{2,6}\t{3,-20}{4,6}",
                    Scorers[i].Position, Scorers[i].Name, Scorers[i].Age, Scorers[i].Club, Scorers[i].Goals);
            }
        }

        public void WriteAssistantsEnglish()
        {
            Console.SetCursorPosition((Console.WindowWidth - "Top assistants".Length) / 2, Console.CursorTop);
            Console.WriteLine("Top assistants");
            Console.WriteLine();
            Console.WriteLine("{0,20}\t{1,-20}{2,6}\t{3,-20}{4,6}",
                    "#", "Assistant", "Age", "Team", "Assists");
            Console.WriteLine();
            for (int i = 0; i < Scorers.Count(); i++)
            {
                Console.WriteLine("{0,20}\t{1,-20}{2,6}\t{3,-20}{4,6}",
                    Assistants[i].Position, Assistants[i].Name, Assistants[i].Age, Assistants[i].Club, Assistants[i].Assists);
            }

        }

        public List<Fixture> Schedule { get; set; }
        public List<Club> Table { get; set; }
        public List<Scorer> Scorers { get; set; }
        public List<Assistant> Assistants { get; set; }
    }
}
