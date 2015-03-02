using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ui
{
    public static class UiDefs
    {
        public static class Triggers
        {
            public static string Open = "Open";
            public static string Close = "Close";
            public static string OpenFinished = "OpenFinished";
            public static string CloseFinished = "CloseFinished";
        }

        public static class Window
        {
            public enum Anims
            {
                None,
                Open_SlideFromBottom,
                Close_SlideToTop,
                Open_SlideFromLeft,
                Close_SlideToRight
            }
        }

        public static class Scene
        {
            public static string Intro = "Intro";
            public static string Menu  = "Menu";
            public static string Game  = "Game";
        }   
    }
}
