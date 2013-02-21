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
       // private static readonly string[] FavGroups = { "India", "Serbia","United Kingdom"};
        //private static readonly int[] FavGroupsID = { 1,5,25,60 ,89,120};

        private Dictionary<int, Country> _personLookup = new Dictionary<int, Country>();

        public void load()
        {
            List<Country> people = new List<Country>(AllCountry.Current);
            people.Sort(Country.CompareByFirstName);
            App.FavGroupsID.Sort();

            Dictionary<string, CountryInGroup> groups = new Dictionary<string, CountryInGroup>();

            foreach (int favCountry in App.FavGroupsID)
            {
                string c = people[favCountry].Name[0].ToString();
                CountryInGroup group = new CountryInGroup(c);
                this.Add(group);
                if (!groups.ContainsKey(c))
                    groups[c] = group;
                groups[c].Add(people[favCountry]);
            }
        }

        public CountryByFav()
        {

            

            load();

        //    List<Country> people = new List<Country>(AllCountry.Current);
           // people.Sort(Country.CompareByFirstName);
           // Sort(FavGroupsID);

            //Dictionary<string, CountryInGroup> groups = new Dictionary<string, CountryInGroup>();

            //foreach (char c in Groups)
            //{
            //    CountryInGroup group = new CountryInGroup(c.ToString());
            //    this.Add(group);
            //    groups[c.ToString()] = group;
            //}

            ////foreach (Country person in people)
            //{
            //    foreach (int favCountry in FavGroupsID)
            //    {
            //        string c = people[favCountry].Name[0].ToString();
            //        CountryInGroup group = new CountryInGroup(c);
            //        this.Add(group);
            //        if(!groups.ContainsKey(c))
            //            groups[c] = group;
            //        groups[c].Add(people[favCountry]);
            //    }
            //}

            //List<Country> people = new List<Country>(AllCountry.Current);
            //people.Sort(Country.CompareByFirstName);

            //Dictionary<string, CountryInGroup> groups = new Dictionary<string, CountryInGroup>();

            //foreach (char c in Groups)
            //{
            //    CountryInGroup group = new CountryInGroup(c.ToString());
            //    this.Add(group);
            //    groups[c.ToString()] = group;
            //}

            //foreach (Country person in people)           
            //{
            //    foreach(string favCountry in FavGroups)
            //    {
            //    if(person.Name ==favCountry)
            //        groups[Country.GetFirstNameKey(person)].Add(person);
            //    }
            //}
        }

    }
}
