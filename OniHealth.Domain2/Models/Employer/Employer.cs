using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace OniHealth.Domain.Models
{
    public class Employer : BaseEntity
    {
        public Employer(){}

        public Employer(string name, string email, EmployerRole role, int salary, string phoneNumber, string zipCode)
        {
            ValidaCategoria(name, email, role, salary, phoneNumber, zipCode);
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
            ValidaCategoria(name, email, role, salary, phoneNumber, zipCode);
        }
        private void ValidaCategoria(string name, string email, EmployerRole role, int salary, string phoneNumber, string zipCode)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("O nome é inválido");

            if (string.IsNullOrEmpty(email))
                throw new InvalidOperationException("O email é inválido");

            if (role <= 0)
                throw new InvalidOperationException("O cargo é inválido");

            if (string.IsNullOrEmpty(phoneNumber))
                throw new InvalidOperationException("O número de telefone é inválido");

            if (string.IsNullOrEmpty(zipCode))
                throw new InvalidOperationException("O CEP é inválido");
        }
    }
}