using System.ComponentModel.DataAnnotations;

namespace Qate3Dashboard.DTOs
{
    public class SubCategoryDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }
    }
}
