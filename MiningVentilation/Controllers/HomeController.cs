using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiningVentilation.Models;

namespace MiningVentilation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEquipmentService _equipmentService;
        private readonly IDepartmentService _departmentService;
        private readonly IUsageService _usageService;
        public HomeController(ILogger<HomeController> logger, IEquipmentService equipmentService
            , IDepartmentService departmentService, IUsageService usageService)
        {
            _logger = logger;
            _equipmentService = equipmentService;
            _departmentService = departmentService;
            _usageService = usageService;
        }

        public IActionResult Index()
        {
            var equipments = _equipmentService.GetEquipments().OrderBy(x=>x.Name).ToList();
            var departments = _departmentService.GetDepartments().OrderBy(x => x.Name).ToList();
            var usages = _usageService.GetUsages().ToList();

            UsageDataVM usageDataVM = new UsageDataVM();
            usageDataVM.Equipments = equipments;
            usageDataVM.Departments = departments;
            usageDataVM.Usages = usages;

            return View(usageDataVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
