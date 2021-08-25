using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ASGame_FlipGame.ViewModel {
    public class GameSetter : INotifyPropertyChanged {
        public readonly static List<object> AContents = new List<object>() {
            new BitmapImage(new Uri("/ASGame_FlipGame;component/Resources/Images/AvaToken/A1.png", UriKind.Relative)),

        };
        public readonly static List<object> BContents = new List<object>() {
            new BitmapImage(new Uri("/ASGame_FlipGame;component/Resources/Images/BellaToken/B1.png", UriKind.Relative)),


        };
        public readonly static List<object> CContents = new List<object>() {
            new BitmapImage(new Uri("/ASGame_FlipGame;component/Resources/Images/CarolToken/C1.png", UriKind.Relative)),


        };
        public readonly static List<object> DContents = new List<object>() {
            new BitmapImage(new Uri("/ASGame_FlipGame;component/Resources/Images/DianaToken/D1.png", UriKind.Relative)),

        };
        public readonly static List<object> EContents = new List<object>() {
            new BitmapImage(new Uri("/ASGame_FlipGame;component/Resources/Images/EileenToken/E1.png", UriKind.Relative)),

        };

        #region 后备字段
        private static GameSetter _singletonInstance;
        private readonly static object _singletonLocker = new object();
        private int _rowSize;
        private int _columnSize;
        private int _mineSize;
        private GameTheme _gameTheme;
        #endregion

        #region 公开事件
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region 公共属性
        public GameTheme GameTheme {
            get {
                return _gameTheme;
            }
            set {
                _gameTheme = value;
                OnPropertyChanged(nameof(GameTheme));
            }
        }
        public int RowSize {
            get {
                return _rowSize;
            }
            set {
                _rowSize = value;
                OnPropertyChanged(nameof(RowSize));
                OnPropertyChanged(nameof(MaxInitFliped));
                OnPropertyChanged(nameof(InitFliped));
            }
        }
        public int ColumnSize {
            get {
                return _columnSize;
            }
            set {
                _columnSize = value;
                OnPropertyChanged(nameof(ColumnSize));
                OnPropertyChanged(nameof(MaxInitFliped));
                OnPropertyChanged(nameof(InitFliped));
            }
        }
        public int InitFliped {
            get {
                return _mineSize;
            }
            set {
                _mineSize = value;
                OnPropertyChanged(nameof(InitFliped));
            }
        }
        public int MinRowSize { get { return 3; } }
        public int MaxRowSize { get { return 7; } }
        public int MinColumnSize { get { return 3; } }
        public int MaxColumnSize { get { return 7; } }
        public int MinInitFliped { get { return 0; } }
        public int MaxInitFliped { get { return _rowSize * _columnSize / 2; } }
        #endregion

        private GameSetter() {
            _rowSize = 9;
            _columnSize = 9;
            _mineSize = 10;
            GameTheme = GameTheme.AS;
        }
        public static GameSetter GetInstance() {
            if (_singletonInstance == null) {
                lock (_singletonLocker) {
                    if (_singletonInstance == null) {
                        _singletonInstance = new GameSetter();
                    }
                }
            }
            return _singletonInstance;
        }
        public static GameTheme GetRandomTheme() {
            Random rnd = new Random();
            switch (rnd.Next(0, 6)) {
                case 0:
                    return GameTheme.AS;
                case 1:
                    return GameTheme.Ava;
                case 2:
                    return GameTheme.Bella;
                case 3:
                    return GameTheme.Carol;
                case 4:
                    return GameTheme.Diana;
                case 5:
                    return GameTheme.Eileen;
                default:
                    return GameTheme.AS;
            }
        }
        public static Tuple<List<object>[], object[]> GetRandomContents(int numContent) {
            List<object>[] selectedContents = new List<object>[numContent];
            object[] selectedBase = new object[numContent];
            var avaliableContents = new List<List<object>>(){
                AContents,
                BContents,
                CContents,
                DContents,
                EContents
            };
            var avaliableBase = new List<object>() {
                App.ColorDict["AvaTheme"] as Brush,
                App.ColorDict["BellaTheme"] as Brush,
                App.ColorDict["CarolTheme"] as Brush,
                App.ColorDict["DianaTheme"] as Brush,
                App.ColorDict["EileenTheme"] as Brush,
            };
            Random rnd = new Random();
            for (int i = 0; i < numContent; i++) {
                // 随机选择一个序列ID
                int contentId = rnd.Next(0, avaliableContents.Count);
                // 设置选择内容
                selectedContents[i] = avaliableContents[contentId];
                selectedBase[i] = avaliableBase[contentId];
                // 避免重复添加
                avaliableContents.Remove(selectedContents[i]);
                avaliableBase.Remove(selectedBase[i]);
            }
            return Tuple.Create(selectedContents, selectedBase);
        }
        public static int[,] LoadLayout(string layoutFilePath) {
            List<int[]> layoutInf = new List<int[]>();
            int rowSize = 0;
            int colSize = 0;
            using (StreamReader reader = new StreamReader(layoutFilePath)) {
                int row = 0;
                while (!reader.EndOfStream) {
                    string line = reader.ReadLine().Trim();
                    if (string.IsNullOrWhiteSpace(line)) {
                        continue;
                    }
                    string[] subLayout = Regex.Split(line, @"[\s]{1,}");
                    // 以第一行的列数为列数
                    if (row == 0) {
                        colSize = subLayout.Length;
                    }
                    // 从第二行开始，如果出现列数和第一行不同的行，抛出异常
                    else if (colSize != subLayout.Length) {
                        throw new FormatException();
                    }
                    layoutInf.Add(new int[colSize]);
                    for (int col = 0; col < colSize; col++) {
                        layoutInf[row][col] = Convert.ToInt32(subLayout[col]);
                    }
                    row++;
                }
                rowSize = row;
            }
            // 转化为二维数组并返回
            int[,] layout = new int[rowSize, colSize];
            for (int row = 0; row < rowSize; row++) {
                for (int col = 0; col < colSize; col++) {
                    layout[row, col] = layoutInf[row][col];
                }
            }
            return layout;
        }
        public static void SaveLayout(int[,] layout, string filePath) {
            int rowSize = layout.GetLength(0);
            int colSize = layout.GetLength(1);
            using (StreamWriter writer = new StreamWriter(filePath)) {
                for (int row = 0; row < rowSize; row++) {
                    for (int col = 0; col < colSize; col++) {
                        writer.Write($"{layout[row, col]}");
                        if (col < colSize - 1) {
                            writer.Write(' ');
                        }
                    }
                    if (row < rowSize - 1) {
                        writer.Write('\n');
                    }
                }
            }
        }
        public static void SaveLayout(IEnumerable<Token> layoutArray, int rowSize, int colSize, string filePath) {
            using (StreamWriter writer = new StreamWriter(filePath)) {
                int col = 0;
                int row = 0;
                foreach (var block in layoutArray) {
                    if (block.Side == TokenSide.Back) {
                        writer.Write(1);
                    }
                    else {
                        writer.Write(0);
                    }
                    col++;
                    writer.Write(' ');
                    if (col >= colSize) {
                        col = 0;
                        row += 1;
                        writer.Write('\n');
                    }
                    if (row >= rowSize) {
                        break;
                    }
                }
            }
        }
        public void SwitchDiffcult(StartGameInfo gameInfo) {
            switch (gameInfo) {
                case StartGameInfo.Easy:
                    RowSize = 3;
                    ColumnSize = 3;
                    InitFliped = 3;
                    break;
                case StartGameInfo.Normal:
                    RowSize = 5;
                    ColumnSize = 5;
                    InitFliped = 10;
                    break;
                case StartGameInfo.Hard:
                    RowSize = 7;
                    ColumnSize = 7;
                    InitFliped = 15;
                    break;
                case StartGameInfo.Custom:
                    break;
                default:
                    break;
            }
        }

        public void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
