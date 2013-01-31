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

//e1922da2-b6c4-490c-8515-01f40583e9f3

namespace AboutCountries
{
    public partial class DisplayRegion : PhoneApplicationPage
    {
        // Constructor
        public DisplayRegion()
        {
            InitializeComponent();

            buddiesRgn.SelectionChanged += CountrySelectionChanged;
        }


        void CountrySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Country country = buddiesRgn.SelectedItem as Country;
            if (country != null)
            {
                NavigationService.Navigate(new Uri("/PanoramaPage1.xaml?ID=" + country.ID, UriKind.Relative));
                buddiesRgn.SelectedItem = null;
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
    }
}