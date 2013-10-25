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

        static ServiceAPI serviceApi = new ServiceAPI("62f6b446a152a488b92a7cc27421e6ee105247a973246b2d528ca67f746004fc",
            "6ee0b3f11115e55b92c3eab66a5a2a94197a27cea8e43f4239748a443214a312");
        ScoreBoardService scoreBoardService = serviceApi.BuildScoreBoardService();
        public delegate void UIcallbackEvent(IList<Game.Score> gList);
        public delegate void ShowExceptionCallbackEvent(String message);
        public LeaderBoard()
        {
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