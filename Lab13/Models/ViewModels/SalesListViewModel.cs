using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab13.Models;

namespace Lab13.Models
{
    public class SalesListViewModel
    {
        /*public List<Employee> Employees { get; set; }

        public List<Sales> Sales { get; set; }

        public int EmployeeId { get; set; }*/
        
        public IEnumerable<Sales> Sales { get; set; }
        
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }

        // data for filter drop-downs - one hardcoded
        public IEnumerable<Employee> Employees { get; set; }
        //public IEnumerable<Sales>.Year Year { get; set; }
        public Dictionary<string, string> Years =>
            new Dictionary<string, string>
            {
                {"2021", "2021" },
                {"2020", "2020" },
                {"2019", "2019" },
                {"2018", "2018" }
            };

        public Dictionary<string, string> Quarters =>
            new Dictionary<string, string> {
                { "1", "1" },
                { "2", "2" },
                { "3", "3" },
                { "4", "4" }
            };


    }
}



