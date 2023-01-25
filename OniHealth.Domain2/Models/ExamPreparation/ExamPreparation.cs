using System.ComponentModel.DataAnnotations.Schema;

namespace OniHealth.Domain.Models
{
    public class ExamPreparation : BaseEntity
    {
        public ExamPreparation() { }

        public ExamPreparation(string details)
        {
            ValidateCategory(details);
            Details = details;
            
        }

        [Column(TypeName = "text")]
        public string Details { get; set; }

        public void Update(string details)
        {
            ValidateCategory(details);
        }
        private void ValidateCategory(string details)
        {
            if (string.IsNullOrEmpty(details))
                throw new InvalidOperationException("The exam's details are invalid");
        }
    }
}