using System;
using System.ComponentModel.DataAnnotations;

namespace OniHealth.Worker2.Models
{
    public class UserLogin
    {
        public UserLogin() { }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string ProfilePicture { get; set; }

        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}