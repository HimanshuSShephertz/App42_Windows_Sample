using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

// To use App42 Api's yyou have to import some packages

using com.shephertz.app42.paas.sdk.windows;
using com.shephertz.app42.paas.sdk.windows.user;
// This is the main page of your app

namespace Sample
{
    public partial class MainPage : PhoneApplicationPage, App42Callback
    {
        static ServiceAPI serviceApi = new ServiceAPI("YOUR_API_KEY", "YOUR_SECRET_KEY");
        // Here you have to use your apiKey and secretKey in order to obtain connection from App42 Server
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        // this is the click event

        private void login_Click_1(object sender, RoutedEventArgs e)
        {
            String userName = this.username.Text;
            String password = this.password.Password;
            String email = this.email.Text;

            // here you have to build the service u want to use

            UserService userService = serviceApi.BuildUserService();
            userService.CreateUser(userName, password, email, this);
            // now using this userService you can use all functions of User Service
            // this represents App42Callbaack object
            // now you have to explictlly implements methods of App42Callback
            
        }

        void App42Callback.OnException(App42Exception exception)
        {
            // here Exception is handled
            Dispatcher.BeginInvoke(() => { textB.Text = "Exception Caught successfully"; });
        }

        void App42Callback.OnSuccess(object response)
        {
            // here success is shown
            Dispatcher.BeginInvoke(() => { textB.Text = "User Successfully Created"; });
        }
    }
}