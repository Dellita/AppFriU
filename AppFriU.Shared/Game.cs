using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;

namespace AppFriU
{
    public class Game
    {
        bool _running;
        int _numberSteep;

        string _message;
        Player _player1;
        AI _playerAI;
        List<string> _listWord;
        ResourceLoader _resourceloader;
        string _lastSteepLPlayer;
        Random rndSteep;

        public Game()
        {
            _resourceloader = new Windows.ApplicationModel.Resources.ResourceLoader();
            _lastSteepLPlayer = "";
            rndSteep = new Random();
        }

        /// <summary>
        /// Создает справочник слов.
        /// </summary>
        public async void WordDict()
        {
            _listWord = await FileStorage.ReadFile(Global.TYPE_DICT);
        }

        /// <summary>
        ///   Подготавливает и запускает игру.
        /// </summary>
       public void NewGame()
        {
             WordDict();
            _player1 = new Player();
            _playerAI = new AI();
            _running = true;
            _numberSteep = 0;
            if (rndSteep.Next(1) == 1)
                _player1.GetSteep(true);
            else
                _player1.GetSteep(false);
            _player1.GetSteep(Convert.ToBoolean(rndSteep.Next(1)));
            _playerAI.GetSteep(!_player1.SetSteep());
      
        }

        public void GetLastSteepPlayer(string lastSteep)
        {
            _lastSteepLPlayer = lastSteep;
        }

        /// <summary>
        /// Очередность игроков.
        /// </summary>
        internal string SteepGames(List<List<Button>> itemSourse)
        {
            CheckOverGames();
            if (!_playerAI.SetSteep())
            {
                if (_running)
                { 
                    var rndNumberBtn = _playerAI.RndButton(itemSourse, _lastSteepLPlayer);
                    ChangeAfterSteep();
                    return rndNumberBtn;
                }
            }
            return "";
        }

        public bool SetSteepAI()
        {
            return _playerAI.SetSteep();
        }

        public string  SteepAI()
        {
            if (_playerAI.SteepAI().Length > 0)
             return _playerAI.SteepAI();
            return "";
        }

        /// <summary>
        /// Проверяет окончание игры.
        /// </summary>
        public void CheckOverGames()
        {
            if (_numberSteep == Global.GOR_CELL * Global.VERT_CELL)
            {
                _message = _resourceloader.GetString("gameOver");
                if (_player1.CountWord() > _playerAI.CountWord())
                    _message = _resourceloader.GetString("gameOverPlayer");
                if (_player1.CountWord() < _playerAI.CountWord())
                    _message = _resourceloader.GetString("gameOverAI");
                _running = false;
            }
        }

        public string SetMessage()
        {
            return _message;
        }
    
        public string SetCountWordPlayerAI()
        {
            return _playerAI.CountWord().ToString();
        }

        public string SetCountWordPlayer()
        {
            return _player1.CountWord().ToString();
        }

        /// <summary>
        /// Добавляет слово игроку.
        /// </summary>
        /// <param name="word">Слово.</param>
        public void AddWordPlayers(string word)
        {
            if (this._listWord.Contains(word) && word.Length > 0)
            {
                if (_player1.SetSteep() && !_playerAI.CheckWord(word))
                {
                    _player1.AddWord(word);
                }
                if (_playerAI.SetSteep() && !_player1.CheckWord(word))
                {
                    _playerAI.AddWord(word);
                }
            }
        }


        /// <summary>
        ///  Метод производит изменения после хода игрока.
        /// </summary>
        public void ChangeAfterSteep()
        {
            _player1.GetSteep(!_player1.SetSteep());
            _playerAI.GetSteep(!_playerAI.SetSteep());
            _numberSteep++;
            this.CheckOverGames();
        }

        public bool isRunningGame()
        {
            return _running;
        }
  
    }
}
