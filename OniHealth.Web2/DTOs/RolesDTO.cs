using System.ComponentModel.DataAnnotations;

namespace OniHealth.Web.DTOs
{
    public class RolesDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}