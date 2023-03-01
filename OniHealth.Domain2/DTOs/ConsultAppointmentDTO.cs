using OniHealth.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace OniHealth.Domain.DTOs
{
    public class ConsultAppointmentDTO : BaseEntity
    {
        public string Title { get; set; }
        public DateTime AppointmentTime { get; set; }
        public DateTime StartOfAppointment { get; set; }
        public DateTime EndOfAppointment { get; set; }
        public string Type { get; set; }
        public string TypeDetails { get; set; }
        public bool? CustomerIsPresent { get; set; }
        public bool? DoctorIsPresent { get; set; }
        public bool IsActive { get; set; }
    }
}