﻿#pragma checksum "..\..\..\Controls\TorrentSearch.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FC6E4FEC5D48F09F6853A5003BC8781E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TM2.Controls;


namespace TM2.Controls {
    
    
    /// <summary>
    /// TorrentSearch
    /// </summary>
    public partial class TorrentSearch : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 48 "..\..\..\Controls\TorrentSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbCategory;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\Controls\TorrentSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbGenre;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\Controls\TorrentSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbYear;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\Controls\TorrentSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CmbMeklet;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\Controls\TorrentSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnMeklet;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TM2;component/controls/torrentsearch.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Controls\TorrentSearch.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.cmbCategory = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.cmbGenre = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.cmbYear = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.CmbMeklet = ((System.Windows.Controls.ComboBox)(target));
            
            #line 113 "..\..\..\Controls\TorrentSearch.xaml"
            this.CmbMeklet.KeyUp += new System.Windows.Input.KeyEventHandler(this.CmbMeklet_KeyUp);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BtnMeklet = ((System.Windows.Controls.Button)(target));
            
            #line 121 "..\..\..\Controls\TorrentSearch.xaml"
            this.BtnMeklet.Click += new System.Windows.RoutedEventHandler(this.BtnMeklet_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
