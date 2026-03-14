using HrApp.Data;
using HrApp.Models;
using HrApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HrApp.Services.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        AppDbContext _context;
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task Add(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Employee employee)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            //var task = _context.Employees.ToListAsync();
            //do
            //{ }
            //while (!task.IsCompleted);
            //var employees = task.Result;
            var employees = await _context.Employees.ToListAsync();
            //await Task.Delay(0);
            return employees;
        }

        public Task<Employee?> GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
