using System;

namespace OniHealth.Domain.Models
{
    public class Customer : BaseEntity
    {
        public Customer() { }

        public Customer(string name, string email, DateTime birthDate, short signedPlan, bool isDependent)
        {
            ValidaCategoria(name, email, birthDate, signedPlan, isDependent );
            Name = name;
            Email = email;
            BirthDate = birthDate;  
            SignedPlan = signedPlan;
            IsDependent = isDependent;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public short SignedPlan { get; set; }
        public bool IsDependent { get; set; }
        public string PhoneNumber { get; set; }

        public void Update(string name, string email, DateTime birthDate, short signedPlan, bool isDependent)
        {
            ValidaCategoria(name, email, birthDate, signedPlan, isDependent);
        }
        private void ValidaCategoria(string name, string email, DateTime birthDate, short signedPlan, bool isDependent)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("O nome é inválido");

            if (string.IsNullOrEmpty(email))
                throw new InvalidOperationException("O email é inválido");

            if (DateTime.MinValue == birthDate)
                throw new InvalidOperationException("A data de nascimento é inválida");

            if (signedPlan == null || signedPlan <= 0)
                throw new InvalidOperationException("O plano é inválido");
                
            if (isDependent == null)
                throw new InvalidOperationException("A dependência do cliente é inválida");
        }
    }
}