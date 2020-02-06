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
    public class EquipmentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEquipmentService _equipmentService;
        private readonly IUsageService _usageService;
        public EquipmentController(ILogger<HomeController> logger, IEquipmentService equipmentService,IUsageService usageService)
        {
            _logger = logger;
            _equipmentService = equipmentService;
            _usageService = usageService;
        }
        [HttpPost]
        public IActionResult Add() {
            var equipment = _equipmentService.Add();
            return Json(equipment);
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            _equipmentService.Remove(id);

            var usageAll = _usageService.GetUsageByDepartment().ToList();

            UsageAllVM usage = new UsageAllVM();
            usage.Status = true;
            usage.DepartmentUsage = usageAll;

            return Json(usage);
        }

        [HttpPost]
        public IActionResult Save(Equipment equipment)
        {
            var equipmentUpdated = _equipmentService.Save(equipment);
            return Json(equipmentUpdated);
        }
    }
}