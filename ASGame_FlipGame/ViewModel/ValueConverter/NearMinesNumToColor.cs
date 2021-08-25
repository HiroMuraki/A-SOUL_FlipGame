using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ASGame_FlipGame.ViewModel.ValueConverter {
    [ValueConversion(typeof(int), typeof(Brush))]
    public class NearMinesNumToColor : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int val = (int)value;
            Color color;
            switch (val) {
                case 0:
                    return null;
                case 1:
                    color = Color.FromRgb(0x89, 0x89, 0x89);
                    break;
                case 2:
                    color = Color.FromRgb(0x3d, 0xad, 0xe6);
                    break;
                case 3:
                    color = Color.FromRgb(0x00, 0x8d, 0x09);
                    break;
                case 4:
                    color = Color.FromRgb(0x00, 0xad, 0xb3);
                    break;
                case 5:
                    color = Color.FromRgb(0x00, 0x41, 0xb3);
                    break;
                case 6:
                    color = Color.FromRgb(0x5f, 0x00, 0xb3);
                    break;
                case 7:
                    color = Color.FromRgb(0xb3, 0x00, 0xa1);
                    break;
                case 8:
                    color = Color.FromRgb(0xb3, 0x00, 0x00);
                    break;
                case 9:
                    color = Color.FromRgb(0xe6, 0x3d, 0x59);
                    break;
                default:
                    return null;
            }
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
