using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;

namespace AppFriU
{
    public class AI : Player
    {
        enum StepAI
        {
            steepGor = 0,
            steepVert = 1,
            steepAll = 2,
        }

        Random _rndABC;
        List<string> _listWord;
        List<string> _listWordsCellTemp;
        string _wordTemp;
        int _steepAIonCell;


        public AI()
        {
            this.Name = "AI";
            WordDict();
            _steepAIonCell = (int) StepAI.steepVert;
        }
       /// <summary>
        /// Ходит ИИ.
        /// </summary>
        public string SteepAI()
        {
            _rndABC = new Random();
            if (_listWordsCellTemp.Count>0)
            {
                int i = _rndABC.Next(_listWordsCellTemp.Count-1);
                var buk = _listWordsCellTemp[i].Substring(_wordTemp.Length).ToCharArray()[0];
                return buk.ToString();//_listWordsCellTemp[i].Substring(2);
            }
            // _rndABC.Next(31);
            //UTF-8 Hex-410 Dec - 1040
            return randomABC();
        }

        private string randomABC()
        {
            string abc;
            //do
            //{
            //    abc = Convert.ToChar(Global.KEY_UTF8 + _rndABC.Next(31)).ToString();
            //}
            //while (abc = "Ъ" ||
            //       abc = "Ь" ||
            //       abc = "Й" ||
            //       abc = "Ы" ||
            //       abc = "Э");
            abc = Convert.ToChar(Global.KEY_UTF8 + _rndABC.Next(31)).ToString();
            return abc;
        }

        /// <summary>
        /// Выбирает клетку для текущего хода ИИ.
        /// </summary>
        /// <param name="itemsSource"></param>
        /// <returns></returns>
        internal String RndButton(IEnumerable itemsSource, string lastSteepPlayer)
        {
            int ind = 0;
            _rndABC = new Random();
            int x = 0;
            int y = 0;
            int isCoord = 0;
            var itemS = itemsSource as List<List<Button>>;
            string nameBtn = "";


            for (int i = 0; i < Global.GOR_CELL; i++)
            {
                for (int j = 0; j < Global.VERT_CELL; j++)
                {
                    ind++;
           
                    if (itemS[i][j].Content == null && isCoord == 0)
                    {
                        x = i;
                        y = j;
                        isCoord = 1;
                    }
                    if (lastSteepPlayer != "" && lastSteepPlayer == itemS[i][j].Name)
                    {
                        nameBtn = FindWordInButton(itemS, i, j);
                        if (nameBtn != "")
                            return nameBtn;

                    }

                }
            }
       
            _steepAIonCell = (int)StepAI.steepAll;
            CheckWordCell(itemS, x, y);
            return itemS[x][y].Name.ToString();
           
        }


        private string FindWordInButton(List<List<Button>> itemS, int i, int j)
        {
            string wordGor = Word.FindWordGor(i, itemS);
            string wordVert = Word.FindWordVert(j, itemS);
            if (wordGor.Length >= wordVert.Length)
            {
                //смотрит левее и если не пустая клетка то смотрим на клетку правее.
                if ((j == 0 &&
                     itemS[i][j + 1].Content == null) ||
                    (j - 1 >= 0 &&
                    j + 1 < Global.GOR_CELL &&
                    itemS[i][j - 1].Content != null &&
                    itemS[i][j + 1].Content == null))
                {
                    _steepAIonCell = (int)StepAI.steepGor;
                    if (CheckWordCell(itemS, i, j + 1))
                    {
                        return itemS[i][j + 1].Name.ToString();
                    }
                }
            }
            else
            {
                if ((i == 0 &&
                    itemS[i + 1][j].Content == null) ||
                       //смотрит выше и если не пустая клетка тосмотрим на клетку ниже.
                       (i - 1 >= 0 &&
                      i + 1 < Global.VERT_CELL &&
                      itemS[i - 1][j].Content != null &&
                      itemS[i + 1][j].Content == null))
                {
                    _steepAIonCell = (int)StepAI.steepVert;
                    if (CheckWordCell(itemS, i + 1, j))
                    {
                        return itemS[i + 1][j].Name.ToString();
                    }
                }
            }
            return "";
        }


        /// <summary>
        /// Производит поиск слов для хода ИИ.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="gor"></param>
        /// <param name="ver"></param>
        private bool CheckWordCell(List<List<Button>> item, int gor, int vert)
        {
            _wordTemp = "";
            bool isAll = false;
            //Ищет слово по строке.
            //  if (vert == Global.VERT_CELL-1)
            if (_steepAIonCell == (int)StepAI.steepGor ||
                _steepAIonCell == (int)StepAI.steepAll)
            {
                for (int j = 0; j < Global.GOR_CELL - 1; j++)
                {
                    if (item[gor][j].Content != null)
                    {
                        _wordTemp += item[gor][j].Content.ToString();
                    }
                }
            }
            if (_steepAIonCell == (int)StepAI.steepAll &&
                _wordTemp.Length > 0)
                isAll = true;

                //Ищет слово по столбцу.
                //  if (gor == Global.GOR_CELL - 1 && _wordTemp.Length == 0)
                if (_steepAIonCell == (int)StepAI.steepVert ||
                (_steepAIonCell == (int)StepAI.steepAll &&
                isAll == false))
            {
                //  if (_wordTemp.Length == 0)
               for (int i = 0; i < Global.VERT_CELL; i++)
                {
                    if (item[i][vert].Content != null)
                    {
                        _wordTemp += item[i][vert].Content.ToString();
                    }
                }
            }
            _listWordsCellTemp = new List<string>();
            if (_listWord != null)
            {
                foreach (var lWord in _listWord)
                {
                    if (lWord.StartsWith(_wordTemp))
                        _listWordsCellTemp.Add(lWord);
                }
            }
            if (_listWordsCellTemp.Count > 0)
                return true;
            return false;
        }


        /// <summary>
        /// Подключает словарь.
        /// </summary>
        private async void WordDict()
        {
             _listWord = await FileStorage.ReadFile(Global.TYPE_DICT);
        }

    }
}
