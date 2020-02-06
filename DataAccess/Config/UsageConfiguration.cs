using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Config
{
    public class UsageConfiguration : IEntityTypeConfiguration<Usage>
    {
        public void Configure(EntityTypeBuilder<Usage> builder)
        {
            builder.ToTable("Usages");
        }
    }
}
