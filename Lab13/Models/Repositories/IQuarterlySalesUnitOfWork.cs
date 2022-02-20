using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab13.Models;

namespace Lab13.Models
{
    public interface IQuarterlySalesUnitOfWork
    {
        Repository<Sales> Saless { get; }
        Repository<Employee> Employees { get; }
      
        void AddNewSales(Sales sale);
        void AddNewEmployee(Employee employee);
        void Save();
    }
}
