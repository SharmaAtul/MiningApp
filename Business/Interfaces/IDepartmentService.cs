using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface IDepartmentService
    {
        IEnumerable<Department> GetDepartments();
    }
}
