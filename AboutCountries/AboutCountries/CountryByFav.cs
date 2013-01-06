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
    public class CountryByFav: List<CountryInGroup>
    {
        private static readonly string[] FavGroups = { "India", "UK", "USA"};

        private static readonly string Groups = "#abcdefghijklmnopqrstuvwxyz";

        private Dictionary<int, Country> _personLookup = new Dictionary<int, Country>();

        public CountryByFav()
        {
            List<Country> people = new List<Country>(AllCountry.Current);
            people.Sort(Country.CompareByFirstName);

            Dictionary<string, CountryInGroup> groups = new Dictionary<string, CountryInGroup>();

            foreach (char c in Groups)
            {
                CountryInGroup group = new CountryInGroup(c.ToString());
                this.Add(group);
                groups[c.ToString()] = group;
            }

            foreach (Country person in people)
            {
                groups[Country.GetFirstNameKey(person)].Add(person);
            }
        }

    }
}
