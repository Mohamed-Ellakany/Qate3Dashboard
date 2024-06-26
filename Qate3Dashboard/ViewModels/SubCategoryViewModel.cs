using Qate3DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Qate3Dashboard.ViewModels
{
    public class SubCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Subcategory Is Required")]
        public string Title { get; set; }


        public ICollection<Product> Products { get; set; }


    }
}
