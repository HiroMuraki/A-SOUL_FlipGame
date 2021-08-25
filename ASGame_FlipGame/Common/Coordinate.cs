using System;

namespace ASGame_FlipGame {
    /// <summary>
    /// 原点位于左上角
    /// </summary>
    public readonly struct Coordinate {
        public static int TopBorder { get; set; }
        public static int BottomBorder { get; set; }
        public static int LeftBorder { get; set; }
        public static int RightBorder { get; set; }

        public int Row { get; }
        public int Column { get; }

        public Coordinate Up {
            get {
                return new Coordinate(Row - 1, Column);
            }
        }
        public Coordinate Down {
            get {
                return new Coordinate(Row + 1, Column);
            }
        }
        public Coordinate Left {
            get {
                return new Coordinate(Row, Column - 1);
            }
        }
        public Coordinate Right {
            get {
                return new Coordinate(Row, Column + 1);
            }
        }
        public bool IsInBorder {
            get {
                return
                Row >= TopBorder &&
                Row < BottomBorder &&
                Column >= LeftBorder &&
                Column < RightBorder;
            }
        }

        public Coordinate(int row, int column) {
            Row = row;
            Column = column;
        }

        public static bool operator ==(Coordinate left, Coordinate right) {
            return left.Row == right.Row && left.Column == right.Column;
        }
        public static bool operator !=(Coordinate left, Coordinate right) {
            return !(left == right);
        }
        public override bool Equals(object obj) {
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
        public override string ToString() {
            return $"({Row},{Column})";
        }
    }
}