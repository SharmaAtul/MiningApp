using MiningVentilation.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Entities
{
    public class Usage : EntityBase
    {
        public override int Id { get; set; }

        [ForeignKey("Equipment")]
        public int EqipmentId { get; set; }
        public Equipment Equipment { get; set; }
        
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int Value { get; set; }
    }
}
