using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Lab13.Models
{
    // extends generic QueryOptions<Book> class to add a 
    // SortFilter() method that adds the Sort and Filter
    // expression specific to the Bookstore application

    public class SalesQueryOptions : QueryOptions<Sales>
    {
        public void SortFilter(QuarterlySalesGridBuilder builder)
        {
            // filter
            if (builder.IsFilterByQuarter)
            {
                Where = b => b.Quarter == builder.CurrentRoute.QuarterFilter.ToInt();
            }
            if (builder.IsFilterByYear)
            {
                Where = b => b.Year == builder.CurrentRoute.YearFilter.ToInt();
            }
            if (builder.IsFilterByEmployee)
            {
                Where = b => b.EmployeeId == builder.CurrentRoute.EmployeeFilter.ToInt();
 
            }

            // sort 
            if (builder.IsSortByQuarter)
            {
                OrderBy = b => b.Quarter;
            }
            else if (builder.IsSortByYear)
            {
                OrderBy = b => b.Year;
            }
            else
            {
                OrderBy = b => b.Employee.LastName;
            }
        }
    }
}
