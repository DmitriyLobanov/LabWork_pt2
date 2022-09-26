using System.ComponentModel.DataAnnotations;


namespace LabWork_pt2.DTO.Request
{
    public class RegistrationRequestDto
    {
        [Required(ErrorMessage = "Please enter a password")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter a username")]
        public string Password { get; set; }    

       // public Role Role { get; set;}
    }
}