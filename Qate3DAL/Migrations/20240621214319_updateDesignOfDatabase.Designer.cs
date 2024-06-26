﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Qate3DAL.Data;

#nullable disable

namespace Qate3DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240621214319_updateDesignOfDatabase")]
    partial class updateDesignOfDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Qate3DAL.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cat_ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Cat_Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Dept_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Dept_Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Qate3DAL.Models.Category_SubCategory", b =>
                {
                    b.Property<int>("categoryId")
                        .HasColumnType("int");

                    b.Property<int>("subCategoryId")
                        .HasColumnType("int");

                    b.HasKey("categoryId", "subCategoryId");

                    b.HasIndex("subCategoryId");

                    b.ToTable("Category_SubCategory");
                });

            modelBuilder.Entity("Qate3DAL.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Dept_ImageName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Dept_Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Qate3DAL.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Prod_ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Prod_Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("SubcategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubcategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Qate3DAL.Models.SubCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("SubCategory");
                });

            modelBuilder.Entity("Qate3DAL.Models.Category", b =>
                {
                    b.HasOne("Qate3DAL.Models.Department", "Department")
                        .WithMany("Categories")
                        .HasForeignKey("Dept_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Qate3DAL.Models.Category_SubCategory", b =>
                {
                    b.HasOne("Qate3DAL.Models.Category", "category")
                        .WithMany("SubCategoryCategories")
                        .HasForeignKey("categoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Qate3DAL.Models.SubCategory", "subCategory")
                        .WithMany("CategorySubCategories")
                        .HasForeignKey("subCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("category");

                    b.Navigation("subCategory");
                });

            modelBuilder.Entity("Qate3DAL.Models.Product", b =>
                {
                    b.HasOne("Qate3DAL.Models.SubCategory", "Subcategory")
                        .WithMany()
                        .HasForeignKey("SubcategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subcategory");
                });

            modelBuilder.Entity("Qate3DAL.Models.Category", b =>
                {
                    b.Navigation("SubCategoryCategories");
                });

            modelBuilder.Entity("Qate3DAL.Models.Department", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("Qate3DAL.Models.SubCategory", b =>
                {
                    b.Navigation("CategorySubCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
