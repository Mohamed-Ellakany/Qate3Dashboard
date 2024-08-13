using System.ComponentModel.DataAnnotations;

namespace Qate3Dashboard.DTOs
{
    public class DepartmentDto
    {


        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Dept_Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Dept_ImageName { get; set; }



    }
}
