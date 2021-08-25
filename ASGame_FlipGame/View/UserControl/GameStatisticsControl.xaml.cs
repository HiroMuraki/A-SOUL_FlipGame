using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ASGame_FlipGame.View {
    /// <summary>
    /// GameStatisticsControl.xaml 的交互逻辑
    /// </summary>
    public partial class GameStatisticsControl : UserControl, INotifyPropertyChanged {
        private int _rowSize;
        private int _columnSize;
        private int _initFliped;
        private double _time;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int RowSize {
            get {
                return _rowSize;
            }
        }
        public int ColumnSize {
            get {
                return _columnSize;
            }
        }
        public int InitFliped {
            get {
                return _initFliped;
            }
        }
        public double Time {
            get {
                return _time;
            }
        }

        public GameStatisticsControl() {
            InitializeComponent();
        }

        public void Display(int rowSize, int columnSize, int initFliped, TimeSpan time) {
            IsHitTestVisible = true;
            _rowSize = rowSize;
            _columnSize = columnSize;
            _initFliped = initFliped;
            _time = time.TotalSeconds;
            OnPropertyChanged(nameof(RowSize));
            OnPropertyChanged(nameof(ColumnSize));
            OnPropertyChanged(nameof(InitFliped));
            OnPropertyChanged(nameof(Time));
            DoubleAnimation animation = new DoubleAnimation() {
                To = 1,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(200)
            };
            BeginAnimation(OpacityProperty, animation);
        }
        public void Hide() {
            IsHitTestVisible = false;
            DoubleAnimation animation = new DoubleAnimation() {
                To = 0,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.8,
                Duration = TimeSpan.FromMilliseconds(200)
            };
            BeginAnimation(OpacityProperty, animation);
        }
    }
}
