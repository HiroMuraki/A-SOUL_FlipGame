using System;
using System.Globalization;
using System.Windows.Data;

namespace ASGame_FlipGame.ViewModel.ValueConverter {
    [ValueConversion(typeof(GameTheme), typeof(Uri))]
    public class GameThemeToBackground : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            switch ((GameTheme)value) {
                case GameTheme.AS:
                    return new Uri("/ASGame_FlipGame;component/Resources/Images/Background_ASTheme.jpg", UriKind.Relative);
                case GameTheme.Ava:
                    return new Uri("/ASGame_FlipGame;component/Resources/Images/Background_AvaTheme.jpg", UriKind.Relative);
                case GameTheme.Bella:
                    return new Uri("/ASGame_FlipGame;component/Resources/Images/Background_BellaTheme.jpg", UriKind.Relative);
                case GameTheme.Carol:
                    return new Uri("/ASGame_FlipGame;component/Resources/Images/Background_CarolTheme.jpg", UriKind.Relative);
                case GameTheme.Diana:
                    return new Uri("/ASGame_FlipGame;component/Resources/Images/Background_DianaTheme.jpg", UriKind.Relative);
                case GameTheme.Eileen:
                    return new Uri("/ASGame_FlipGame;component/Resources/Images/Background_EileenTheme.jpg", UriKind.Relative);
                default:
                    return new Uri("/ASGame_FlipGame;component/Resources/Images/Background_ASTheme.jpg", UriKind.Relative);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
