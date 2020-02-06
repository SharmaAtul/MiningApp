using MiningVentilation.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Entities
{
    public class Equipment : EntityBase
    {
        public Equipment()
        {
            Usage = new Collection<Usage>();
        }

        public override int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Usage> Usage { get; set; }
    }
}
