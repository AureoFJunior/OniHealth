using System;

namespace OniHealth.Domain.Models
{
    public class User : BaseEntity
    {
        public User(){}

        public User(string firstName, string lastName, string email, DateTime birthDate)
        {
            ValidaCategoria(firstName, lastName, email, birthDate);
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public void Update(string firstName, string lastName, string email, DateTime birthDate)
        {
            ValidaCategoria(firstName, lastName, email, birthDate);
        }
        private void ValidaCategoria(string firstName, string lastName, string email, DateTime birthDate)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                throw new InvalidOperationException("O nome é inválido");

            if (string.IsNullOrEmpty(email))
                throw new InvalidOperationException("O email é inválido");

            if (birthDate == DateTime.MinValue)
                throw new InvalidOperationException("O cargo é inválido");
        }
    }
}