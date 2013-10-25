using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using com.shephertz.app42.paas.sdk.windows.game;
using com.shephertz.app42.paas.sdk.windows;
using System.Windows;

namespace Sample
{
    public  class ScoreViewModel
    {
        public  ObservableCollection<scoreitemmodel> mScores;
        public ScoreViewModel()
        {
            mScores = new ObservableCollection<scoreitemmodel>();
        }
        public void LoadScores(IList<Game.Score> gameObj)
        {
            for (int i = 0; i < gameObj.Count; i++)
            {
                scoreitemmodel item3 = new scoreitemmodel();
                item3.UserName = gameObj[i].GetUserName();
                item3.Score = (int)gameObj[i].GetValue();
                item3.Rank = i + 1;
                mScores.Add(item3);
            }
        
        }
    }
   public class scoreitemmodel :INotifyPropertyChanged
    {
        private string _username;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                if (value != _username)
                {
                    _username = value;
                    NotifyPropertyChanged("UserName");
                }
            }
        }
        private int _score;
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                if (value != _score)
                {
                    _score = value;
                    NotifyPropertyChanged("Score");
                }
            }
        }

        private int _rank;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public int Rank
        {
            get
            {
                return _rank;
            }
            set
            {
                if (value != _rank)
                {
                    _rank = value;
                    NotifyPropertyChanged("Rank");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
