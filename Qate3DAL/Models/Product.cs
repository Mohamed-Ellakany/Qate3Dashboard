using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qate3DAL.Models
{
    public class Product:BaseEntity
    {
        [Required(ErrorMessage = "Product Name is required")]
        public string Prod_Title { get; set; }


        [Required(ErrorMessage = "Product Image is required")]
        public string Prod_ImageName { get; set; }



        #region Product - SubCategory Relationship

        [Required]
        public int SubcategoryId { get; set; }


        [ForeignKey("SubcategoryId")]
        public SubCategory Subcategory { get; set; }

        #endregion 
        #region Product - category Relationship

        [Required]
        public int categoryId { get; set; }

        [ForeignKey("categoryId")]
        public Category category { get; set; }

        #endregion


    }
}
