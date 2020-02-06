using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Migrations
{
    public class AppDataSeedAsync
    {
        public static async Task SeedAsync(DataContext context)
        {
            context.Database.Migrate();

            var hasDepartments = await context.EntitySet<Department>().AnyAsync();
            if (!hasDepartments)
            {
                context.EntitySet<Department>().AddRange(GetDepartments());
                await context.SaveChangesAsync();
            }
        }

        static IEnumerable<Department> GetDepartments() {
            List<Department> departments = new List<Department>();

            departments.Add(new Department { Name = "Longwall", VentilationCapacity = 115 });
            departments.Add(new Department { Name = "Tailgate", VentilationCapacity = 70 });
            departments.Add(new Department { Name = "Maingate 13", VentilationCapacity = 49 });
            departments.Add(new Department { Name = "Maingate 14", VentilationCapacity = 52 });

            return departments;
        }

    }
}
