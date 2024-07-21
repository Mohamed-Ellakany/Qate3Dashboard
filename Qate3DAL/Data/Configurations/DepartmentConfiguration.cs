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
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D=>D.Dept_Title).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(D => D.Dept_ImageName).HasColumnType("nvarchar(255)").IsRequired();
            builder.HasMany(c=>c.Categories).WithOne(c=>c.Department).HasForeignKey(D=>D.Dept_Id).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(P => P.Dept_Title);
        }
    }
}
