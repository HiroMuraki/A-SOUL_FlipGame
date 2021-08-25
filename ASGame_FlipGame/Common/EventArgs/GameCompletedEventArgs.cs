using System;

namespace ASGame_FlipGame {
    public class GameCompletedEventArgs : EventArgs {
        public TimeSpan Time { get; }
        public GameCompletedEventArgs(TimeSpan time) {
            Time = time;
        }
    }
}