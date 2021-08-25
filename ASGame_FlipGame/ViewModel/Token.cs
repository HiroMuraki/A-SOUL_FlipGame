using System;

namespace ASGame_FlipGame.ViewModel {
    public class Token : ViewModelBase {
        private TokenSide _side;
        private Coordinate _coordinate;
        private object _frontBase;
        private object _backBase;
        private object _frontContent;
        private object _backContent;

        #region 
        public event EventHandler<FlippedEventArgs> Fliped;
        #endregion

        #region 公共属性
        public object FrontBase {
            get {
                return _frontBase;
            }
            set {
                _frontBase = value;
                OnPropertyChanged(nameof(FrontBase));
            }
        }
        public object FrontContent {
            get {
                return _frontContent;
            }
            set {
                _frontContent = value;
                OnPropertyChanged(nameof(FrontContent));
            }
        }
        public object BackBase {
            get {
                return _backBase;
            }
            set {
                _backBase = value;
                OnPropertyChanged(nameof(BackBase));
            }
        }
        public object BackContent {
            get {
                return _backContent;
            }
            set {
                _backContent = value;
                OnPropertyChanged(nameof(BackContent));
            }
        }
        public TokenSide Side {
            get {
                return _side;
            }
        }
        public Coordinate Coordinate {
            get {
                return _coordinate;
            }
            set {
                _coordinate = value;
                OnPropertyChanged(nameof(Coordinate));
            }
        }
        #endregion

        public void Flip() {
            SwitchSide();
            Fliped?.Invoke(this, new FlippedEventArgs(_coordinate));
        }
        public void SwitchSide() {
            if (_side == TokenSide.Front) {
                _side = TokenSide.Back;
            }
            else if (_side == TokenSide.Back) {
                _side = TokenSide.Front;
            }
            OnPropertyChanged(nameof(Side));
        }
    }
}