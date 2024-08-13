using System.ComponentModel.DataAnnotations;

namespace Qate3Dashboard.DTOs
{
    public class ProductDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Prod_Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Prod_ImageName { get; set; }

        public int SubcategoryId { get; set; }

        public int categoryId { get; set; }

    }
}
