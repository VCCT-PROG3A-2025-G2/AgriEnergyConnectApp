using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnectApp.Models
{
    public enum UserRole
    {
        Farmer,
        Employee
    }

    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
