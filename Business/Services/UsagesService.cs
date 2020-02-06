using Business.Interfaces;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Resources;
using MiningVentilation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Services
{
    public class UsageService : IUsageService
    {
        private readonly IRepository<Usage> _usageRepository;
        public UsageService(IRepository<Usage> usageRepository) {
            _usageRepository = usageRepository;
        }
        public IEnumerable<Usage> GetUsages()
        {
            return _usageRepository.TableNoTracking.ToList();
        }

        public Usage Save(Usage usage)
        {
            var usageDB = _usageRepository.Table.FirstOrDefault(x=>x.DepartmentId == usage.DepartmentId && x.EqipmentId==usage.EqipmentId);
            if (usageDB != null)
            {
                usageDB.Value = usage?.Value ?? 0;
                _usageRepository.Update(usageDB);
            }
            else
            {
                _usageRepository.Insert(usage);
            }
            return usage;
        }

        public List<UsageByDepartmentResource> GetUsageByDepartment(int departmentId = 0)
        {
            var usageDB = _usageRepository.TableNoTracking
                .Where(x => (departmentId == 0 || x.DepartmentId == departmentId))
                .ToList()
                .GroupBy(x => x.DepartmentId).Select(x => new
                UsageByDepartmentResource
                {
                    DepartmentId = x.Key,
                    Value = x.Sum(u => u.Value)
                });

            return usageDB.ToList();
        }
    }
}
