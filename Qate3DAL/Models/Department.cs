using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qate3DAL.Models
{
    public class Department :BaseEntity
    {

        [Required]
        public string Dept_Title { get; set; }

        [MaxLength(250)]
        public string  Dept_ImageName { get; set; }


        #region Department Category Relationship

        [InverseProperty(nameof(Models.Category.Department))]
        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();

        #endregion




    }
}
