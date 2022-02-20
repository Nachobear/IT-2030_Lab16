using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab13.Models.Repositories
{
    public class QuarterlySalesUnitOfWork
    {

    }
}

namespace Lab13.Models
{
    public class QuarterlySalesUnitOfWork : IQuarterlySalesUnitOfWork
    {
        private SalesContext context { get; set; }
        public QuarterlySalesUnitOfWork(SalesContext ctx) => context = ctx;

        private Repository<Sales> salesData;
        public Repository<Sales> Saless
        {
            get
            {
                if (salesData == null)
                    salesData = new Repository<Sales>(context);
                return salesData;
            }
        }

        private Repository<Employee> employeeData;
        public Repository<Employee> Employees
        {
            get
            {
                if (employeeData == null)
                    employeeData = new Repository<Employee>(context);
                return employeeData;
            }
        }

        public void AddNewEmployee(Employee employee)
        {
            Employee e = new Employee();
            Employees.Insert(e);
        }

        public void AddNewSales(Sales sale)
        {
            Sales s = new Sales();
            Saless.Insert(s);
        }

        public void Save() => context.SaveChanges();
    }
}