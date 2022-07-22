using System;

namespace OniHealth.Domain.Models
{
    public class Employer : BaseEntity
    {
        public Employer(){}

        public Employer(string name, string email, short role)
        {
            ValidaCategoria(name, email, role);
            Name = name;
            Email = email;
            Role = role;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public short Role { get; set; }

        public void Update(string name, string email, short role)
        {
            ValidaCategoria(name, email, role);
        }
        private void ValidaCategoria(string name, string email, short role)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("O nome é inválido");

            if (string.IsNullOrEmpty(email))
                throw new InvalidOperationException("O email é inválido");

            if (role <= 0)
                throw new InvalidOperationException("O cargo é inválido");
        }
    }
}