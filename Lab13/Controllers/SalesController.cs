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
    public class SalesController : Controller
    {


        private SalesContext context { get; set; }

        public SalesController(SalesContext ctx) => context = ctx;

        public IActionResult Index() => RedirectToAction("Index", "Home");

        [Authorize]
        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Employees = context.Employees.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToList();
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(Sales sales)
        {
            string message = Validate.CheckSales(context, sales);
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError(nameof(sales.EmployeeId), message);
            }

            if (ModelState.IsValid)
            {
                context.Sales.Add(sales);
                context.SaveChanges();
                TempData["message"] = $"Sales added";
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
