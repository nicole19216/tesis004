#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Win32;
using Syncfusion.Windows.Shared;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Markup;
using Syncfusion.SfSkinManager;

namespace SyncfusionWpfApp1
{
    /// <summary>
    /// Interaction logic for WordStyleWindow.xaml
    /// </summary>
    public partial class PRINCIPAL : RibbonWindow
    {
        private const string UriString = "PRODUCTO/MainP.xaml";

        public PRINCIPAL()
        {
            InitializeComponent();
            frameprincipal.NavigationService.Navigate(new Uri(UriString, UriKind.Relative));
            SubMenuUsuario.IsEnabled = false;
        }
    }
}
