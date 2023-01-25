using System.ComponentModel;

namespace OniHealth.Domain.Models
{
    public class ExamTime : BaseEntity
    {
        public ExamTime() { }

        public ExamTime(DateTime examinationTime, DateTime startOfExam, DateTime endOfExam)
        {
            ValidateCategory(examinationTime);
            ExaminationTime = examinationTime;
            StartOfExam = startOfExam;
            EndOfExam = endOfExam;
            
        }

        public DateTime ExaminationTime { get; set; }

        [DefaultValue(typeof(DateTime), "0001-01-01T00:00:00")]
        public DateTime StartOfExam { get; set; }

        [DefaultValue(typeof(DateTime), "0001-01-01T00:00:00")]
        public DateTime EndOfExam  { get; set; }

        public void Update(DateTime examinationTime)
        {
            ValidateCategory(examinationTime);
        }
        private void ValidateCategory(DateTime examinationTime)
        {
            if (examinationTime == DateTime.MinValue)
                throw new InvalidOperationException("The examination time is invalid");
        }
    }
}