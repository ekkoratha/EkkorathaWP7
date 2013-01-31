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
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

//44°31'E
//e1922da2-b6c4-490c-8515-01f40583e9f3

namespace AboutCountries
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            buddies.SelectionChanged += CountrySelectionChanged;
        }
        
private void ContextMenu_Closed(object sender, RoutedEventArgs e) 
{
    buddies.IsEnabled = true; 
 } 

 private void ContextMenu_Opened(object sender, RoutedEventArgs e) 
 {

     buddies.IsEnabled = false; 
 } 


        void CountrySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          //  MessageBox.Show("ss");
            Country country = buddies.SelectedItem as Country;
           // MessageBox.Show("ss2");
            if (country != null)
            {
             //   MessageBox.Show("ss3");
                NavigationService.Navigate(new Uri("/PanoramaPage1.xaml?ID=" + country.ID, UriKind.Relative));
                buddies.SelectedItem = null;
            }

        }

        private void MenuAboutClick(object sender, System.EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }
        private void MenuRateThisClick(object sender, System.EventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();

            marketplaceReviewTask.Show();

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Country country = buddies.SelectedItem as Country;
            string ss = "You chose to ";
            if (country != null)
            {
              //  NavigationService.Navigate(new Uri("/PanoramaPage1.xaml?ID=" + country.ID, UriKind.Relative));
              //  buddies.SelectedItem = null;
                ss += country.ID.ToString();
            }

            MenuItem menuItem = (MenuItem)sender;
          //  menuItem.
            //MessageBox.Show("You chose to  " + menuItem.Header.ToString(), "Result", MessageBoxButton.OK);
            MessageBox.Show(ss + menuItem.Header.ToString());
        }

    }
}