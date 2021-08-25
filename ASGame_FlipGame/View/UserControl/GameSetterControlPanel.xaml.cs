using ASGame_FlipGame.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ASGame_FlipGame.View {
    /// <summary>
    /// GameSetterControlPanel.xaml 的交互逻辑
    /// </summary>
    public partial class GameSetterControlPanel : UserControl {
        private GameSetter _gameSetter;

        public static readonly DependencyProperty GameThemeProperty =
            DependencyProperty.Register(nameof(GameTheme), typeof(GameTheme), typeof(GameSetterControlPanel), new PropertyMetadata(GameTheme.AS));

        public event EventHandler<StartGameEventArgs> StartGame;

        public GameTheme GameTheme {
            get { return (GameTheme)GetValue(GameThemeProperty); }
            set { SetValue(GameThemeProperty, value); }
        }

        public GameSetter GameSetter {
            get {
                return _gameSetter;
            }
        }
        public GameSetterControlPanel() {
            _gameSetter = GameSetter.GetInstance();
            InitializeComponent();
        }

        private void StartGame_Click(object sender, RoutedEventArgs e) {
            StartGameInfo inf;
            switch ((sender as Button).Name) {
                case "Easy":
                    inf = StartGameInfo.Easy;
                    break;
                case "Normal":
                    inf = StartGameInfo.Normal;
                    break;
                case "Hard":
                    inf = StartGameInfo.Hard;
                    break;
                default:
                    inf = StartGameInfo.Custom;
                    break;
            }
            StartGame?.Invoke(this, new StartGameEventArgs(inf));
            return;
        }
    }
}
