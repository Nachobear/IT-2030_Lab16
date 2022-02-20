using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab13.Models;
using Lab13.Models.Validation;
using Microsoft.AspNetCore.Authorization;

namespace Lab13.Controllers
{
    public class EmployeeController : Controller
    {
       
         private SalesContext context { get; set; }

        public EmployeeController(SalesContext ctx) => context = ctx;

        public IActionResult Index() => RedirectToAction("Index", "Name");
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Employees = context.Employees.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToList();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            string message = Validate.CheckEmployee(context, employee);
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError(nameof(Employee.DateOfBirth), message);
            }

            message = Validate.CheckManagerEmployeeMatch(context, employee);
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError(nameof(Employee.ManagerId), message);
            }

            if (ModelState.IsValid)
            {
                context.Employees.Add(employee);
                context.SaveChanges();
                TempData["message"] = $"Employee {employee.FullName} added";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Employees = context.Employees.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToList();
                return View();
            }
        }
        
    }
}
