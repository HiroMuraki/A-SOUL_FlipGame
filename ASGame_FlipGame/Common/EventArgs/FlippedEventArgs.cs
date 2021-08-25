using System;

namespace ASGame_FlipGame {
    public class FlippedEventArgs : EventArgs {
        public Coordinate Coordinate { get; }

        public FlippedEventArgs(Coordinate coordinate) {
            Coordinate = coordinate;
        }
    }
}