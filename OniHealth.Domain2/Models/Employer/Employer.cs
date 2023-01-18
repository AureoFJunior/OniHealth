using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using OniHealth.Domain.Enums;

namespace OniHealth.Domain.Models
{
    public class Employer : BaseEntity
    {
        public Employer(){}

        public Employer(string name, string email, EmployerRole role, int salary, string phoneNumber, string zipCode)
        {
            ValidateCategory(name, email, role, salary, phoneNumber, zipCode);
            Name = name;
            Email = email;
            Role = (int) role;
            Salary = salary;
            PhoneNumber = phoneNumber;
            ZipCode = zipCode;
        }

        public string Name { get; set; }
        public string Email { get; set; }

        [ForeignKey(nameof(Roles))]
        public int Role { get; set; }
        public virtual Roles Roles { get; set; }

        public int Salary { get; set; }
        public string PhoneNumber { get; set; }
        public string ZipCode { get; set; }
        

        public void Update(string name, string email, EmployerRole role, int salary, string phoneNumber, string zipCode)
        {
            ValidateCategory(name, email, role, salary, phoneNumber, zipCode);
        }
        private void ValidateCategory(string name, string email, EmployerRole role, int salary, string phoneNumber, string zipCode)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("The name is invalid");

            if (string.IsNullOrEmpty(email))
                throw new InvalidOperationException("The email is invalid");

            if (role <= 0)
                throw new InvalidOperationException("The role is invalid");

            if (string.IsNullOrEmpty(phoneNumber))
                throw new InvalidOperationException("The phone number is invalid");

            if (string.IsNullOrEmpty(zipCode))
                throw new InvalidOperationException("The CEP is invalid");
        }
    }
}