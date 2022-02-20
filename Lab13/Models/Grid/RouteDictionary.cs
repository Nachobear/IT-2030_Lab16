

using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab13.Models
{
    // inherits dictionary of strings, adds a Clone() method. Adds properties
    // to get and set general paging, sorting, and filtering values from dictionary. 
    // Adds methods to set sort field value and sort direction value based on sort field, re-set filter values.

    public class RouteDictionary : Dictionary<string, string>
    {
        public int PageNumber
        {
            get => Get(nameof(GridDTO.PageNumber)).ToInt();
            set => this[nameof(GridDTO.PageNumber)] = value.ToString();
        }

        public int PageSize
        {
            get => Get(nameof(GridDTO.PageSize)).ToInt();
            set => this[nameof(GridDTO.PageSize)] = value.ToString();
        }

        public string SortField
        {
            get => Get(nameof(GridDTO.SortField));
            set => this[nameof(GridDTO.SortField)] = value;
        }

        public string SortDirection
        {
            get => Get(nameof(GridDTO.SortDirection));
            set => this[nameof(GridDTO.SortDirection)] = value;
        }

        public void SetSortAndDirection(string fieldName, RouteDictionary current)
        {
            this[nameof(GridDTO.SortField)] = fieldName;

            // set sort direction based on comparison of new and current sort field. if 
            // sort field is same as current, toggle between ascending and descending. 
            // if it's different, should always be ascending.
            if (current.SortField.EqualsNoCase(fieldName) &&
                current.SortDirection == "asc")
                this[nameof(GridDTO.SortDirection)] = "desc";
            else
                this[nameof(GridDTO.SortDirection)] = "asc";
        }

        public string QuarterFilter
        {
            get => Get(nameof(QuarterlySalesGridDTO.Quarter))?.Replace("quarter-", "");
            set => this[nameof(QuarterlySalesGridDTO.Quarter)] = value;
        }

        public string YearFilter
        {
            get => Get(nameof(QuarterlySalesGridDTO.Year))?.Replace("year-", "");
            set => this[nameof(QuarterlySalesGridDTO.Year)] = value;
        }

        public string EmployeeFilter
        {
            
           get
            {
                // author filter contains prefix, author id, and slug (eg, author-8-ta-nehisi-coates).
                // only need author id for filtering, so first remove 'author-' prefix from string. At
                // that point, the authorid will be at beginning of string. So find index of dash after 
                // id number and then return substring from beginning of string to that index.
                string s = Get(nameof(QuarterlySalesGridDTO.Employee))?.Replace("employee-", "");
                int index = s?.IndexOf('-') ?? -1;
                
                return (index == -1) ? s : s.Substring(0, index);
                
            }
            set => this[nameof(QuarterlySalesGridDTO.Employee)] = value;
        }

        //public void ClearFilters() =>
        //    GenreFilter = PriceFilter = AuthorFilter = BooksGridDTO.DefaultFilter;
        public void ClearFilters()
        {
            QuarterFilter = QuarterlySalesGridDTO.DefaultFilter;
            YearFilter = QuarterlySalesGridDTO.DefaultFilter;
            EmployeeFilter = QuarterlySalesGridDTO.DefaultFilter;
        }

        private string Get(string key) => Keys.Contains(key) ? this[key] : null;

        // return a new dictionary that contains the same values as this dictionary.
        // needed so that pages can change the route values when calculating paging, sorting,
        // and filtering links, without changing the values of the current route
        public RouteDictionary Clone()
        {
            var clone = new RouteDictionary();
            foreach (var key in Keys)
            {
                clone.Add(key, this[key]);
            }
            return clone;
        }
    }
}

