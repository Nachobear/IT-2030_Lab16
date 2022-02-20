using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lab13.Models
{
    public class QuarterlySalesGridDTO : GridDTO
    {
        [JsonIgnore]
        public const string DefaultFilter = "all";

        public string Employee { get; set; } = DefaultFilter;
        public string Year { get; set; } = DefaultFilter;
        public string Quarter { get; set; } = DefaultFilter;
    }
}



