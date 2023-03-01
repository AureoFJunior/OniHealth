using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace OniHealth.Domain.Models
{
    public class ConsultAppointment : BaseEntity
    {
        public ConsultAppointment() { }

        #region Fields
        public string Title { get; set; }
        public DateTime AppointmentTime { get; set; }
        public DateTime StartOfAppointment { get; set; }
        public DateTime EndOfAppointment { get; set; }
        public string Type { get; set; }
        public string TypeDetails { get; set; }
        public bool? CustomerIsPresent { get; set; }
        public bool? DoctorIsPresent { get; set; }
        public bool IsActive { get; set; }
        #endregion
    }
}