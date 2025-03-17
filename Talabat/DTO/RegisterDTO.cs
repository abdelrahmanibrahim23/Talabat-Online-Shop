using System.ComponentModel.DataAnnotations;

namespace Talabat.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone] 
        public string PhoneNumber { get; set; }    
        [Required]
        [RegularExpression("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*+\\/|!\"£$%^&*()#[\\]@~'?><,.=-_]).{6,}", 
            ErrorMessage = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string Password { get; set; }
    }
}
