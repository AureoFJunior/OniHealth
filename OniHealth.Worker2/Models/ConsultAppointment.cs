
namespace OniHealth.Worker2.Models
{
    public class ConsultAppointment
    {
        public ConsultAppointment() { }

        #region Fields
        public int Id { get; set; }
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