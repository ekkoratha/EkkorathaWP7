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
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace AboutCountries
{
    public class AllCountry : IEnumerable<Country>
    {
        private static Dictionary<int, Country> _countryLookup;
        private static AllCountry _instance;

        public static AllCountry Current
        {
            get
            {
                return _instance ?? (_instance = new AllCountry());
            }
        }

        public Country this[int index]
        {
            get
            {
                Country country;
                _countryLookup.TryGetValue(index, out country);
                return country;
            }
        }

        #region IEnumerable<Country> Members

        public IEnumerator<Country> GetEnumerator()
        {
            EnsureData();
            return _countryLookup.Values.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            EnsureData();
            return _countryLookup.Values.GetEnumerator();
        }

        #endregion

        private void EnsureData()
        {
            if (_countryLookup == null)
            {                
                int i=0;
                XDocument doc = XDocument.Load("worldTime.xml");
                _countryLookup = new Dictionary<int, Country>();
                foreach (XElement e in doc.Descendants("Country"))
                {
                    Country cc = new Country();
                    cc.ID = i;
                    cc.Name = e.Element("Name").Value ;
                    cc.Capital = e.Element("Capital").Value ;
                    cc.Currency = e.Element("Currency").Value;
                    cc.Region= e.Element("Region").Value;
                    cc.UTC = e.Element("UTC").Value ;
                    cc.Img = "png/"+(e.Element("Name").Value) + ".png"; 
                    cc.StartDate = e.Element("StartDate").Value;
                    cc.EndDate = e.Element("EndDate").Value;
                    cc.LocalTime = e.Element("UTC").Value;
                    cc.Longitude = e.Element("Longitude").Value;
                    cc.Latitude = e.Element("Latitude").Value;
                    cc.Language = e.Element("Language").Value; 
                    cc.DialCode = e.Element("DialCode").Value;

                    _countryLookup[i++] = cc;
                }
            }
        }

    }
}
