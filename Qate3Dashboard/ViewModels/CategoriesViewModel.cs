using Qate3DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qate3Dashboard.ViewModels
{
    public class CategoriesViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        public string Cat_Title { get; set; }


        [MaxLength(250)]
        public string? Cat_ImageName { get; set; }


        [Required(ErrorMessage = "Category Image is required")]
        public IFormFile Image { get; set; }

        

        #region Department Category Relationship
      
        //[ForeignKey("Department")]
        public int Dept_Id { get; set; }

        //[InverseProperty(nameof(Qate3DAL.Models.Department.Categories))]
       // public Department Department { get; set; } = null!;
      

        #endregion

        //#region Category - SubCategory Relationship 
        //public ICollection<SubCategory> SubCategories { get; set; } = new HashSet<SubCategory>();
        //#endregion


    }
}
