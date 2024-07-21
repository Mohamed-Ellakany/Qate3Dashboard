using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qate3DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qate3DAL.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Prod_Title).HasColumnType("nvarchar(255)").IsRequired();

            builder.Property(P => P.Prod_ImageName).HasColumnType("nvarchar(255)").IsRequired();

            builder.HasIndex(P=>P.Prod_Title);


        }
    }
}
