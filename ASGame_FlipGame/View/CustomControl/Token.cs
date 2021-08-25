using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ASGame_FlipGame.View {
    public class Token : Button {
        public static readonly DependencyProperty SideProperty =
            DependencyProperty.Register(nameof(Side), typeof(TokenSide), typeof(Token), new PropertyMetadata(TokenSide.Front));
        public static readonly DependencyProperty FrontContentProperty =
            DependencyProperty.Register(nameof(FrontContent), typeof(object), typeof(Token), new PropertyMetadata(0));
        public static readonly DependencyProperty BackContentProperty =
            DependencyProperty.Register(nameof(BackContent), typeof(object), typeof(Token), new PropertyMetadata(1));
        public static readonly DependencyProperty CoordinateProperty =
            DependencyProperty.Register(nameof(Coordinate), typeof(Coordinate), typeof(Token), new PropertyMetadata(new Coordinate(0, 0)));
        
        public TokenSide Side {
            get { return (TokenSide)GetValue(SideProperty); }
            set { SetValue(SideProperty, value); }
        }
        public object FrontContent {
            get { return (object)GetValue(FrontContentProperty); }
            set { SetValue(FrontContentProperty, value); }
        }
        public object BackContent {
            get { return (object)GetValue(BackContentProperty); }
            set { SetValue(BackContentProperty, value); }
        }
        public Coordinate Coordinate {
            get { return (Coordinate)GetValue(CoordinateProperty); }
            set { SetValue(CoordinateProperty, value); }
        }


        static Token() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Token), new FrameworkPropertyMetadata(typeof(Token)));
        }
    }
}
