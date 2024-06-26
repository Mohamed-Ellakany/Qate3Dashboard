using System.ComponentModel.DataAnnotations;

namespace Qate3Dashboard.ViewModels
{
    public class SignUpViewModel
    {

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }





    }
}
