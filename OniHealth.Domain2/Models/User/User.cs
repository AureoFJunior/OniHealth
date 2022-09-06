using System;

namespace OniHealth.Domain.Models
{
    public class User : BaseEntity
    {
        public User(){}

        public User(string firstName, string lastName, string userName, string password, string email, DateTime birthDate, short? isLogged = 0)
        {
            ValidaCategoria(firstName, lastName, userName, password, email, birthDate);
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Password = password;
            Email = email;
            BirthDate = birthDate;
            IsLogged = isLogged;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public short? IsLogged { get; set; }

        public void Update(string firstName, string lastName, string userName, string password, string email, DateTime birthDate)
        {
            ValidaCategoria(firstName, lastName, userName, password, email, birthDate);
        }
        private void ValidaCategoria(string firstName, string lastName, string userName, string password, string email, DateTime birthDate)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                throw new InvalidOperationException("O nome é inválido");

            if (string.IsNullOrEmpty(email))
                throw new InvalidOperationException("O email é inválido");

            if (string.IsNullOrEmpty(userName))
                throw new InvalidOperationException("O nome de usuário é inválido");

            if (string.IsNullOrEmpty(password))
                throw new InvalidOperationException("A senha é inválida");

            if (birthDate == DateTime.MinValue)
                throw new InvalidOperationException("A data de nascimento é inválida");
        }
    }
}