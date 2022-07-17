using System.ComponentModel.DataAnnotations;

namespace OniHealth.Web.DTOs
{
    public class EmployerDTO
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }
    }
}