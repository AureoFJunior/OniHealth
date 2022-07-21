using System.ComponentModel.DataAnnotations;

namespace OniHealth.Web.DTOs
{
    public class EmployerDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public short Role { get; set; }
    }
}