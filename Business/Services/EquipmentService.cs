using Business.Interfaces;
using DataAccess;
using DataAccess.Entities;
using MiningVentilation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IRepository<Equipment> _equipmentRepository;
        private readonly IRepository<Usage> _usageRepository;
        public EquipmentService(IRepository<Equipment> equipmentRepository, IRepository<Usage> usageRepository) {
            _equipmentRepository = equipmentRepository;
            _usageRepository = usageRepository;
        }
        public IEnumerable<Equipment> GetEquipments()
        {
            return _equipmentRepository.TableNoTracking.ToList();
        }

        public Equipment Add()
        {
            var equipment = new Equipment { Name="temp" };
            _equipmentRepository.Insert(equipment);
            return equipment;
        }

        public void Remove(int id)
        {
            var equipment = _equipmentRepository.GetById(id);
            var usages = _usageRepository.Table.Where(x => x.EqipmentId == id);

            foreach (var usage in usages)
            {
                _usageRepository.DeleteDeferred(usage);
            }

            _equipmentRepository.Delete(equipment);
        }

        public Equipment Save(Equipment equipment)
        {
            var equipmentDB = _equipmentRepository.GetById(equipment.Id);
            if (equipmentDB!=null) {
                equipmentDB.Name = equipment.Name;
                _equipmentRepository.Update(equipment);
                equipment = equipmentDB;
            }
            return equipment;
        }
    }
}
