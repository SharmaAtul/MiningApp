using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiningVentilation.Models
{
    public class UsageDataVM
    {
        public UsageDataVM() {
            Departments = new List<Department>();
            Equipments = new List<Equipment>();
            Usages = new List<Usage>();
        }

        public List<Department> Departments { get; set; }
        public List<Equipment> Equipments { get; set; }
        public List<Usage> Usages { get; set; }
    }
}
