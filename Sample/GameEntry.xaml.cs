using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using com.shephertz.app42.paas.sdk.windows;
using com.shephertz.app42.paas.sdk.windows.game;

namespace Sample
{
    public partial class GameEntry : PhoneApplicationPage, App42Callback
    {
        static ServiceAPI serviceApi = new ServiceAPI("62f6b446a152a488b92a7cc27421e6ee105247a973246b2d528ca67f746004fc",
             "6ee0b3f11115e55b92c3eab66a5a2a94197a27cea8e43f4239748a443214a312");

        ScoreBoardService scoreBoardService = serviceApi.BuildScoreBoardService();
        GameService gameService = serviceApi.BuildGameService();
        public delegate void OnResponseCallback(string gameName,string userName,double score,App42Callback app42Callback);
        public delegate void UIcallbackEvent(IList<Game.Score> gList);
        public delegate void ShowExceptionCallbackEvent(String message);
        public Boolean boolean = false;
        public Boolean boolean1 = false;
        public delegate void CreateGame();
        String gameName = "PokerGame"; String gameUserName = "";
        String scoreId = "";
        public double scoreValue ;
        public GameEntry()
        {
            InitializeComponent();
            tbxGname.Text = gameName;
            tbxScoreId.Text = scoreId;
        }

        public void UICallback(IList<Game.Score> gList)
        {
            tblGname.Visibility = Visibility.Collapsed;
            tbxGname.Visibility = Visibility.Collapsed;
            App.mViewModel.LoadScores(gList);
        }
        public void ShowExceptionCallback(String message)
        {
            tblGname.Visibility = Visibility.Collapsed;
            tbxGname.Visibility = Visibility.Collapsed;
            tblErrorMessage.Visibility = Visibility.Visible;
            tblErrorMessage.Text = message;
        }

        //-------------------------------------------------ON-SUCCESS-----------------------------------
        public void OnSuccess(object response)
        {
            if (response is Game)
            {
                Game gameObj = (Game)response;
                Console.WriteLine("GameName is  : " + gameObj.GetName());
                gameName = gameObj.GetName();
                if (boolean)
                    Deployment.Current.Dispatcher.BeginInvoke(new ShowExceptionCallbackEvent(ShowExceptionCallback), "Score of the user " + gameObj.GetScoreList()[0].GetUserName() + " has sucessfully edited");
                else
                    Deployment.Current.Dispatcher.BeginInvoke(new ShowExceptionCallbackEvent(ShowExceptionCallback), "Score of the user " + gameObj.GetScoreList()[0].GetUserName() + " has sucessfully saved");
                if (gameObj.GetScoreList().Count > 0)
                {
                    scoreId = gameObj.GetScoreList()[0].GetScoreId();
                }
            }
        }

        //-------------------------------------------------ON-EXCEPTION---------------------------------
        public void OnException(App42Exception exception)
        {
            Deployment.Current.Dispatcher.BeginInvoke(new ShowExceptionCallbackEvent(ShowExceptionCallback), exception.Message);
            if (exception.GetAppErrorCode() == 3002)
            {
                Deployment.Current.Dispatcher.BeginInvoke(new ShowExceptionCallbackEvent(ShowExceptionCallback), "Please create your game from AppHQ Console.");
            }
            if (exception.Message.Equals("ScoreId can not be blank"))
            {
                Deployment.Current.Dispatcher.BeginInvoke(new ShowExceptionCallbackEvent(ShowExceptionCallback), "First save the score of the user.");
           
            }
        }

        //---------------------------------------------------Click EVents-------------------------------

        private void btnLeaderBoard_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/LeaderBoard.xaml", UriKind.Relative));
        }

        private void btnSaveUserScore_Click_1(object sender, RoutedEventArgs e)
        {
                gameUserName = tbxUname.Text;
                scoreValue = Convert.ToInt32(tbxScore.Text);
                boolean = false;
                scoreBoardService.SaveUserScore(gameName, gameUserName, scoreValue, this);
        }

        private void btnEditUserScore_Click_1(object sender, RoutedEventArgs e)
        {
            boolean = true;
            tbxScoreId.Text = scoreId;
            scoreBoardService.EditScoreValueById(scoreId, scoreValue, this);
        }
    }
}