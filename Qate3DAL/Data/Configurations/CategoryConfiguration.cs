using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qate3DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qate3DAL.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(E=>E.Cat_ImageName).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(E=>E.Cat_Title).HasColumnType("nvarchar(255)").IsRequired();
           
        }
    }
}
