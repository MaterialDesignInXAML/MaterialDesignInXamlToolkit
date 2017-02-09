using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaterialDesignThemes.Wpf.Transitions
{
    public static class TransitionOrigins
    {
        public static readonly Point TopLeft = new Point(0, 0);
        public static readonly Point TopRight = new Point(1, 0);

        public static readonly Point BottomLeft = new Point(0, 1);
        public static readonly Point BottomRight = new Point(1, 1);

        public static readonly Point Center = new Point(0.5, 0.5);

        public static readonly Point MiddleLeft = new Point(0, 0.5);
        public static readonly Point MiddleRight = new Point(1, 0.5);

        public static readonly Point TopMiddle = new Point(0.5, 0);
        public static readonly Point BottomMiddle = new Point(0.5, 1);
    }
}
