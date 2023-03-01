using OniHealth.Domain.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OniHealth.Domain.DTOs
{
    public class ConsultTimeDTO : BaseEntity
    {
        [Required]
        public DateTime AppointmentTime { get; set; }

        public DateTime StartOfAppointment { get; set; }

        public DateTime EndOfAppointment { get; set; }
    }
}