using Lab13.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Lab13.Controllers
{
    public class HomeController : Controller
    {
        /* private SalesContext context { get; set; }

         public HomeController(SalesContext ctx) => context = ctx;*/

        private QuarterlySalesUnitOfWork data { get; set; }
        public HomeController(SalesContext ctx) => data = new QuarterlySalesUnitOfWork(ctx);

        [HttpGet]
        public ViewResult Index(QuarterlySalesGridDTO values)
        {

            var builder = new QuarterlySalesGridBuilder(HttpContext.Session, values,
                defaultSortField: nameof(Sales.Year));


            var options = new SalesQueryOptions
            {
                Includes = "Employee",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };

            options.SortFilter(builder);

            var vm = new SalesListViewModel
            {
                Sales = data.Saless.List(options),
                Employees = data.Employees.List(new QueryOptions<Employee>
                {
                    OrderBy = a => a.LastName
                }),
                
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Saless.Count)
            };

            // pass view model to view
            return View(vm);
            /*IQueryable<Sales> query = context.Sales
                .Include(s => s.Employee)
                .OrderBy(s => s.Employee.LastName)
                .ThenBy(s => s.Employee.FirstName)
                .ThenBy(s => s.Year)
                .ThenBy(s => s.Quarter);

            if (id > 0)
            {
                query = query.Where(s => s.EmployeeId == id);
            }



            SalesListViewModel vm = new SalesListViewModel
            {
                Sales = query.ToList(),
                Employees = context.Employees.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToList(),
                EmployeeId = id
            };

            return View(vm);*/
        }

        /*[HttpPost]
        public RedirectToActionResult Index(Employee employee)
        {
            if(employee.EmployeeId > 0)
            {
                return RedirectToAction("Index", new { id = employee.EmployeeId });
            }
            else
            {
                return RedirectToAction("Index", new { id = string.Empty });
            }


        }*/

        [HttpPost]
        public RedirectToActionResult Filter(string[] filter, bool clear = false)
        {
            // get current route segments from session
            var builder = new QuarterlySalesGridBuilder(HttpContext.Session);

            // clear or update filter route segment values. If update, get author data
            // from database so can add author name slug to author filter value.
            if (clear)
            {
                builder.ClearFilterSegments();
            }
            else
            {
                var employee = data.Employees.Get(filter[0].ToInt());
                builder.CurrentRoute.PageNumber = 1;
                builder.LoadFilterSegments(filter, employee);
            }

            // save route data back to session and redirect to Book/List action method,
            // passing dictionary of route segment values to build URL
            builder.SaveRouteSegments();
            return RedirectToAction("Index", builder.CurrentRoute);
        }
    }
}
