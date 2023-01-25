using System.ComponentModel;

namespace OniHealth.Domain.Models
{
    public class ConsultTime : BaseEntity
    {
        public ConsultTime() { }

        public ConsultTime(DateTime appointmentTime, DateTime startOfAppointment, DateTime endOfAppointment)
        {
            ValidateCategory(appointmentTime);
            AppointmentTime = appointmentTime;
            StartOfAppointment = startOfAppointment;
            EndOfAppointment = endOfAppointment;
            
        }

        public DateTime AppointmentTime { get; set; }

        [DefaultValue(typeof(DateTime), "0001-01-01T00:00:00")]
        public DateTime StartOfAppointment { get; set; }

        [DefaultValue(typeof(DateTime), "0001-01-01T00:00:00")]
        public DateTime EndOfAppointment { get; set; }

        public void Update(DateTime appointmentTime)
        {
            ValidateCategory(appointmentTime);
        }
        private void ValidateCategory(DateTime appointmentTime)
        {
            if (appointmentTime == DateTime.MinValue)
                throw new InvalidOperationException("The appointment's time is invalid");
        }
    }
}