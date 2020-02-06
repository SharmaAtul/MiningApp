using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IEquipmentService
    {
        IEnumerable<Equipment> GetEquipments();
        Equipment Add();
        void Remove(int id);
        Equipment Save(Equipment equipment);
    }
}
