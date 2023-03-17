using OniHealth.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OniHealth.Domain.Models
{
    public class Customer : BaseEntity
    {
        public Customer() { }

        public Customer(string name, string email, DateTime birthDate, int signedPlanId, bool isDependent, string phoneNumber, DateTime lastPaymentDate)
        {
            ValidateCategory(name, email, birthDate, signedPlanId, isDependent, phoneNumber, lastPaymentDate);
            Name = name;
            Email = email;
            BirthDate = birthDate;  
            SignedPlanId = signedPlanId;
            IsDependent = isDependent;
            PhoneNumber = phoneNumber;
            LastPaymentDate = lastPaymentDate;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        [ForeignKey(nameof(Plans))]
        public int SignedPlanId { get; set; }
        public virtual Plans Plans { get; set; }

        public bool IsDependent { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime LastPaymentDate { get; set; }

        public void Update(string name, string email, DateTime birthDate, int signedPlanId, bool isDependent, string phoneNumber, DateTime lastPaymentDate)
        {
            ValidateCategory(name, email, birthDate, signedPlanId, isDependent, phoneNumber, lastPaymentDate);
        }
        private void ValidateCategory(string name, string email, DateTime birthDate, int signedPlanId, bool isDependent, string phoneNumber, DateTime lastPaymentDate)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("The name is invalid");

            if (string.IsNullOrEmpty(email))
                throw new InvalidOperationException("The email is invalid");

            if (DateTime.MinValue == birthDate)
                throw new InvalidOperationException("Birthdate is invalid");

            if (signedPlanId <= 0)
               // throw new InvalidOperationException("The signed plan is invalid");
                
            if (isDependent == null)
                throw new InvalidOperationException("The customer's dependency is invalid");

            if (string.IsNullOrEmpty(phoneNumber))
                throw new InvalidOperationException("The phone number is invalid");

            if (DateTime.MinValue == lastPaymentDate)
                throw new InvalidOperationException("The Last Payment Date is invalid");
        }
    }
}