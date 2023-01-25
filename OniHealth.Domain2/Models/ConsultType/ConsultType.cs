using System.ComponentModel.DataAnnotations.Schema;

namespace OniHealth.Domain.Models
{
    public class ConsultType : BaseEntity
    {
        public ConsultType() { }

        public ConsultType(string name, string details)
        {
            ValidateCategory(name, details);
            Name = name;
            Details = details;
            
        }

        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string Details{ get; set; }

        public void Update(string name, string details)
        {
            ValidateCategory(name, details);
        }
        private void ValidateCategory(string name, string details)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("The consult's name is invalid");

            if (string.IsNullOrEmpty(details))
                throw new InvalidOperationException("The consult's details are invalid");
        }
    }
}