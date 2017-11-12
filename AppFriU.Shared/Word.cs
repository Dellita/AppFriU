using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace AppFriU
{
    public static class Word
    {

        /// <summary>
        /// Проверяет, есть ли составленное слово по горизонтали 
        /// или по вертикали
        /// или по диоганали.
        /// </summary>
        /// <param name="gor">Координата строки.</param>
        /// <param name="vert">Координата столбца.</param>
        public static string CheckWord(object itemsSource, int gor, int vert)
        {
            var itemSourse = itemsSource as List<List<Button>>;
            string word = "";
            word = FindWordGor(gor, itemSourse);
            if (word.Length == Global.GOR_CELL)
            {
                return word;
            }
            //Ищет слово по столбцу.
            word = FindWordVert(vert, itemSourse);
            if (word.Length == Global.VERT_CELL)
            {
                return word;
            }

            //Ищет слово по основной диаганали.
            word = FindWordDiagonal(itemSourse);
            if (word.Length == Global.VERT_CELL)
            {
                return word;
            }

            //Ищет слово по дополнтельной диагонали.
            word = FindWordDopDiagonal(itemSourse);
            if (word.Length == Global.VERT_CELL)
            {
                return word;
            }
            return word;
        }

        private static string FindWordDopDiagonal(List<List<Button>> itemSourse)
        {
            string word = "";
            for (int i = 0; i < Global.GOR_CELL; i++)
            {
                for (int j = 0; j < Global.VERT_CELL; j++)
                {
                    if (i + j == Global.GOR_CELL + 1)
                    {
                        if (itemSourse[i][j].Content != null)
                            word += itemSourse[i][j].Content.ToString();
                    }
                }
            }

            return word;
        }

        private static string FindWordDiagonal(List<List<Button>> itemSourse)
        {
            string word = "";
            for (int i = 0; i < Global.GOR_CELL; i++)
            {
                for (int j = 0; j < Global.VERT_CELL; j++)
                {
                    if (i == j)
                    {
                        if (itemSourse[i][j].Content != null)
                            word += itemSourse[i][j].Content.ToString();
                    }
                }
            }

            return word;
        }

        public static string FindWordVert(int vert, List<List<Button>> itemSourse)
        {
            string word = "";
            for (int i = 0; i < Global.VERT_CELL; i++)
            {
                if (itemSourse[i][vert].Content != null)
                    word += itemSourse[i][vert].Content.ToString();
            }

            return word;
        }

        public static string FindWordGor(int gor, List<List<Button>> itemSourse)
        {
            string word = "";
            //Ищет слово по строке.
            for (int j = 0; j < Global.GOR_CELL; j++)
            {
                if (itemSourse[gor][j].Content != null)
                    word += itemSourse[gor][j].Content.ToString();
            }

            return word;
        }

    }
}
