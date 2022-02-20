using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;




namespace Lab13.Models
{
    // inherits the general purpose GridBuilder class and adds application-specific 
    // methods for loading and clearing filter route segments in route dictionary.
    // Also adds application-specific Boolean flags for sorting and filtering. 

    public class QuarterlySalesGridBuilder : GridBuilder
    {
        // this constructor gets current route data from session
        public QuarterlySalesGridBuilder(ISession sess) : base(sess) { }

        // this constructor stores filtering route segments, as well as
        // the paging and sorting route segments stored by the base constructor
        public QuarterlySalesGridBuilder(ISession sess, QuarterlySalesGridDTO values,
            string defaultSortField) : base(sess, values, defaultSortField)
        {
            // store filter route segments - add filter prefixes if this is initial load
            // of page with default values rather than route values (route values have prefix)
            bool isInitial = values.Quarter.IndexOf("quarter-") == -1;
            routes.EmployeeFilter = (isInitial) ?  values.Employee : values.Employee;
            routes.QuarterFilter = (isInitial) ?  values.Quarter : values.Quarter;
            routes.YearFilter = (isInitial) ?  values.Year : values.Year;
        }

        // load new filter route segments contained in a string array - add filter prefix 
        // to each one. if filtering by author (rather than just 'all'), add author slug 
        public void LoadFilterSegments(string[] filter, Employee employee)
        {
            if (employee == null)
            {
                routes.EmployeeFilter =   filter[0];
            }
            else
            {
                routes.EmployeeFilter =  filter[0]
                    + "-" + employee.FullName.Slug();
            }
            routes.YearFilter = filter[1];
            routes.QuarterFilter = filter[2];
        }
        public void ClearFilterSegments() => routes.ClearFilters();

        //~~ filter flags ~~//
        string def = QuarterlySalesGridDTO.DefaultFilter;   // get default filter value from static DTO property
        public bool IsFilterByEmployee => routes.EmployeeFilter != def;
        public bool IsFilterByQuarter => routes.QuarterFilter != def;
        public bool IsFilterByYear => routes.YearFilter != def;

        //~~ sort flags ~~//
        public bool IsSortByYear =>
            routes.SortField.EqualsNoCase(nameof(Sales.Year));
        public bool IsSortByQuarter =>
            routes.SortField.EqualsNoCase(nameof(Sales.Quarter));
    }
}

