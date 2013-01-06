using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AboutCountries
{
    public class Country
    {
         string actualTime;
         string dCode;
        public int ID { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public string Currency { get; set; }
        public string Capital { get; set; }
        
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string UTC { get; set; }
        public string Region { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Language { get; set; }
        public string Link { get; set; }
        public string DialCode
        {
            get{return dCode;
            }
            set
            {
                dCode = "+" +value;                
            }
        }

        public string LocalTime { 
            get
            {
                return actualTime;
            }
            set
            {
                int len = value.Length;
                string hourStr = "0";
                string minuteStr = "0";

                if (len > 0)
                {
                    value.Trim();
                    value.Replace("+0", "");
                    value.Replace("+", "");
                    value.Replace("-0", "-");

                    int i = value.IndexOf(':');

                    if (i>0 && i < len)
                    {
                        hourStr = value.Substring(0,i);
                        minuteStr = value.Substring(i+1,len - (i+1));
                    }                    
                }
                double hh = double.Parse(hourStr);
                //
                //if(StartDate=="")
                {
                    DateTimeOffset localTime = DateTimeOffset.Now;
                    if (localTime.Offset.Hours < 0)
                        hh += 1;
                    else
                        hh -= 1;

                }
                //

                DateTime t = DateTime.Now.AddHours(hh).AddMinutes(double.Parse(minuteStr));
                actualTime = t.ToShortTimeString();
            }
        }

        public static string GetFirstNameKey(Country country)
        {
            char key = char.ToLower(country.Name[0]);

            if (key < 'a' || key > 'z')
            {
                key = '#';
            }

            return key.ToString();
        }

        public static string GetRegionKey(Country country)
        {
             return country.Region;
        }

        public static int CompareByFirstName(object obj1, object obj2)
        {
            Country p1 = (Country)obj1;
            Country p2 = (Country)obj2;

            int result = p1.Name.CompareTo(p2.Name);
            if (result == 0)
            {
                result = p1.Capital.CompareTo(p2.Capital);
            }

            return result;
        }

        public static int CompareByRegion(object obj1, object obj2)
        {
            Country p1 = (Country)obj1;
            Country p2 = (Country)obj2;

            int result = p1.Region.CompareTo(p2.Region);
            if (result == 0)
            {
                result = p1.Name.CompareTo(p2.Name);
            }

            return result;
        }
        public Country()
        {
        }
    }
}
