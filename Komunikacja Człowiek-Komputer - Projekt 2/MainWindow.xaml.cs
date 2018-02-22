using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Media;
using System.IO;
using System.Reflection;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Komunikacja_Człowiek_Komputer___Projekt_2.Klasy;

namespace Komunikacja_Człowiek_Komputer___Projekt_2 {

    public partial class MainWindow : Window {

        private WindowController windowController;

        private string soundStatus = "unmuted";

        private Assembly assembly;
        private SoundPlayer soundPlayer;

        public MainWindow () {
            InitializeComponent();
            this.FontFamily = new FontFamily("Consolas");
            WindowController Controller = new WindowController ("polish","light",MainWindowCanvas);
            this.windowController = Controller;
        }

        private void KeyboardListener (object sender,KeyEventArgs e) {
            if (e.Key==Key.M) {
                if (soundStatus=="unmuted") {
                    windowController.ChangeSoundStatus("off");
                    soundStatus = "muted";
                }
                else {
                    soundStatus = "unmuted";
                    windowController.ChangeSoundStatus("on");
                }
            }
            switch (windowController.getStatus()) {
                case "WelcomeScreen":
                    if (e.Key==Key.Left || e.Key==Key.Right) {
                        windowController.WelcomeScreenChange();
                    }
                    else if (e.Key==Key.Enter)
                        windowController.ThemeSelectionScreen();
                    else if (e.Key==Key.Escape)
                        Environment.Exit(0);
                    break;
                case "ThemeSelectionScreen":
                    if (e.Key==Key.Left || e.Key==Key.Right) {
                        windowController.ThemeSelectionScreenChange();
                    }
                    else if (e.Key==Key.Escape)
                        Environment.Exit(0);
                    else if (e.Key==Key.Back)
                        windowController.WelcomeScreen();
                    else if (e.Key==Key.Enter)
                        windowController.MenuScreen();
                    break;
                case "MenuScreen":
                    if (e.Key==Key.Left)
                        windowController.MenuScreenChange("left");
                    else if (e.Key==Key.Right)
                        windowController.MenuScreenChange("right");
                    else if (e.Key==Key.Up)
                        windowController.MenuScreenChange("up");
                    else if (e.Key==Key.Down)
                        windowController.MenuScreenChange("down");
                    else if (e.Key==Key.Escape)
                        Environment.Exit(0);
                    else if (e.Key==Key.Enter)
                        windowController.EnterSection();
                    else if (e.Key==Key.Back)
                        windowController.ThemeSelectionScreen();
                    break;
                case "LeagueTableScreen":
                case "MatchOfTheWeekScreen":
                case "StatisticsScreen":
                    if (e.Key==Key.Back)
                        windowController.MenuScreen();
                    else if (e.Key==Key.Escape)
                        Environment.Exit(0);
                    break;
                case "ScheduleScreen":
                    if (e.Key==Key.Left)
                        windowController.ScheduleScreenChange("left");
                    else if (e.Key==Key.Right)
                        windowController.ScheduleScreenChange("right");
                    else if (e.Key==Key.Back)
                        windowController.MenuScreen();
                    else if (e.Key==Key.Escape)
                        Environment.Exit(0);
                    break;
            }
        }
    }
}
