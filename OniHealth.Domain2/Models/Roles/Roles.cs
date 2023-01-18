using System;

namespace OniHealth.Domain.Models
{
    public class Roles : BaseEntity
    {
        public Roles(){}

        public Roles(string name)
        {
            ValidateCategory(name);
            Name = name;
        }

        public string Name { get; set; }
        public virtual ICollection<Employer> Employer { get; set; }
      

        public void Update(string name)
        {
            ValidateCategory(name);
        }
        private void ValidateCategory(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException();
        }
    }
}