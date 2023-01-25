using System;

namespace OniHealth.Domain.Models
{
    public class User : BaseEntity
    {
        public User(){}

        public User(string firstName, string lastName, string userName, string password, string email, DateTime birthDate, string profilePicture = "", short? isLogged = 0, short actualTheme = 0)
        {
            ValidateCategory(firstName, lastName, userName, password, email, birthDate, profilePicture);
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Password = password;
            Email = email;
            BirthDate = birthDate;
            IsLogged = isLogged;
            ProfilePicture = profilePicture;
            ActualTheme = actualTheme;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public short? IsLogged { get; set; }
        public short ActualTheme { get; set; }
        public string ProfilePicture { get; set; }

        public void Update(string firstName, string lastName, string userName, string password, string email, DateTime birthDate, string profilePicture)
        {
            ValidateCategory(firstName, lastName, userName, password, email, birthDate, profilePicture);
        }
        private void ValidateCategory(string firstName, string lastName, string userName, string password, string email, DateTime birthDate, string profilePicture)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                throw new InvalidOperationException("The name is invalid");

            if (string.IsNullOrEmpty(email))
                throw new InvalidOperationException("The email is invalid");

            if (string.IsNullOrEmpty(userName))
                throw new InvalidOperationException("The name de usuário is invalid");

            if (string.IsNullOrEmpty(password))
                throw new InvalidOperationException("The password is invalid");

            if (birthDate == DateTime.MinValue)
                throw new InvalidOperationException("Birth date is invalid");

            if (string.IsNullOrEmpty(profilePicture))
                throw new InvalidOperationException("The Profile picture is invalid");
        }
    }
}