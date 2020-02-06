using DataAccess.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiningVentilation.Models
{
    public class UsageAllVM
    {
        public UsageAllVM() {
            DepartmentUsage = new List<UsageByDepartmentResource>();
        }
        public bool Status { get; set; }
        public List<UsageByDepartmentResource> DepartmentUsage { get; set; }
    }
}
