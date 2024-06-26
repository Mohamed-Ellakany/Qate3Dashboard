using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qate3DAL.Models
{
    public class Category :BaseEntity
    {

        [Required(ErrorMessage = "Category Name is required")]
        public string Cat_Title { get; set; }


        [Required(ErrorMessage = "Category Image is required")]
        public string Cat_ImageName { get; set; }


        #region Department Category Relationship
        [ForeignKey("Department")]
        public int Dept_Id { get; set; }

        [InverseProperty(nameof(Models.Department.Categories))]
        public Department Department { get; set; } = null!;
        #endregion

        #region Category - SubCategory Relationship 

        public ICollection<Product> Products { get; set; }


        #endregion



    }
}
