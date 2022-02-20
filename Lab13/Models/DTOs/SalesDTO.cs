using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




namespace Lab13.Models
{
    // Trying to store a Book object in session can cause problems because the JSON 
    // serialization done in SessionExtensionMethods.cs can create circular references
    // as the serializer tries to follow all the navigation properties. You can decorate
    // those properties with the [JsonIgnore] attribute, but you can end up with that
    // scattered all around. Another way, shown here, is to create a DTO class with the 
    // data needed for the cart. The DTO includes a Load() method to transfer the needed 
    // data from a Book object.

    public class SalesDTO
    {
        public int? SalesId { get; set; }
        public int? Quarter { get; set; }
        public int? Year { get; set; }
        public int EmployeeId { get; set; }

       

        public void Load(Sales sales)
        {
            SalesId = sales.SalesId;
            Quarter = sales.Quarter;
            Year = sales.Year;
            EmployeeId = sales.EmployeeId;

        }

    }

}
