﻿#pragma checksum "F:\Personal Repo\App42_Windows_Sample\Sample\GameEntry.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "49D79ABF70C3E3796639A118C07740FE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Sample {
    
    
    public partial class GameEntry : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock tblGname;
        
        internal System.Windows.Controls.TextBox tbxGname;
        
        internal System.Windows.Controls.TextBlock tblUname;
        
        internal System.Windows.Controls.TextBox tbxUname;
        
        internal System.Windows.Controls.TextBlock tblScore;
        
        internal System.Windows.Controls.TextBox tbxScore;
        
        internal System.Windows.Controls.TextBlock tblScoreId;
        
        internal System.Windows.Controls.TextBox tbxScoreId;
        
        internal System.Windows.Controls.TextBlock tblErrorMessage;
        
        internal System.Windows.Controls.Button btnSaveUserScore;
        
        internal System.Windows.Controls.Button btnEditUserScore;
        
        internal System.Windows.Controls.Button btnLeaderBoard;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Sample;component/GameEntry.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.tblGname = ((System.Windows.Controls.TextBlock)(this.FindName("tblGname")));
            this.tbxGname = ((System.Windows.Controls.TextBox)(this.FindName("tbxGname")));
            this.tblUname = ((System.Windows.Controls.TextBlock)(this.FindName("tblUname")));
            this.tbxUname = ((System.Windows.Controls.TextBox)(this.FindName("tbxUname")));
            this.tblScore = ((System.Windows.Controls.TextBlock)(this.FindName("tblScore")));
            this.tbxScore = ((System.Windows.Controls.TextBox)(this.FindName("tbxScore")));
            this.tblScoreId = ((System.Windows.Controls.TextBlock)(this.FindName("tblScoreId")));
            this.tbxScoreId = ((System.Windows.Controls.TextBox)(this.FindName("tbxScoreId")));
            this.tblErrorMessage = ((System.Windows.Controls.TextBlock)(this.FindName("tblErrorMessage")));
            this.btnSaveUserScore = ((System.Windows.Controls.Button)(this.FindName("btnSaveUserScore")));
            this.btnEditUserScore = ((System.Windows.Controls.Button)(this.FindName("btnEditUserScore")));
            this.btnLeaderBoard = ((System.Windows.Controls.Button)(this.FindName("btnLeaderBoard")));
        }
    }
}

