using System;
using System.Collections.Generic;
using System.Text;

namespace AppFriU
{
    public class Player
    {
        public string Name { get; set; }
        /// <summary>
        /// Лист слов игрока.
        /// </summary>
        private List<String> _listWords;
        /// <summary>
        /// Признак сходил/не сходил игрок.
        /// </summary>
        private bool _isSteep;

        public Player()
        {
            _listWords = new List<String>();
            Name = "Игрок";
        }
        
        public void AddWord(string word)
        {
            if (!_listWords.Contains(word))
               _listWords.Add(word);
        }

        public bool CheckWord(string word)
        {
            if (_listWords.Contains(word))
              return true;
            return false;
        }

        public void DeleteWord()
        {
            foreach (var item in _listWords)
            {
                _listWords.Remove(item);
            }
        }

        /// <summary>
        /// Подсчитывает количество слов.
        /// </summary>
        /// <returns></returns>
        public int CountWord()
        {
            return _listWords.Count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isBool"></param>
        public void GetSteep(bool isBool)
        {
            _isSteep = isBool;
        }

        /// <summary>
        /// Получает признак сходил / не сходил игрок.
        /// </summary>
        /// <returns></returns>
        public bool SetSteep()
        {
            return _isSteep;
        }

    }
}
