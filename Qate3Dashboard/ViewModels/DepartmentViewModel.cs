using Qate3DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qate3Dashboard.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        [MinLength(2)]
        [MaxLength(250)]
        public string Dept_Title { get; set; }

        [Required(ErrorMessage = "Department Image is required")]
        public IFormFile Image { get; set; }
        
        [MaxLength(250)]
        public string? Dept_ImageName { get; set; }


        #region Department Category Relationship
                [InverseProperty(nameof(Qate3DAL.Models.Category.Department))]
        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();
        #endregion





    }
}
