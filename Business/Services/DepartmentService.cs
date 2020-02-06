using Business.Interfaces;
using DataAccess;
using DataAccess.Entities;
using MiningVentilation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _departmentRepository;
        public DepartmentService(IRepository<Department> departmentRepository) {
            _departmentRepository = departmentRepository;
        }
        public IEnumerable<Department> GetDepartments()
        {
            return _departmentRepository.TableNoTracking.ToList();
        }
    }
}
