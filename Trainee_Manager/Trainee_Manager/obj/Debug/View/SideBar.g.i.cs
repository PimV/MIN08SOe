﻿#pragma checksum "..\..\..\View\SideBar.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "74BB64A8609422DF8E3F223772559FC5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
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


namespace Trainee_Manager.View {
    
    
    /// <summary>
    /// SideBar
    /// </summary>
    public partial class SideBar : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\View\SideBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MainScreenButton;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\View\SideBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button StageButton;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\View\SideBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ReportsButton;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\View\SideBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PreferencesButton;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\View\SideBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ImportButton;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\View\SideBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LogoutButton;
        
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
            System.Uri resourceLocater = new System.Uri("/Trainee_Manager;component/view/sidebar.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\SideBar.xaml"
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
            this.MainScreenButton = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\..\View\SideBar.xaml"
            this.MainScreenButton.Click += new System.Windows.RoutedEventHandler(this.MainScreenButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.StageButton = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\View\SideBar.xaml"
            this.StageButton.Click += new System.Windows.RoutedEventHandler(this.StageButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ReportsButton = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\View\SideBar.xaml"
            this.ReportsButton.Click += new System.Windows.RoutedEventHandler(this.ReportsButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.PreferencesButton = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\View\SideBar.xaml"
            this.PreferencesButton.Click += new System.Windows.RoutedEventHandler(this.PreferencesButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ImportButton = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\View\SideBar.xaml"
            this.ImportButton.Click += new System.Windows.RoutedEventHandler(this.ImportButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.LogoutButton = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\View\SideBar.xaml"
            this.LogoutButton.Click += new System.Windows.RoutedEventHandler(this.LogoutButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
