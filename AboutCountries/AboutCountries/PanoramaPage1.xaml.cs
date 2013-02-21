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
using System.Windows.Navigation;
using System.Globalization;
using System.Windows.Media.Imaging;
using System.Net.NetworkInformation;
namespace AboutCountries
{
    public partial class PanoramaPage1 : PhoneApplicationPage
    {
        int id = -1;
        public PanoramaPage1()
        {
            
            InitializeComponent();
            bool isConnected = NetworkInterface.GetIsNetworkAvailable();
            #if DEBUG
                isConnected = false;
            #endif

            if (!isConnected)
            {
                // do stuff that talks to the interwebseses here...
                map.Visibility = Visibility.Visible;
                mapUserControl1.Visibility = Visibility.Collapsed;
            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string idParam;
            if (NavigationContext.QueryString.TryGetValue("ID", out idParam))
            {
                
               id = Int32.Parse(idParam);
                DataContext = AllCountry.Current[id];
                AClock aa = new AClock();
                string utc = AllCountry.Current[id].UTC;
                CountryTxt.Text = AllCountry.Current[id].Name;
                CapitalTxt.Text = AllCountry.Current[id].Capital;
                CurrencyTxt.Text = AllCountry.Current[id].Currency;
                RegionTxt.Text = AllCountry.Current[id].Region;
                string longStr = AllCountry.Current[id].Longitude;
                string latStr = AllCountry.Current[id].Latitude;
                DialTxt.Text = AllCountry.Current[id].DialCode;
                LangTxt.Text = AllCountry.Current[id].Language;
                pan.Title= AllCountry.Current[id].Name;

                hyperlinkButton1.NavigateUri =new Uri(AllCountry.Current[id].Link);
                hyperlinkButton1.TargetName = "_blank";

                Longitude1Txt.Text = longStr;
                Latitude2Txt.Text = latStr;
                //map
                string mapPath = "img/"+AllCountry.Current[id].Name+".png";
                Uri uri = new Uri(mapPath, UriKind.Relative);
                ImageSource imgSource = new BitmapImage(uri);

                map.Source = imgSource;

                //flag
                string flagPath = "png/" + AllCountry.Current[id].Name + ".png";
                Uri uri_flag = new Uri(flagPath, UriKind.Relative);
                ImageSource flagImgSource = new BitmapImage(uri_flag);

                flag.Source = flagImgSource;

               // MessageBox.Show("ss7");
                string sLong= longStr.Replace('°', '.');
                string sLat = latStr.Replace('°', '.');
               // MessageBox.Show("ss76");
                int latIndex = sLat.IndexOf("'");
               // MessageBox.Show("int latIndex = sLat.IndexOf(\"'\");");
                sLat=sLat.Remove(latIndex);
               // MessageBox.Show("xx");
               int longIndex = sLong.IndexOf("'");
               // MessageBox.Show("xx3w");
                sLong=sLong.Remove(longIndex);
               // MessageBox.Show(sLong);
                IFormatProvider culture = new CultureInfo("en-GB");
                double dsLong = double.Parse(sLong, culture);
                double dsLat = double.Parse(sLat, culture);
                //if LAT contains N & Long contains W 
                 double iLong = (longStr.IndexOf("W") != -1 ? -1 : 1) * dsLong;
                double iLat = (latStr.IndexOf("N") != -1 ? 1 : -1) * dsLat;

                mapUserControl1.Latitude = iLat;
                mapUserControl1.Longitude = iLong;
                mapUserControl1.display();


                //{
                //    int pointOnLongitude = longStr.IndexOf("°");
                //    int pointOnLatitude = latStr.IndexOf("°");

                //    string sLong = longStr.Substring(0, pointOnLongitude);
                //    double iLong = double.Parse(sLong) * 1.45;
                    
                //    string sLat = latStr.Substring(0, pointOnLatitude);
                //    double iLat = double.Parse(sLat) * 1.12;

                //    if(longStr.Contains('W'))
                //    {
                //        iLong = 206-iLong;
                //    } 
                //    else
                //        iLong = 206 + iLong;

                //    if (latStr.Contains('N'))
                //    {
                //        iLat = 170 - iLat;
                //    }
                //    else
                //    {
                //        iLat = 170+ iLat;
                //    }

                //}

                if (utc == "")
                    utc = "0";

                UTCtxt.Text = utc;
          
                int len = utc.Length;
                string hourStr = "0";
                string minuteStr = "0";

                if (len > 0)
                {
                    utc.Trim();
                    utc.Replace("+0", "");
                    utc.Replace("+", "");
                    utc.Replace("-0", "-");

                    int i = utc.IndexOf(':');

                    if (i > 0 && i < len)
                    {
                        hourStr = utc.Substring(0, i);
                        minuteStr = utc.Substring(i + 1, len - (i + 1));
                    }
                }

                double hh = double.Parse(hourStr, culture);
                double mm = double.Parse(minuteStr, culture);
                string st_date = AllCountry.Current[id].StartDate;
                string en_date = AllCountry.Current[id].EndDate;
                st_date.Trim();
                en_date.Trim();

                if (st_date.Length > 0)
                {
                    //MessageBox.Show("ss72");
                    //IFormatProvider culture = new CultureInfo("en-GB");
                    //MessageBox.Show("ss765");
                    DateTime dt_start = DateTime.ParseExact(st_date, "M/dd/yyyy", culture);
                    DateTime dt_end = DateTime.ParseExact(en_date, "M/dd/yyyy", culture);
                    DateTime dt_today = DateTime.Now;

                    if (dt_start < dt_end) 
                    {
                        DateTime Jan01 = new DateTime(dt_start.Year,01,01);
                        DateTime Dec31 = new DateTime(dt_start.Year, 12, 31);

                        if (((dt_today >= dt_start) && (dt_today <= Dec31)) ||
                            ((dt_today <= dt_end) && (dt_today >= Jan01)))
                        {
                            if ((dt_start <= dt_today) && (dt_today<=dt_end))
                                hh += 1;
                        }

                    }
                    else
                    {

                        // convert StartDate and EndDate and check withe current date if it falls in then add hour
                        if (dt_today >= dt_start || dt_today <= dt_end) //check brazil start/end date
                        {
                            hh += 1;
                        }
                    }
                }
                
                aa.Hours = hh;
                aa.Minutes = double.Parse(minuteStr);

                if (App.FavGroupsID.Contains(id))
                {
                    textBlockAddFav.Text = "Remove from Fav";
                   // btnAddFav.Background.
                    string iconPath = "icons/remove-favourites.png";
                    Uri Iconuri = new Uri(iconPath, UriKind.Relative);

                    ImageBrush brush1 = new ImageBrush();
                    BitmapImage image = new BitmapImage(Iconuri);
                    brush1.ImageSource = image;
                    btnAddFav.Background = brush1;
                }
                
                canvas1.Children.Add(aa);
            }
        }

        private void btnAddFav_Click(object sender, RoutedEventArgs e)
        {
            if (textBlockAddFav.Text == "Remove from Fav")
            {
                App.FavGroupsID.Remove(id);
                App.saveSettings();
            }
            else
            {
                if (!App.FavGroupsID.Contains(id))
                {
                    App.FavGroupsID.Add(id);
                }
                    App.saveSettings();
                
            }
            this.NavigationService.Navigate(new Uri("/StartPage.xaml", UriKind.Relative));
        }
    }
}