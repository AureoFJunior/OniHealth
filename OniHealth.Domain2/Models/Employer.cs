using System;

namespace OniHealth.Domain.Models
{
    public class Employer : BaseEntity
    {
        public Employer(){}

        public Employer(string nome, string email)
        {
            ValidaCategoria(nome, email);
            Nome = nome;
            Email = email;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }

        public void Update(string nome, string email)
        {
            ValidaCategoria(nome, email);
        }
        private void ValidaCategoria(string nome, string email)
        {
            if (string.IsNullOrEmpty(nome))
                throw new InvalidOperationException("O nome é inválido");

            if (string.IsNullOrEmpty(email))
                throw new InvalidOperationException("O email é inválido");
        }
    }
}