using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using com.shephertz.app42.paas.sdk.windows;
using com.shephertz.app42.paas.sdk.windows.game;

namespace Sample
{
    public partial class LeaderBoard : PhoneApplicationPage,App42Callback
    {
        
        static ServiceAPI serviceApi = new ServiceAPI("e6d7c63afc8abe1739ef977363d608370ce56de49c6397ceb5910a1ff0c167b2", "bd4f6fbad9becc13e9c4cbbe50dfcf54b68b7f0d9fa94e7fa44a53418275b373");
        ScoreBoardService scoreBoardService = serviceApi.BuildScoreBoardService();
        public delegate void UIcallbackEvent(IList<Game.Score> gList);
        public delegate void ShowExceptionCallbackEvent(String message);
        public LeaderBoard()
        {
            serviceApi.SetBaseURL("http://", "localhost", 8082);
            InitializeComponent();
            tblErrorMessage.Visibility = Visibility.Collapsed;
            lbxScore.ItemsSource = App.mViewModel.mScores;
        }
        private void btnLeaderBoard_Click_1(object sender, RoutedEventArgs e)
        {
            scoreBoardService.GetTopNRankers(tbxGname.Text, 15,this);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            tblGname.Visibility = Visibility.Visible;
            tbxGname.Visibility = Visibility.Visible;
            btnLeaderBoard.Visibility = Visibility.Visible;
            tblErrorMessage.Visibility = Visibility.Collapsed;
            App.mViewModel.mScores.Clear();
            base.OnNavigatedTo(e);
        }
        public void UICallback(IList<Game.Score> gList)
        {
            tblGname.Visibility = Visibility.Collapsed;
            tbxGname.Visibility = Visibility.Collapsed;
            btnLeaderBoard.Visibility = Visibility.Collapsed;
            App.mViewModel.LoadScores(gList);
        }
        public void ShowExceptionCallback(String message)
        {
            tblGname.Visibility = Visibility.Collapsed;
            tbxGname.Visibility = Visibility.Collapsed;
            btnLeaderBoard.Visibility = Visibility.Collapsed;
            tblErrorMessage.Visibility = Visibility.Visible;
            tblErrorMessage.Text = message;
        }
        public void OnSuccess(object response)
        {
            Game gameResponse = (Game)response;
            Console.WriteLine("SCOREBOARD_SUCCESS-->> " + response.ToString());
            if (response is Game)
            {
                Game gameObj = (Game)response;
                IList<Game.Score> scoreList = gameObj.GetScoreList();

                Console.WriteLine("ScoreId is  : " + scoreList[0].GetScoreId());
                Deployment.Current.Dispatcher.BeginInvoke(new UIcallbackEvent(UICallback), scoreList);
            }
        }
        //-------------------------------------------------ON-EXCEPTION---------------------------------
        public void OnException(App42Exception exception)
        {
            Deployment.Current.Dispatcher.BeginInvoke(new ShowExceptionCallbackEvent(ShowExceptionCallback), "Exception is : " + exception);
            Console.WriteLine("Exception is : " + exception);
        }
    }
}