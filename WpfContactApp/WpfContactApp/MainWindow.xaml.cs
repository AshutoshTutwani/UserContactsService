using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using Newtonsoft.Json;
using WpfContactApp.Model;
using WpfContactApp.ViewModels;
using Path = System.IO.Path;

namespace WpfContactApp
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      this.DataContext = new UserDetailsVM();
    }

    private void OnClose(object sender, RoutedEventArgs e)
    {
      this.Close();
    }
  }
}
