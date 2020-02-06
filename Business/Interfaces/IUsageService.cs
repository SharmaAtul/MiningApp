using DataAccess.Entities;
using DataAccess.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface IUsageService
    {
        IEnumerable<Usage> GetUsages();
        Usage Save(Usage usage);
        List<UsageByDepartmentResource> GetUsageByDepartment(int departmentId = 0);
    }
}
