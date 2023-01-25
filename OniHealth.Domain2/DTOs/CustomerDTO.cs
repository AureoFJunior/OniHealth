using System;
using System.ComponentModel.DataAnnotations;

namespace OniHealth.Domain.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public short SignedPlan { get; set; }

        [Required]
        public bool IsDependent { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime LastPaymentDate { get; set; }
    }
}