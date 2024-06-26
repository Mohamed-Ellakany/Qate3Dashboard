//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Qate3DAL.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Qate3DAL.Data.Configurations
//{
//    internal class CategorySubCategoryConfiguration : IEntityTypeConfiguration<Category_SubCategory>
//    {
//        public void Configure(EntityTypeBuilder<Category_SubCategory> builder)
//        {
//            builder.HasKey(CS => new { CS.categoryId, CS.subCategoryId });


//            builder.HasOne(SC => SC.subCategory).WithMany(S=>S.CategorySubCategories)
//                .HasForeignKey(SC=>SC.subCategoryId);

//            builder.HasOne(SC => SC.category).WithMany(S=>S.SubCategoryCategories)
//                .HasForeignKey(SC=>SC.categoryId);
//        }
//    }
//}
