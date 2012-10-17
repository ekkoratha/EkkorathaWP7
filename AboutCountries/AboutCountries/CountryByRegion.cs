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

namespace AboutCountries
{
    public class CountryByRegion: List<CountryInGroup>
    {
        private static readonly string[] Groups = { "Africa", "Asia", "Europe", "Oceania", "North America", "South America" };

        private Dictionary<int, Country> _personLookup = new Dictionary<int, Country>();

        public CountryByRegion()
        {
            List<Country> people = new List<Country>(AllCountry.Current);
            people.Sort(Country.CompareByRegion);

            Dictionary<string, CountryInGroup> groups = new Dictionary<string, CountryInGroup>();

           // foreach (char c in Groups)
            foreach (string regionName in Groups)
            {
                CountryInGroup group = new CountryInGroup(regionName);
                this.Add(group);
                groups[regionName] = group;
            }

            foreach (Country person in people)
            {
                groups[Country.GetRegionKey(person)].Add(person);
            }
        }

    }
}
