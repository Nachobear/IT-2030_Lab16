using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab13.Models;
using Lab13.Models.Validation;

namespace Lab13.Controllers
{

    //this is for client validation
    public class ValidationController : Controller
    {
        private SalesContext context { get; set; }

        public ValidationController(SalesContext ctx) => context = ctx;

        public JsonResult CheckEmployee(string firstName, string lastName, DateTime dateOfBirth)
        {
            Employee employee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth
            };

            string message = Validate.CheckEmployee(context, employee);
            if (string.IsNullOrEmpty(message))
            {
                return Json(true);
            }

            return Json(message);


        }

        public JsonResult CheckManager(int managerId, string firstName, string lastName, DateTime dateOfBirth)
        {

            Employee employee = new Employee
            {
                ManagerId = managerId,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth

            };
            string message = Validate.CheckManagerEmployeeMatch(context, employee);
            if (string.IsNullOrEmpty(message))
            {
                return Json(true);
            }

            return Json(message);
        }

        public JsonResult CheckSales(int employeeId, int year, int quarter)
        {
            Sales sale = new Sales
            {
                EmployeeId = employeeId,
                Year = year,
                Quarter = quarter
            };

            string message = Validate.CheckSales(context, sale);
            if (string.IsNullOrEmpty(message))
            {
                return Json(true);
            }

            return Json(message);
        }
    }
}
