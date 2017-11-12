using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace AppFriU
{
    public static class Global
    {
        public static int KEY_UTF8 = 1040;

        public static SolidColorBrush COLOR_PPLAYER1 = new SolidColorBrush { Color = Colors.DarkGreen};
        public static SolidColorBrush COLOR_AI = new SolidColorBrush { Color = Colors.Red};
        public static SolidColorBrush NO_COLOR = new SolidColorBrush { Color = Colors.Transparent };
        public static SolidColorBrush DIALOG_COLOR = new SolidColorBrush { Color = Colors.LightBlue };

        public static string FILENAME_DICT_FOURWORD = @"dictFourWord.txt";
        public static string FILENAME_DICT_FRIWORD = @"dictFriWord.txt";
        public static string FILENAME_DICT_FAINWORD = @"dictFainWord.txt";

        public static string TYPE_DICT = FILENAME_DICT_FRIWORD;

        public static int NUMBER_WORD_FRI = 3;
        public static int NUMBER_WORD_FOUR = 4;
        public static int NUMBER_WORD_FAIN = 5;

        public static int GOR_CELL = NUMBER_WORD_FRI, VERT_CELL = NUMBER_WORD_FRI;

    }
}
