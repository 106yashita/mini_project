using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.Models.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "User id cannot be empty")]
        public string UserName { get; set; }


        [MinLength(6, ErrorMessage = "Password has to be minmum 6 chars long")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; } = string.Empty;
    }
}
