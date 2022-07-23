using System;
using System.ComponentModel.DataAnnotations;

namespace OniHealth.Web.DTOs
{
    public class UserDTO
    {
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
        public string Token { get; set; }
    }
}