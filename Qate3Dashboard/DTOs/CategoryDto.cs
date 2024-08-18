using System.ComponentModel.DataAnnotations;

namespace Qate3Dashboard.DTOs
{
    public class CategoryDto
    {


        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Cat_Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Cat_ImageName { get; set; }

        public int Dept_Id { get; set; }

        public string Dept_Name { get; set; }

    }
}
