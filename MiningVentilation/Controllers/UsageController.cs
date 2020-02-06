using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiningVentilation.Models;

namespace MiningVentilation.Controllers
{
    public class UsageController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsageService _usageService;
        public UsageController(ILogger<HomeController> logger, IUsageService usageService)
        {
            _logger = logger;
            _usageService = usageService;
        }

        [HttpPost]
        public IActionResult Save(Usage usage)
        {
            var equipmentUpdated = _usageService.Save(usage);

            var usageByDept = _usageService.GetUsageByDepartment(usage.DepartmentId).FirstOrDefault();

            DepartmentUsageVM deptUsage = new DepartmentUsageVM();
            deptUsage.DepartmentId = usage.DepartmentId;
            deptUsage.Usage = usageByDept?.Value ?? 0;

            return Json(deptUsage);
        }
    }
}