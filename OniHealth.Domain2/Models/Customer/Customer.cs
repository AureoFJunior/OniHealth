using System;

namespace OniHealth.Domain.Models
{
    public class Customer : BaseEntity
    {
        public Customer() { }

        public Customer(string name, string email, DateTime birthDate, short signedPlan, bool isDependent, string phoneNumber)
        {
            ValidateCategory(name, email, birthDate, signedPlan, isDependent, phoneNumber);
            Name = name;
            Email = email;
            BirthDate = birthDate;  
            SignedPlan = signedPlan;
            IsDependent = isDependent;
            PhoneNumber = phoneNumber;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public short SignedPlan { get; set; }
        public bool IsDependent { get; set; }
        public string PhoneNumber { get; set; }

        public void Update(string name, string email, DateTime birthDate, short signedPlan, bool isDependent, string phoneNumber)
        {
            ValidateCategory(name, email, birthDate, signedPlan, isDependent, phoneNumber);
        }
        private void ValidateCategory(string name, string email, DateTime birthDate, short signedPlan, bool isDependent, string phoneNumber)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("The name is invalid");

            if (string.IsNullOrEmpty(email))
                throw new InvalidOperationException("The email is invalid");

            if (DateTime.MinValue == birthDate)
                throw new InvalidOperationException("Birth date is invalid");

            if (signedPlan == null || signedPlan <= 0)
                throw new InvalidOperationException("The plan is invalid");
                
            if (isDependent == null)
                throw new InvalidOperationException("The customer's dependency is invalid");

            if (string.IsNullOrEmpty(phoneNumber))
            {
                throw new InvalidOperationException("The phone number is invalid");
            }
        }
    }
}