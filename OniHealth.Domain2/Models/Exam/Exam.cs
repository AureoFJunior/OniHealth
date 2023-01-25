using System.ComponentModel.DataAnnotations.Schema;

namespace OniHealth.Domain.Models
{
    public class Exam : BaseEntity
    {
        public Exam() { }

        public Exam(string name, string details, bool isAuthorized)
        {
            ValidateCategory(name, details);
            Name = name;
            Details = details;
            IsAuthorized = isAuthorized;
            
        }

        #region Fields
        public string Name { get; set; }

        [ForeignKey(nameof(ExamTime))]
        public int? ExamTimeId { get; set; }
        public virtual ExamTime ExamTime { get; set; }

        [ForeignKey(nameof(ExamPreparation))]
        public int? ExamPreparationId { get; set; }
        public virtual ExamPreparation ExamPreparation { get; set; }

        [ForeignKey(nameof(Laboratory))]
        public int? LaboratoryId { get; set; }
        public virtual Laboratory Laboratory { get; set; }

        [Column(TypeName = "text")]
        public string Details { get; set; }
        public bool IsAuthorized { get; set; }
        #endregion

        public void Update(string name, string details)
        {
            ValidateCategory(name, details);
        }
        private void ValidateCategory(string name, string details)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("The exam's name is invalid");

            if (string.IsNullOrEmpty(details))
                throw new InvalidOperationException("The exam's details are invalid");
        }
    }
}