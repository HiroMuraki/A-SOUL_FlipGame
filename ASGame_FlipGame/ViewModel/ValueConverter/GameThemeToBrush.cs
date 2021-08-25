using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ASGame_FlipGame.ViewModel.ValueConverter {
    [ValueConversion(typeof(GameTheme), typeof(Brush))]
    public class GameThemeToBrush : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Brush brush = App.ColorDict["ASTheme"] as Brush;
            try {
                switch ((GameTheme)value) {
                    case GameTheme.AS:
                        brush = App.ColorDict["ASTheme"] as Brush;
                        break;
                    case GameTheme.Ava:
                        brush = App.ColorDict["AvaTheme"] as Brush;
                        break;
                    case GameTheme.Bella:
                        brush = App.ColorDict["BellaTheme"] as Brush;
                        break;
                    case GameTheme.Carol:
                        brush = App.ColorDict["CarolTheme"] as Brush;
                        break;
                    case GameTheme.Diana:
                        brush = App.ColorDict["DianaTheme"] as Brush;
                        break;
                    case GameTheme.Eileen:
                        brush = App.ColorDict["EileenTheme"] as Brush;
                        break;
                    default:
                        break;
                }
                return brush;

            }
            catch (Exception) {
                return App.ColorDict["AS"] as Brush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
