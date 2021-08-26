using ASGame_FlipGame.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace ASGame_FlipGame {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public GameTheme Theme {
            get { return (GameTheme)GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }
        public static readonly DependencyProperty ThemeProperty =
            DependencyProperty.Register(nameof(Theme), typeof(GameTheme), typeof(MainWindow), new PropertyMetadata(GameTheme.AS));

        public Game Game { get; set; }
        public GameSetter GameSetter { get; set; }
        public GameSoundPlayer GameSound { get; set; }

        public MainWindow() {
            Game = Game.GetInstance();
            GameSetter = GameSetter.GetInstance();
            GameSound = GameSoundPlayer.GetInstance();
            Game.GameCompleted += Game_GameCompleted;
            InitializeComponent();
            GridRoot.MaxHeight = SystemParameters.WorkArea.Height + 1;
            GridRoot.MaxWidth = SystemParameters.WorkArea.Width + 1;
            ExpandSetterPanel();
            StartGame_Click(null, new StartGameEventArgs(StartGameInfo.Normal));
            GameSound.PlayMusic();
        }

        private void StartGame_Click(object sender, StartGameEventArgs e) {
            try {
                Theme = GameSetter.GetRandomTheme();
                GameSetter.SwitchDiffcult(e.StartInfo);
                var selected = GameSetter.GetRandomContents(2);
                Game.FrontContentPool = selected.Item1[0];
                Game.BackContentPool = selected.Item1[1];
                Game.FrontBase = selected.Item2[0];
                Game.BackBase = selected.Item2[1];
                Game.Start(GameSetter.RowSize, GameSetter.ColumnSize);
                Game.Shuffle(GameSetter.InitFliped);
                GameStatistics.Hide();
                if (Topmost) {
                    FoldSetterPanel();
                }
            }
            catch (Exception exp) {
                MessageTip.DisplayTip($"文件读取错误：{exp.Message}", TimeSpan.FromSeconds(2));
            }
        }
        private void RestoreGame_Click(object sender, DragEventArgs e) {
            string layoutFile = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            RestoreGame(layoutFile);
        }
        private void RestoreGame_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.CurrentDirectory;
            ofd.Filter = "存档文件|*.txt";
            if (ofd.ShowDialog() == true) {
                RestoreGame(ofd.FileName);
            }
        }
        private void SaveGame_Click(object sender, RoutedEventArgs e) {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "LightLayout.txt";
            if (sfd.ShowDialog() == true) {
                GameSetter.SaveLayout(Game.Tokens, Game.RowSize, Game.ColumnSize, sfd.FileName);
                MessageTip.DisplayTip($"保存完成：{sfd.FileName}", TimeSpan.FromSeconds(2));
            }
        }
        private void Game_GameCompleted(object sender, GameCompletedEventArgs e) {
            GameStatistics.Display(Game.RowSize, Game.ColumnSize, Game.InitFliped, e.Time);
        }
        private void ExpandSetterPanel_Click(object sender, RoutedEventArgs e) {
            if (SetterArea.Width != 50) {
                FoldSetterPanel();
            }
            else {
                ExpandSetterPanel();
            }
        }
        private void LockToTop_Click(object sender, RoutedEventArgs e) {
            Topmost = !Topmost;
        }

        private void Token_Flip(object sender, RoutedEventArgs e) {
            Game.Flip((sender as View.Token).Coordinate);
            GameSound.PlayFlipFXSound();
        }

        private void Window_Minimum(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }
        private void Window_Maximum(object sender, RoutedEventArgs e) {
            if (WindowState == WindowState.Normal) {
                WindowState = WindowState.Maximized;
            }
            else {
                WindowState = WindowState.Normal;
            }
        }
        private void Window_Close(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
        private void Window_Move(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount >= 2) {
                if (WindowState == WindowState.Normal) {
                    WindowState = WindowState.Maximized;
                }
                else {
                    WindowState = WindowState.Normal;
                }
            }
            else {
                DragMove();
            }
        }
        private void Window_DragEnter(object sender, DragEventArgs e) {
            FileLoader.IsHitTestVisible = true;
        }
        private void Window_DragLeave(object sender, DragEventArgs e) {
            FileLoader.IsHitTestVisible = false;
        }
        private void Window_Drop(object sender, DragEventArgs e) {
            FileLoader.IsHitTestVisible = false;
        }

        private void FoldSetterPanel() {
            DoubleAnimation widthAnimation = new DoubleAnimation() {
                To = 50,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(150)
            };
            DoubleAnimation opacityAnimation = new DoubleAnimation() {
                To = 0,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(150)
            };
            SetterArea.BeginAnimation(WidthProperty, widthAnimation);
            SetterPanel.BeginAnimation(OpacityProperty, opacityAnimation);
            SetterPanel.IsHitTestVisible = false;
        }
        private void ExpandSetterPanel() {
            DoubleAnimation widthAnimation = new DoubleAnimation() {
                To = 190,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(150)
            };
            DoubleAnimation opacityAnimation = new DoubleAnimation() {
                To = 1,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(150)
            };
            SetterArea.BeginAnimation(WidthProperty, widthAnimation);
            SetterPanel.BeginAnimation(OpacityProperty, opacityAnimation);
            SetterPanel.IsHitTestVisible = true;
        }
        private void RestoreGame(string layoutFile) {
            try {
                int[,] layoutInf = GameSetter.LoadLayout(layoutFile);
                Game.Start(layoutInf);
                MessageTip.DisplayTip("游戏加载完成", TimeSpan.FromSeconds(1));
                GameStatistics.Hide();
            }
            catch (Exception exp) {
                MessageTip.DisplayTip($"文件读取错误：{exp.Message}", TimeSpan.FromSeconds(2));
            }
        }
    }
}
