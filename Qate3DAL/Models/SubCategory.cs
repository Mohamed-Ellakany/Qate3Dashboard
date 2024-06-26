using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qate3DAL.Models
{
    public class SubCategory :BaseEntity
    {
        [Required(ErrorMessage = "Subcategory Is Required")]
        public string Title { get; set; }


        public ICollection<Product> Products { get; set; }



    }
}
