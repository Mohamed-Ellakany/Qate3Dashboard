using Qate3DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qate3Dashboard.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Product Name is required")]
        public string Prod_Title { get; set; }


        [MaxLength(250)]
        public string? Prod_ImageName { get; set; }

        public IFormFile Image { get; set; }


        #region Product - SubCategory Relationship

        [Required]
        public int SubcategoryId { get; set; }

       

        // [ForeignKey("SubcategoryId")]
        //  public SubCategory Subcategory { get; set; }

        #endregion

        #region Product - category Relationship

        [Required]
        public int categoryId { get; set; }

        //[ForeignKey("categoryId")]
        //public Category category { get; set; }

        #endregion


    }
}
