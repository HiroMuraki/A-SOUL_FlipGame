using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Tokens = System.Collections.ObjectModel.ObservableCollection<ASGame_FlipGame.ViewModel.Token>;
using ContentPool = System.Collections.Generic.List<object>;
using System.Diagnostics;

namespace ASGame_FlipGame.ViewModel {
    public class Game : ViewModelBase {
        private static readonly Game _singletonInstance;
        private DateTime _startTime;
        private int _rowSize;
        private int _columnSize;
        private readonly Tokens _tokens;
        private ContentPool _frontContentPool;
        private ContentPool _backContentPool;
        private readonly Dictionary<Coordinate, Token> _coordinateMap;
        private int _initFliped;

        #region 事件
        public event EventHandler<GameCompletedEventArgs> GameCompleted;
        #endregion

        #region
        public object FrontBase { get; set; }
        public object BackBase { get; set; }
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
        public Tokens Tokens {
            get {
                return _tokens;
            }
        }
        public ContentPool FrontContentPool {
            get {
                return _frontContentPool;
            }
            set {
                _frontContentPool = value;
            }
        }
        public ContentPool BackContentPool {
            get {
                return _backContentPool;
            }
            set {
                _backContentPool = value;
            }
        }
        public int InitFliped {
            get {
                return _initFliped;
            }
        }
        public string Process {
            get {
                return null;
            }
        }
        #endregion

        #region
        public void Start(int rowSize, int columnSize) {
            _rowSize = rowSize;
            _columnSize = columnSize;
            _initFliped = 0;
            _tokens.Clear();
            _coordinateMap.Clear();
            int frontContentPoolSize = _frontContentPool?.Count ?? 0;
            int backContentPoolSize = _backContentPool?.Count ?? 0;
            Random rnd = new Random();
            for (int row = 0; row < _rowSize; row++) {
                for (int col = 0; col < _columnSize; col++) {
                    Token token = new Token();
                    token.Coordinate = new Coordinate(row, col);
                    token.Fliped += Token_Fliped;
                    // 从正面图中随机挑选
                    if (frontContentPoolSize > 0) {
                        token.FrontContent = CreateContent(_frontContentPool[rnd.Next(0, frontContentPoolSize)]);
                    }
                    else {
                        token.FrontContent = 0;
                    }
                    // 从背面图中随机挑选
                    if (backContentPoolSize > 0) {
                        token.BackContent = CreateContent(_backContentPool[rnd.Next(0, backContentPoolSize)]);
                    }
                    else {
                        token.BackContent = 1;
                    }
                    _tokens.Add(token);
                }
            }
            UpdateCoordinateMap();
            OnPropertyChanged(nameof(RowSize));
            OnPropertyChanged(nameof(ColumnSize));
            OnPropertyChanged(nameof(Tokens));
            _startTime = DateTime.Now;
        }
        public void Start(int[,] layoutInf) {
            _rowSize = layoutInf.GetLength(0);
            _columnSize = layoutInf.GetLength(1);
            _initFliped = 0;
            _tokens.Clear();
            _coordinateMap.Clear();
            int frontContentPoolSize = _frontContentPool?.Count ?? 0;
            int backContentPoolSize = _backContentPool?.Count ?? 0;
            Random rnd = new Random();
            for (int row = 0; row < _rowSize; row++) {
                for (int col = 0; col < _columnSize; col++) {
                    Token token = new Token();
                    if (layoutInf[row, col] == 1) {
                        token.SwitchSide();
                    }
                    token.Coordinate = new Coordinate(row, col);
                    token.Fliped += Token_Fliped;
                    // 从正面图中随机挑选
                    if (frontContentPoolSize > 0) {
                        token.FrontContent = CreateContent(_frontContentPool[rnd.Next(0, frontContentPoolSize)]);
                    }
                    else {
                        token.FrontContent = 0;
                    }
                    // 从背面图中随机挑选
                    if (backContentPoolSize > 0) {
                        token.BackContent = CreateContent(_backContentPool[rnd.Next(0, backContentPoolSize)]);
                    }
                    else {
                        token.BackContent = 1;
                    }
                    _tokens.Add(token);
                }
            }
            UpdateCoordinateMap();
            OnPropertyChanged(nameof(RowSize));
            OnPropertyChanged(nameof(ColumnSize));
            OnPropertyChanged(nameof(Tokens));
            _startTime = DateTime.Now;
        }

        public void Shuffle(int shuffleNums) {
            if (shuffleNums == 0) {
                return;
            }
            Random rnd = new Random();
            if (shuffleNums >= _tokens.Count - 1) {
                shuffleNums = _tokens.Count - 1;
            }
            for (int i = 0; i < shuffleNums; i++) {
                _tokens[i].SwitchSide();
            }
            for (int i = 0; i < shuffleNums; i++) {
                int next = rnd.Next(i, _tokens.Count);
                var t = _tokens[i];
                var pos1 = _tokens[i].Coordinate;
                var pos2 = _tokens[next].Coordinate;
                _tokens[i] = _tokens[next];
                _tokens[i].Coordinate = pos1;
                _tokens[next] = t;
                _tokens[next].Coordinate = pos2;
            }
            UpdateCoordinateMap();
            OnPropertyChanged(nameof(Tokens));
            _initFliped = shuffleNums;
            OnPropertyChanged(nameof(InitFliped));
        }
        public void Flip(Coordinate coordinate) {
            if (!_coordinateMap.ContainsKey(coordinate)) {
                return;
            }
            _coordinateMap[coordinate].Flip();
        }
        #endregion

        static Game() {
            _singletonInstance = new Game();
        }
        private Game() {
            _tokens = new Tokens();
            _coordinateMap = new Dictionary<Coordinate, Token>();
            _frontContentPool = new ContentPool();
            _backContentPool = new ContentPool();
        }
        public static Game GetInstance() {
            if (_singletonInstance == null) {
                throw new Exception("FATAL ERROR, RESTRAT REQUIRED");
            }
            return _singletonInstance;
        }

        private static object CreateContent(object content) {
            if (content == null) {
                return null;
            }
            if (content is BitmapImage) {
                return new Image() {
                    Source = (BitmapImage)content,
                    Stretch = Stretch.Fill
                };
            }
            return content.ToString();
        }
        private void UpdateCoordinateMap() {
            _coordinateMap.Clear();
            for (int row = 0; row < _rowSize; row++) {
                for (int col = 0; col < _columnSize; col++) {
                    _coordinateMap[_tokens[row * _columnSize + col].Coordinate] = _tokens[row * _columnSize + col];
                }
            }
        }
        private bool IsGameCompleted() {
            TokenSide checkSide;
            // 如果初始翻面数大于0，则允许正反面通关，否则仅允许反面通关
            if (_initFliped > 0) {
                checkSide = _tokens[0].Side;
            }
            else {
                checkSide = TokenSide.Back;
            }
            for (int i = 0; i < _rowSize * _columnSize; i++) {
                // 如果有一个元素面与检查面不同，返回false
                if (_tokens[i].Side != checkSide) {
                    return false;
                }
            }
            // 通过检查返回true
            return true;
        }
        private void Token_Fliped(object sender, FlippedEventArgs e) {
            if (IsCoordinateInBorder(e.Coordinate.Up)) {
                _coordinateMap[e.Coordinate.Up].SwitchSide();
            }
            if (IsCoordinateInBorder(e.Coordinate.Down)) {
                _coordinateMap[e.Coordinate.Down].SwitchSide();
            }
            if (IsCoordinateInBorder(e.Coordinate.Left)) {
                _coordinateMap[e.Coordinate.Left].SwitchSide();
            }
            if (IsCoordinateInBorder(e.Coordinate.Right)) {
                _coordinateMap[e.Coordinate.Right].SwitchSide();
            }
            if (IsGameCompleted()) {
                GameCompleted?.Invoke(this, new GameCompletedEventArgs(DateTime.Now - _startTime));
            }
        }
        private bool IsCoordinateInBorder(Coordinate coordinate) {
            return
            coordinate.Row >= 0 &&
            coordinate.Row < _rowSize &&
            coordinate.Column >= 0 &&
            coordinate.Column < _columnSize;
        }
    }
}