using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ClientConvertisseurV2.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RootPage : Page
    {
        
        public RootPage(Frame frame)
        {
            this.InitializeComponent();
            this.MySplitView.Content = frame;
            (MySplitView.Content as Frame).Navigate(typeof(MainPage));
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void ButtonEurosDevise_Click(object sender, RoutedEventArgs e)
        {
            (MySplitView.Content as Frame).Navigate(typeof(MainPage));
        }

        private void ButtonDeviseEuros_Click(object sender, RoutedEventArgs e)
        {
            (MySplitView.Content as Frame).Navigate(typeof(DeviseEuros));
        }


    }
}
