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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Reflection;
using System.Media;

namespace Komunikacja_Człowiek_Komputer___Projekt_2.Klasy {
    
    public class WindowController {

        private Canvas windowCanvas;
        
        private const int canvasWidth = 1280;
        private const int canvasHeight = 720;

        private string currentStatus;
        private string selectedLanguage;
        private string selectedTheme;

        private int currentMenuStatus;

        private Assembly assembly;
        private SoundPlayer soundPlayer;

        private string soundStatus;

        private Database database;

        private int currentGameweek;

        public void setLanguage (string language) {
            this.selectedLanguage = language; }
        
        public void setTheme (string theme) {
            this.selectedTheme = theme; }

        public void setStatus (string status) {
            this.currentStatus = status;
        }
        public string getLanguage () {
            return this.selectedLanguage; }

        public string getTheme () {
            return this.selectedTheme; }

        public string getStatus () {
            return this.currentStatus;
        }

        public WindowController (string language, string theme, Canvas canvas) {
            assembly = Assembly.GetExecutingAssembly();
            soundPlayer = new SoundPlayer("PremierLeagueMusic.wav");
            soundPlayer.PlayLooping();
            this.currentStatus = "WelcomeScreen";
            this.selectedLanguage = language;
            this.selectedTheme = theme;
            this.soundStatus = "unmuted";
            this.windowCanvas = canvas;
            this.WelcomeScreen();
            this.currentMenuStatus = 2;
            this.database = new Database();
            this.database.ReadDatabase();
            this.currentGameweek = 1;
            }

        public void printPicture (string fileName) {
            if (File.Exists(fileName)) {
                StreamReader streamReader = new StreamReader(fileName);
                string line = streamReader.ReadLine();
                string[] firstLine = new string[2];
                firstLine = line.Split(null);
                int rows = Int32.Parse(firstLine[0]);
                int columns = Int32.Parse(firstLine[1]);
                int marginTop = Int32.Parse(firstLine[2]);
                int marginLeft = Int32.Parse(firstLine[3]);
                char[,] pictureArray = new char[rows,columns];
                for (int i=0; i<rows; i++) {
                    line = streamReader.ReadLine();
                    for (int j=0; j<columns; j++)
                        pictureArray[i,j] = line[j]; }
                for (int i=0; i<rows; i++) {
                    for (int j=0; j<columns; j++) {
                        Rectangle pixel = new Rectangle();
                        pixel.Height = 1;
                        pixel.Width = 1;
                        pixel.Fill = Brushes.Aqua;
                        switch (pictureArray[i,j]) {
                            case '.':
                                if (this.selectedTheme == "normal")
                                    pixel.Fill = Brushes.White;
                                else
                                    pixel.Fill = Brushes.Black;
                                break;
                            case 'W':
                                if (this.selectedTheme == "normal")
                                    pixel.Fill = Brushes.White;
                                break;
                            case 'B':
                                if (this.selectedTheme == "normal")
                                    pixel.Fill = Brushes.Black;
                                break;
                            case 'b':
                                if (this.selectedTheme == "noraml")
                                    pixel.Fill = Brushes.DarkBlue;
                                break;
                            case 'Y':
                                if (this.selectedTheme == "normal")
                                    pixel.Fill = Brushes.Yellow;
                                break;
                            case 'R':
                                if (this.selectedTheme == "normal")
                                    pixel.Fill = Brushes.Red;
                                break;
                            case 'P':
                                if (this.selectedTheme == "normal")
                                    pixel.Fill = Brushes.Purple;
                                break;
                            default:
                                break;
                        }
                        Canvas.SetLeft(pixel,marginLeft+j);
                        Canvas.SetTop(pixel,marginTop+i);
                        windowCanvas.Children.Add(pixel);
                    }
                }
            }
        }

        public void WelcomeScreen () {
            /*
            printPicture("C:/Studia/Programowanie - Projekty Visual Studio/Komunikacja Człowiek-Komputer - Projekt 2/Komunikacja Człowiek-Komputer - Projekt 2/PremierLeagueLogo.txt");
            printPicture("C:/Studia/Programowanie - Projekty Visual Studio/Komunikacja Człowiek-Komputer - Projekt 2/Komunikacja Człowiek-Komputer - Projekt 2/PolandFlag.txt");
            printPicture("C:/Studia/Programowanie - Projekty Visual Studio/Komunikacja Człowiek-Komputer - Projekt 2/Komunikacja Człowiek-Komputer - Projekt 2/LeftArrow.txt");
            printPicture("C:/Studia/Programowanie - Projekty Visual Studio/Komunikacja Człowiek-Komputer - Projekt 2/Komunikacja Człowiek-Komputer - Projekt 2/RightArrow.txt");
            */
            currentStatus = "WelcomeScreen";
            windowCanvas.Children.Clear();
            Image img = new Image();
            img.Width=1280;
            img.Height=720;
            img.Source = new BitmapImage(new Uri("Obrazki/WelcomeScreen.jpg", UriKind.Relative));
            Canvas.SetLeft(img,0);
            Canvas.SetTop(img,0);
            windowCanvas.Children.Add(img);
            Image flag = new Image();
            flag.Width=300;
            flag.Height=225;
            flag.Name="Flag";
            if (this.selectedLanguage=="polish")
                flag.Source = new BitmapImage(new Uri("Obrazki/WelcomeScreen-PL.png", UriKind.Relative));
            else
                flag.Source = new BitmapImage(new Uri("Obrazki/WelcomeScreen-EN.png", UriKind.Relative));
            Canvas.SetTop(flag,475);
            Canvas.SetLeft(flag,490);
            windowCanvas.Children.Add(flag);
            PrintSoundIcon();
            CreateWelcomeScreenFlagButton();
            CreateWelcomeScreenArrowButtons();
    }
        public void WelcomeScreenChange () {
            var child = (UIElement)LogicalTreeHelper.FindLogicalNode(windowCanvas,"Flag");
            windowCanvas.Children.Remove(child);
            if (selectedLanguage=="polish")
                selectedLanguage="english";
            else
                selectedLanguage="polish";
            Image flag = new Image();
            flag.Width=300;
            flag.Height=225;
            flag.Name="Flag";
            if (this.selectedLanguage=="polish")
                flag.Source = new BitmapImage(new Uri("Obrazki/WelcomeScreen-PL.png", UriKind.Relative));
            else
                flag.Source = new BitmapImage(new Uri("Obrazki/WelcomeScreen-EN.png", UriKind.Relative));
            Canvas.SetTop(flag,475);
            Canvas.SetLeft(flag,490);
            windowCanvas.Children.Add(flag);
        }
        public void ThemeSelectionScreen () {
            this.setStatus("ThemeSelectionScreen");
            windowCanvas.Children.Clear();
            Image img = new Image();
            img.Width=1280;
            img.Height=720;
            img.Name = "Background";
            img.Source = (selectedTheme=="light") ? new BitmapImage(new Uri("Obrazki/ThemeSelectionScreen-Light.jpg",UriKind.Relative)) : new BitmapImage(new Uri("Obrazki/ThemeSelectionScreen-Dark.jpg",UriKind.Relative));
            Canvas.SetLeft(img,0);
            Canvas.SetTop(img,0);
            windowCanvas.Children.Add(img);
            Image theme = new Image();
            theme.Width=880;
            theme.Height=140;
            theme.Name = "Theme";
            if (selectedLanguage=="polish") {
                if (selectedTheme=="light")
                    theme.Source = new BitmapImage(new Uri("Obrazki/ThemeSelectionScreen-Light-PL.jpg",UriKind.Relative));
                else
                    theme.Source = new BitmapImage(new Uri("Obrazki/ThemeSelectionScreen-Dark-PL.jpg",UriKind.Relative)); }
            else {
                if (selectedTheme=="light")
                    theme.Source = new BitmapImage(new Uri("Obrazki/ThemeSelectionScreen-Light-EN.jpg",UriKind.Relative));
                else
                    theme.Source = new BitmapImage(new Uri("Obrazki/ThemeSelectionScreen-Dark-EN.jpg",UriKind.Relative)); }
            Canvas.SetLeft(theme,200);
            Canvas.SetTop(theme,580);
            windowCanvas.Children.Add(theme);
            PrintSoundIcon();
            CreateThemeSelectionScreenArrowButtons();
        }
        public void ThemeSelectionScreenChange () {
            selectedTheme = (selectedTheme=="light") ? "dark":"light";
            windowCanvas.Children.Clear();
            Image img = new Image();
            img.Width=1280;
            img.Height=720;
            img.Name = "Background";
            img.Source = (selectedTheme=="light") ? new BitmapImage(new Uri("Obrazki/ThemeSelectionScreen-Light.jpg",UriKind.Relative)) : new BitmapImage(new Uri("Obrazki/ThemeSelectionScreen-Dark.jpg",UriKind.Relative));
            Canvas.SetLeft(img,0);
            Canvas.SetTop(img,0);
            windowCanvas.Children.Add(img);
            Image theme = new Image();
            theme.Width=880;
            theme.Height=140;
            theme.Name = "Theme";
            if (selectedLanguage=="polish") {
                if (selectedTheme=="light")
                    theme.Source = new BitmapImage(new Uri("Obrazki/ThemeSelectionScreen-Light-PL.jpg",UriKind.Relative));
                else
                    theme.Source = new BitmapImage(new Uri("Obrazki/ThemeSelectionScreen-Dark-PL.jpg",UriKind.Relative)); }
            else {
                if (selectedTheme=="light")
                    theme.Source = new BitmapImage(new Uri("Obrazki/ThemeSelectionScreen-Light-EN.jpg",UriKind.Relative));
                else
                    theme.Source = new BitmapImage(new Uri("Obrazki/ThemeSelectionScreen-Dark-EN.jpg",UriKind.Relative)); }
            Canvas.SetLeft(theme,200);
            Canvas.SetTop(theme,580);
            windowCanvas.Children.Add(theme);
            PrintSoundIcon();
            CreateThemeSelectionScreenArrowButtons();
        }
        public void MenuScreen () {
            windowCanvas.Children.Clear();
            currentStatus = "MenuScreen";
            Image img;
            img = new Image();
            img.Width=1280;
            img.Height=720;
            img.Name="Background";
            if (this.selectedTheme =="light") {
                if (this.selectedLanguage == "polish")
                    img.Source = new BitmapImage(new Uri("Obrazki/Menu-PL-Light.jpg", UriKind.Relative));
                else
                    img.Source = new BitmapImage(new Uri("Obrazki/Menu-EN-Light.jpg", UriKind.Relative));
            }
            else {
                if (this.selectedLanguage == "polish")
                    img.Source = new BitmapImage(new Uri("Obrazki/Menu-PL-Dark.jpg", UriKind.Relative));
                else
                    img.Source = new BitmapImage(new Uri("Obrazki/Menu-EN-Dark.jpg", UriKind.Relative));
            }
            Canvas.SetLeft(img,0);
            Canvas.SetTop(img,0);
            windowCanvas.Children.Add(img);
            ActiveMenuSection();
            PrintSoundIcon();
            CreateMenuSectionButtons();
        }
        public void ActiveMenuSection () {
            Image active = new Image();
            active.Name = "Active";
            switch (currentMenuStatus) {
                case 1:
                    active.Width = 192;
                    active.Height = 185;
                    if (selectedLanguage=="polish") {
                        if (selectedTheme=="light")
                            active.Source = new BitmapImage(new Uri("Obrazki/Schedule-PL-Light.jpg",UriKind.Relative));
                        else
                            active.Source = new BitmapImage(new Uri("Obrazki/Schedule-PL-Dark.jpg",UriKind.Relative)); }
                    else {
                        if (selectedTheme=="light")
                            active.Source = new BitmapImage(new Uri("Obrazki/Schedule-EN-Light.jpg",UriKind.Relative));
                        else
                            active.Source = new BitmapImage(new Uri("Obrazki/Schedule-EN-Dark.jpg",UriKind.Relative)); }
                    Canvas.SetTop(active,247);
                    Canvas.SetLeft(active,243);
                    windowCanvas.Children.Add(active);
                    break;
                case 2:
                    active.Width = 255;
                    active.Height = 169;
                    if (selectedLanguage=="polish") {
                        if (selectedTheme=="light")
                            active.Source = new BitmapImage(new Uri("Obrazki/LeagueTable-PL-Light.jpg",UriKind.Relative));
                        else
                            active.Source = new BitmapImage(new Uri("Obrazki/LeagueTable-PL-Dark.jpg",UriKind.Relative)); }
                    else {
                        if (selectedTheme=="light")
                            active.Source = new BitmapImage(new Uri("Obrazki/LeagueTable-EN-Light.jpg",UriKind.Relative));
                        else
                            active.Source = new BitmapImage(new Uri("Obrazki/LeagueTable-EN-Dark.jpg",UriKind.Relative)); }
                    Canvas.SetTop(active,215);
                    Canvas.SetLeft(active,517);
                    windowCanvas.Children.Add(active);
                    break;
                case 3:
                    active.Width = 180;
                    active.Height = 190;
                    if (selectedLanguage=="polish") {
                        if (selectedTheme=="light")
                            active.Source = new BitmapImage(new Uri("Obrazki/Stats-PL-Light.jpg",UriKind.Relative));
                        else
                            active.Source = new BitmapImage(new Uri("Obrazki/Stats-PL-Dark.jpg",UriKind.Relative)); }
                    else {
                        if (selectedTheme=="light")
                            active.Source = new BitmapImage(new Uri("Obrazki/Stats-EN-Light.jpg",UriKind.Relative));
                        else
                            active.Source = new BitmapImage(new Uri("Obrazki/Stats-EN-Dark.jpg",UriKind.Relative)); }
                    Canvas.SetTop(active,246);
                    Canvas.SetLeft(active,838);
                    windowCanvas.Children.Add(active);
                    break;
                case 4:
                    active.Height = 226;
                    if (selectedLanguage=="polish") {
                        active.Width = 229;
                        Canvas.SetTop(active,415);
                        Canvas.SetLeft(active,526);
                        if (selectedTheme=="light")
                            active.Source = new BitmapImage(new Uri("Obrazki/MotW-PL-Light.jpg",UriKind.Relative));
                        else
                            active.Source = new BitmapImage(new Uri("Obrazki/MotW-PL-Dark.jpg",UriKind.Relative)); }
                    else {
                        active.Width = 352;
                        Canvas.SetTop(active,415);
                        Canvas.SetLeft(active,466);
                        if (selectedTheme=="light")
                            active.Source = new BitmapImage(new Uri("Obrazki/MotW-EN-Light.jpg",UriKind.Relative));
                        else
                            active.Source = new BitmapImage(new Uri("Obrazki/MotW-EN-Dark.jpg",UriKind.Relative)); }
                    windowCanvas.Children.Add(active);
                    break;
                default:
                    break;
            }
        }

        public void MenuScreenChange (string direction) {
            var child = (UIElement)LogicalTreeHelper.FindLogicalNode(windowCanvas,"Active");
            windowCanvas.Children.Remove(child);
            switch (direction) {
                case "left":
                    if (currentMenuStatus==2)
                        currentMenuStatus = 1;
                    else if (currentMenuStatus==3)
                        currentMenuStatus = 2;
                    else if (currentMenuStatus==4)
                        currentMenuStatus = 1;
                    break;
                case "right":
                    if (currentMenuStatus==1)
                        currentMenuStatus = 2;
                    else if (currentMenuStatus==2)
                        currentMenuStatus = 3;
                    else if (currentMenuStatus==4)
                        currentMenuStatus = 3;
                    break;
                case "up":
                    if (currentMenuStatus==4)
                        currentMenuStatus = 2;
                    break;
                case "down":
                    if (currentMenuStatus<4)
                        currentMenuStatus = 4;
                    break;
                default:
                    break;
            }
            ActiveMenuSection();
        }

        public void EnterSection () {
            Image img;
            img = new Image();
            img.Width=1280;
            img.Height=720;
            switch (currentMenuStatus) {
                case 1:
                    this.setStatus("ScheduleScreen");
                    if (selectedLanguage=="polish") {
                        if (selectedTheme=="light")
                            img.Source = new BitmapImage(new Uri("Obrazki/ScheduleScreen-PL-Light.jpg", UriKind.Relative));
                        else
                            img.Source = new BitmapImage(new Uri("Obrazki/ScheduleScreen-PL-Dark.jpg", UriKind.Relative));
                    }
                    else {
                        if (selectedTheme=="light")
                            img.Source = new BitmapImage(new Uri("Obrazki/ScheduleScreen-EN-Light.jpg", UriKind.Relative));
                        else
                            img.Source = new BitmapImage(new Uri("Obrazki/ScheduleScreen-EN-Dark.jpg", UriKind.Relative));
                    }
                    break;
                case 2:
                    this.setStatus("LeagueTableScreen");
                    if (selectedLanguage=="polish") {
                        if (selectedTheme=="light")
                            img.Source = new BitmapImage(new Uri("Obrazki/FullLeagueTable-PL-Light.jpg", UriKind.Relative));
                        else
                            img.Source = new BitmapImage(new Uri("Obrazki/FullLeagueTable-PL-Dark.jpg", UriKind.Relative));
                    }
                    else {
                        if (selectedTheme=="light")
                            img.Source = new BitmapImage(new Uri("Obrazki/FullLeagueTable-EN-Light.jpg", UriKind.Relative));
                        else
                            img.Source = new BitmapImage(new Uri("Obrazki/FullLeagueTable-EN-Dark.jpg", UriKind.Relative));
                    }
                    break;
                case 3:
                    this.setStatus("StatisticsScreen");
                    if (selectedLanguage=="polish") {
                        if (selectedTheme=="light")
                            img.Source = new BitmapImage(new Uri("Obrazki/Statistics-PL-Light.jpg", UriKind.Relative));
                        else
                            img.Source = new BitmapImage(new Uri("Obrazki/Statistics-PL-Dark.jpg", UriKind.Relative));
                    }
                    else {
                        if (selectedTheme=="light")
                            img.Source = new BitmapImage(new Uri("Obrazki/Statistics-EN-Light.jpg", UriKind.Relative));
                        else
                            img.Source = new BitmapImage(new Uri("Obrazki/Statistics-EN-Dark.jpg", UriKind.Relative));
                    }
                    break;
                case 4:
                    this.setStatus("MatchOfTheWeekScreen");
                    if (selectedLanguage=="polish") {
                        if (selectedTheme=="light")
                            img.Source = new BitmapImage(new Uri("Obrazki/MatchOfTheWeek-PL-Light.jpg", UriKind.Relative));
                        else
                            img.Source = new BitmapImage(new Uri("Obrazki/MatchOfTheWeek-PL-Dark.jpg", UriKind.Relative));
                    }
                    else {
                        if (selectedTheme=="light")
                            img.Source = new BitmapImage(new Uri("Obrazki/MatchOfTheWeek-EN-Light.jpg", UriKind.Relative));
                        else
                            img.Source = new BitmapImage(new Uri("Obrazki/MatchOfTheWeek-EN-Dark.jpg", UriKind.Relative));
                    }
                    break;
                default:
                    break;
            }
            windowCanvas.Children.Clear();
            Canvas.SetLeft(img,0);
            Canvas.SetLeft(img,0);
            windowCanvas.Children.Add(img);
            PrintSoundIcon();
            if (currentMenuStatus==1) {
                ScheduleScreen();
            }
            if (currentMenuStatus==2) {
                LeagueTableScreen();
            }
        }

        public void ChangeSoundStatus (string setStatus) {
            if (setStatus=="on")
                soundPlayer.PlayLooping();
            else
                soundPlayer.Stop();
            var child = (UIElement)LogicalTreeHelper.FindLogicalNode(windowCanvas,"Sounds");
            windowCanvas.Children.Remove(child);
            if (soundStatus=="unmuted") {
                soundStatus="muted";
            }
            else if (soundStatus=="muted") {
                soundStatus="unmuted";
            }
            PrintSoundIcon();
        }

        public void PrintSoundIcon () {
            Image sounds = new Image();
            sounds.Width=50;
            sounds.Height=75;
            sounds.Name = "Sounds";
            if (soundStatus=="unmuted") {
                if (selectedTheme=="light")
                    sounds.Source = new BitmapImage(new Uri("Obrazki/SoundsUnmuted-Light.png",UriKind.Relative));
                else
                    sounds.Source = new BitmapImage(new Uri("Obrazki/SoundsUnmuted-Dark.png",UriKind.Relative));
            }
            else if (soundStatus=="muted") {
                if (selectedTheme=="light")
                    sounds.Source = new BitmapImage(new Uri("Obrazki/SoundsMuted-Light.png",UriKind.Relative));
                else
                    sounds.Source = new BitmapImage(new Uri("Obrazki/SoundsMuted-Dark.png",UriKind.Relative));
            }
            sounds.MouseUp += (s,e) => 
            {
                var child = (UIElement)LogicalTreeHelper.FindLogicalNode(windowCanvas,"Sounds");
                windowCanvas.Children.Remove(child);
                if (soundStatus=="unmuted") {
                    ChangeSoundStatus("off");
                }
                else if (soundStatus=="muted") {
                    ChangeSoundStatus("on");
                }
                PrintSoundIcon();
            };
            sounds.MouseEnter += (s,e) => Mouse.OverrideCursor = Cursors.Hand;
            sounds.MouseLeave += (s,e) => Mouse.OverrideCursor = Cursors.Arrow;
            Canvas.SetLeft(sounds,1220);
            Canvas.SetTop(sounds,10);
            windowCanvas.Children.Add(sounds);
        }

        public void CreateWelcomeScreenFlagButton () {
            Image flagButton = new Image();
            flagButton.Source = new BitmapImage(new Uri("Obrazki/EmptyFlag.png",UriKind.Relative));
            flagButton.Width = 250;
            flagButton.Height = 175;
            flagButton.MouseEnter += (s,e) => Mouse.OverrideCursor = Cursors.Hand;
            flagButton.MouseLeave += (s,e) => Mouse.OverrideCursor = Cursors.Arrow;
            flagButton.MouseDown += (s,e) => {
                flagButton.MouseUp += (S,E) => {
                    ThemeSelectionScreen();
                };
            };
            Canvas.SetTop(flagButton,496);
            Canvas.SetLeft(flagButton,515);
            windowCanvas.Children.Add(flagButton);
        }
        public void CreateWelcomeScreenArrowButtons () {
            Image leftArrow = new Image();
            Image rightArrow = new Image();
            leftArrow.Source = new BitmapImage(new Uri("Obrazki/EmptyArrow.png",UriKind.Relative));
            leftArrow.Width = 110;
            leftArrow.Height = 30;
            leftArrow.MouseEnter += (s,e) => Mouse.OverrideCursor = Cursors.Hand;
            leftArrow.MouseLeave += (s,e) => Mouse.OverrideCursor = Cursors.Arrow;
            leftArrow.MouseDown += (s,e) => {
                leftArrow.MouseUp += (S,E) => {
                    WelcomeScreenChange();
                };
            };
            rightArrow.Source = new BitmapImage(new Uri("Obrazki/EmptyArrow.png",UriKind.Relative));
            rightArrow.Width = 110;
            rightArrow.Height = 30;
            rightArrow.MouseEnter += (s,e) => Mouse.OverrideCursor = Cursors.Hand;
            rightArrow.MouseLeave += (s,e) => Mouse.OverrideCursor = Cursors.Arrow;
            rightArrow.MouseDown += (s,e) => {
                rightArrow.MouseUp += (S,E) => {
                    WelcomeScreenChange();
                };
            };
            Canvas.SetTop(leftArrow,575);
            Canvas.SetLeft(leftArrow,346);
            windowCanvas.Children.Add(leftArrow);
            Canvas.SetTop(rightArrow,575);
            Canvas.SetLeft(rightArrow,823);
            windowCanvas.Children.Add(rightArrow);
        }
        public void CreateThemeSelectionScreenArrowButtons () {
            Image leftArrow = new Image();
            Image rightArrow = new Image();
            leftArrow.Source = new BitmapImage(new Uri("Obrazki/EmptyArrow.png",UriKind.Relative));
            leftArrow.Width = 110;
            leftArrow.Height = 30;
            leftArrow.MouseEnter += (s,e) => Mouse.OverrideCursor = Cursors.Hand;
            leftArrow.MouseLeave += (s,e) => Mouse.OverrideCursor = Cursors.Arrow;
            leftArrow.MouseDown += (s,e) => {
                leftArrow.MouseUp += (S,E) => {
                    ThemeSelectionScreenChange();
                };
            };
            rightArrow.Source = new BitmapImage(new Uri("Obrazki/EmptyArrow.png",UriKind.Relative));
            rightArrow.Width = 110;
            rightArrow.Height = 30;
            rightArrow.MouseEnter += (s,e) => Mouse.OverrideCursor = Cursors.Hand;
            rightArrow.MouseLeave += (s,e) => Mouse.OverrideCursor = Cursors.Arrow;
            rightArrow.MouseDown += (s,e) => {
                rightArrow.MouseUp += (S,E) => {
                    ThemeSelectionScreenChange();
                };
            };
            Canvas.SetTop(leftArrow,618);
            Canvas.SetLeft(leftArrow,75);
            windowCanvas.Children.Add(leftArrow);
            Canvas.SetTop(rightArrow,618);
            Canvas.SetLeft(rightArrow,1095);
            windowCanvas.Children.Add(rightArrow);
        }

        public void CreateScheduleScreenArrowButtons () {
            Image leftArrow = new Image();
            Image rightArrow = new Image();
            leftArrow.Source = new BitmapImage(new Uri("Obrazki/EmptyArrow.png",UriKind.Relative));
            leftArrow.Width = 110;
            leftArrow.Height = 30;
            leftArrow.MouseEnter += (s,e) => Mouse.OverrideCursor = Cursors.Hand;
            leftArrow.MouseLeave += (s,e) => Mouse.OverrideCursor = Cursors.Arrow;
            leftArrow.MouseDown += (s,e) => {
                leftArrow.MouseUp += (S,E) => {
                    ScheduleScreenChange("left");
                };
            };
            rightArrow.Source = new BitmapImage(new Uri("Obrazki/EmptyArrow.png",UriKind.Relative));
            rightArrow.Width = 110;
            rightArrow.Height = 30;
            rightArrow.MouseEnter += (s,e) => Mouse.OverrideCursor = Cursors.Hand;
            rightArrow.MouseLeave += (s,e) => Mouse.OverrideCursor = Cursors.Arrow;
            rightArrow.MouseDown += (s,e) => {
                rightArrow.MouseUp += (S,E) => {
                    ScheduleScreenChange("right");
                };
            };
            Canvas.SetTop(leftArrow,352);
            Canvas.SetLeft(leftArrow,72);
            windowCanvas.Children.Add(leftArrow);
            Canvas.SetTop(rightArrow,352);
            Canvas.SetLeft(rightArrow,1098);
            windowCanvas.Children.Add(rightArrow);
        }

        public void CreateMenuSectionButtons () {
            Image schedule = new Image();
            Image table = new Image();
            Image stats = new Image();
            Image motw = new Image();
            schedule.Source = new BitmapImage(new Uri("Obrazki/EmptySchedule.png",UriKind.Relative));
            schedule.Width = 119;
            schedule.Height = 143;
            schedule.MouseEnter += (s,e) => Mouse.OverrideCursor = Cursors.Hand;
            schedule.MouseLeave += (s,e) => Mouse.OverrideCursor = Cursors.Arrow;
            schedule.MouseDown += (s,e) => {
                schedule.MouseUp += (S,E) => {
                    setStatus("ScheduleScreen");
                    currentMenuStatus = 1;
                    EnterSection();
                };
            };
            table.Source = new BitmapImage(new Uri("Obrazki/EmptyTable.png",UriKind.Relative));
            table.Width = 200;
            table.Height = 100;
            table.MouseEnter += (s,e) => Mouse.OverrideCursor = Cursors.Hand;
            table.MouseLeave += (s,e) => Mouse.OverrideCursor = Cursors.Arrow;
            table.MouseDown += (s,e) => {
                table.MouseUp += (S,E) => {
                    setStatus("LeagueTableScreen");
                    currentMenuStatus = 2;
                    EnterSection();
                };
            };
            stats.Source = new BitmapImage(new Uri("Obrazki/EmptyStats.png",UriKind.Relative));
            stats.Width = 127;
            stats.Height = 134;
            stats.MouseEnter += (s,e) => Mouse.OverrideCursor = Cursors.Hand;
            stats.MouseLeave += (s,e) => Mouse.OverrideCursor = Cursors.Arrow;
            stats.MouseDown += (s,e) => {
                stats.MouseUp += (S,E) => {
                    setStatus("StatisticsScreen");
                    currentMenuStatus = 3;
                    EnterSection();
                };
            };
            motw.Source = new BitmapImage(new Uri("Obrazki/EmptyMotW.png",UriKind.Relative));
            motw.Width = 180;
            motw.Height = 180;
            motw.MouseEnter += (s,e) => Mouse.OverrideCursor = Cursors.Hand;
            motw.MouseLeave += (s,e) => Mouse.OverrideCursor = Cursors.Arrow;
            motw.MouseDown += (s,e) => {
                motw.MouseUp += (S,E) => {
                    setStatus("MatchOfTheWeekScreen");
                    currentMenuStatus = 4;
                    EnterSection();
                };
            };
            Canvas.SetTop(schedule,250);
            Canvas.SetLeft(schedule,278);
            windowCanvas.Children.Add(schedule);
            Canvas.SetTop(table,242);
            Canvas.SetLeft(table,540);
            windowCanvas.Children.Add(table);
            Canvas.SetTop(stats,258);
            Canvas.SetLeft(stats,863);
            windowCanvas.Children.Add(stats);
            Canvas.SetTop(motw,419);
            Canvas.SetLeft(motw,552);
            windowCanvas.Children.Add(motw);
        }

        public void ScheduleScreen () {
            CreateScheduleScreenArrowButtons();
            TextBox gameweekNr = new TextBox();
            gameweekNr.Name = "GameWeekNr";
            TextBox gameweekSchedule = new TextBox();
            gameweekSchedule.Name = "GameWeekSchedule";
            gameweekNr.FontSize = 36;
            gameweekNr.BorderBrush = Brushes.Transparent;
            //gameweekNr.FontWeight = FontWeights.Bold;
            gameweekNr.TextAlignment = TextAlignment.Center;
            gameweekNr.Width = canvasWidth;
            gameweekNr.Text = (selectedLanguage=="polish") ? "Kolejka " : "Gameweek ";
            gameweekNr.Text += currentGameweek;
            gameweekNr.Foreground = (selectedTheme=="light") ? Brushes.Black : Brushes.White;
            gameweekNr.Background = Brushes.Transparent;
            gameweekNr.HorizontalAlignment = HorizontalAlignment.Center;
            Canvas.SetTop(gameweekNr,225);
            Canvas.SetLeft(gameweekNr,0);
            windowCanvas.Children.Add(gameweekNr);
            gameweekSchedule.FontSize = 18;
            gameweekSchedule.BorderBrush = Brushes.Transparent;
            gameweekSchedule.Foreground = (selectedTheme=="light") ? Brushes.Black : Brushes.White;
            gameweekSchedule.Background = Brushes.Transparent;
            foreach (Fixture match in database.Schedule) {
                if (match.Gameweek==currentGameweek) {
                    gameweekSchedule.Text += match.Date+", ";
                    gameweekSchedule.Text += match.Hour+"\t";
                    string home = match.Home;
                    while (home.Length<20)
                        home = " "+home;
                    gameweekSchedule.Text += home+"  ";
                    gameweekSchedule.Text += match.Score+"  ";
                    gameweekSchedule.Text += match.Visitor+"\n";
                }
            }
            Canvas.SetTop(gameweekSchedule,300);
            Canvas.SetLeft(gameweekSchedule,320);
            windowCanvas.Children.Add(gameweekSchedule);
        }

        public void ScheduleScreenChange (string arrow) {
            if (arrow=="left") {
                if (currentGameweek>1) {
                    currentGameweek--;
                    var child = (UIElement)LogicalTreeHelper.FindLogicalNode(windowCanvas,"GameWeekNr");
                    windowCanvas.Children.Remove(child);
                    var child2 = (UIElement)LogicalTreeHelper.FindLogicalNode(windowCanvas,"GameWeekSchedule");
                    windowCanvas.Children.Remove(child2);
                    ScheduleScreen();
                }
            }
            else if (arrow=="right") {
                if (currentGameweek<12) {
                    currentGameweek++;
                    var child = (UIElement)LogicalTreeHelper.FindLogicalNode(windowCanvas,"GameWeekNr");
                    windowCanvas.Children.Remove(child);
                    var child2 = (UIElement)LogicalTreeHelper.FindLogicalNode(windowCanvas,"GameWeekSchedule");
                    windowCanvas.Children.Remove(child2);
                    ScheduleScreen();
                }
            }
        }

        public void LeagueTableScreen () {
            int marginTop = 30;
            TextBox leagueTable = new TextBox();
            leagueTable.Name = "LeagueTable";
            leagueTable.FontSize = 16;
            leagueTable.FontWeight = FontWeights.Bold;
            if (selectedTheme=="light")
                leagueTable.Foreground = Brushes.Black;
            else
                leagueTable.Foreground = Brushes.White;
            leagueTable.BorderBrush = Brushes.Transparent;
            leagueTable.Background = Brushes.Transparent;
            if (selectedLanguage == "polish")
                leagueTable.Text+="#\t"+"Drużyna\t\t\t\t"+"M\t"+"W\t"+"R\t"+"P\t"+"BZ\t"+"BS\t"+"RB\t"+"P\t\t"+"\n\n";
            else
                leagueTable.Text+="#\t"+"Team\t\t\t\t"+"M\t"+"W\t"+"D\t"+"L\t"+"GS\t"+"GC\t"+"GD\t"+"P\t\t"+"\n\n";
            Canvas.SetTop(leagueTable,170);
            Canvas.SetLeft(leagueTable,250);
            windowCanvas.Children.Add(leagueTable);
            foreach (Club club in database.Table) {
                TextBox NewClub = new TextBox();
                NewClub.FontSize = 16;
                NewClub.BorderBrush = Brushes.Transparent;
                NewClub.Background = Brushes.Transparent;
                if (club.Position<5) {
                    if (selectedTheme=="light")
                    NewClub.Foreground = Brushes.DarkGreen;
                    else
                    NewClub.Foreground = Brushes.LightGreen; }
                else if (club.Position<7) {
                    if (selectedTheme=="light")
                    NewClub.Foreground = Brushes.DarkBlue;
                    else
                    NewClub.Foreground = Brushes.LightBlue; }
                else if (club.Position<18) {
                    if (selectedTheme=="light")
                    NewClub.Foreground = Brushes.Black;
                    else
                    NewClub.Foreground = Brushes.White; }
                else {
                    if (selectedTheme=="light")
                    NewClub.Foreground = Brushes.DarkRed;
                    else
                    NewClub.Foreground = Brushes.IndianRed; }
                string Klub = "";
                Klub += club.Position + "\t";
                while(club.Name.Length<29)
                    club.Name += " ";
                Klub += club.Name;
                Klub += club.FixturesPlayed + "\t";
                string mw = club.FixturesWon.ToString();
                while (mw.Length<2)
                    mw+=" ";
                Klub += mw + "\t";
                string md = club.FixturesDrawn.ToString();
                while (md.Length<2)
                    md+=" ";
                Klub += md + "\t";
                string mp = club.FixturesLost.ToString();
                while (mp.Length<2)
                    mp+=" ";
                Klub += mp + "\t";
                string gz = club.GoalsScored.ToString();
                while (gz.Length<2)
                    gz+=" ";
                Klub += gz + "\t";
                string gs = club.GoalsConceded.ToString();
                while (gs.Length<2)
                    gs+=" ";
                Klub += gs + "\t";
                string rb = club.GoalDifference.ToString();
                while (rb.Length<2)
                    rb+=" ";
                Klub += rb + "\t";
                string points = club.Points.ToString();
                while (points.Length<2)
                    points+=" ";
                Klub += club.Points;
                Klub += "\n";
                NewClub.Text += Klub;
                marginTop += 18;
                Canvas.SetTop(NewClub,150+marginTop);
                Canvas.SetLeft(NewClub,250);
                windowCanvas.Children.Add(NewClub);
            }
        }
    }
}
