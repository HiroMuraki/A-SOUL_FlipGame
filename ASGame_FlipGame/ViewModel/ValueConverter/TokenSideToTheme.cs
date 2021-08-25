using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ASGame_FlipGame.ViewModel.ValueConverter {
    public class TokenSideToTheme : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (((string)parameter).ToUpper() == "FRONT") {
                return Game.GetInstance().FrontBase;
            }
            else if (((string)parameter).ToUpper() == "BACK") {
                return Game.GetInstance().BackBase;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
