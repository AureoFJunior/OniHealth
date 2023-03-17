using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniHealth.Domain.DTOs
{
    public class PlansDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public int TotalValue { get; set; }

        [Required]
        public bool HasEmergency { get; set; }

    }
}
