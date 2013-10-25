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
        static ServiceAPI serviceApi = new ServiceAPI("e6d7c63afc8abe1739ef977363d608370ce56de49c6397ceb5910a1ff0c167b2", "bd4f6fbad9becc13e9c4cbbe50dfcf54b68b7f0d9fa94e7fa44a53418275b373");
        ScoreBoardService scoreBoardService = serviceApi.BuildScoreBoardService();
        GameService gameService = serviceApi.BuildGameService();
        public delegate void OnResponseCallback(string gameName,string userName,double score,App42Callback app42Callback);
        public delegate void UIcallbackEvent(IList<Game.Score> gList);
        public delegate void ShowExceptionCallbackEvent(String message);
        public Boolean boolean = false;
        public Boolean boolean1 = false;
        public delegate void CreateGame();
        String gameName = "TestGAME"; String gameUserName = "";
        String scoreId = "";
        public double scoreValue ;
        public GameEntry()
        {
            InitializeComponent();
            serviceApi.SetBaseURL("http://", "localhost", 8082);
            tbxGname.Text = gameName;
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
       
        Double score;

        //-------------------------------------------------ON-SUCCESS-----------------------------------
        public void OnSuccess(object response)
        {
            if(boolean1 == false){
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
        }

        //-------------------------------------------------ON-EXCEPTION---------------------------------
        public void OnException(App42Exception exception)
        {
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
            if (tbxUname.Text != null || tbxUname.Text != "")
            {
                gameUserName = tbxUname.Text;
                scoreValue = Convert.ToInt32(tbxScore.Text);
                boolean = false;
                scoreBoardService.SaveUserScore(gameName, gameUserName, scoreValue, this);
            }
            else
            {
                throw new App42Exception("Please enter the credentilas first");
            }
        }

        private void btnEditUserScore_Click_1(object sender, RoutedEventArgs e)
        {
            boolean = true;
            scoreBoardService.EditScoreValueById(scoreId, scoreValue, this);
        }
    }
}