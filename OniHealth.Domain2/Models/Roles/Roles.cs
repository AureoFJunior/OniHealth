using System;

namespace OniHealth.Domain.Models
{
    public class Roles : BaseEntity
    {
        public Roles(){}

        public Roles(string name)
        {
            ValidaCategoria(name);
            Name = name;
        }

        public string Name { get; set; }
        public virtual ICollection<Employer> Employer { get; set; }
      

        public void Update(string name)
        {
            ValidaCategoria(name);
        }
        private void ValidaCategoria(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("O nome do cargo é inválido");
        }
    }
}