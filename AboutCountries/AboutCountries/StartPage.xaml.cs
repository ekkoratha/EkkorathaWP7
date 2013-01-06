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

namespace AboutCountries
{
    public partial class StartPage : PhoneApplicationPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void btnAlphabetic_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void btnRegion_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/DisplayRegions.xaml", UriKind.Relative));
        }

        private void btnFav_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/DisplayFav.xaml", UriKind.Relative));
        }
    }
}