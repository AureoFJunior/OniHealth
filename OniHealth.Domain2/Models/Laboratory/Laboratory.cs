namespace OniHealth.Domain.Models
{
    public class Laboratory : BaseEntity
    {
        public Laboratory() { }

        public Laboratory(string adress, string zipCode, string phoneNumber, bool isAuthorized)
        {
            ValidateCategory(adress, zipCode, phoneNumber);
            Adress = adress;
            ZipCode = zipCode;
            PhoneNumber = phoneNumber;
            IsAuthorized = isAuthorized;
        }

        public string Adress { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAuthorized { get; set; }

        public void Update(string adress, string zipCode, string phoneNumber)
        {
            ValidateCategory(adress, zipCode, phoneNumber);
        }
        private void ValidateCategory(string adress, string zipCode, string phoneNumber)
        {
            if (string.IsNullOrEmpty(adress))
                throw new InvalidOperationException("The laboratory's adress is invalid");

            if (string.IsNullOrEmpty(zipCode))
                throw new InvalidOperationException("The laboratory's zip code is invalid");

            if (string.IsNullOrEmpty(phoneNumber))
                throw new InvalidOperationException("The laboratory's phone number is invalid");

        }
    }
}