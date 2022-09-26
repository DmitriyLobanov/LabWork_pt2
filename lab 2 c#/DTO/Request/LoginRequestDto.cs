    using System.ComponentModel.DataAnnotations;

namespace LabWork_pt2.DTO.Request
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Please enter a username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        public string Password { get; set; }    

    }
}
