using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OniHealth.Domain.Models
{
    public class Plans : BaseEntity
    {
        public Plans() { }

        public Plans(string name, string details, int totalValue, bool hasEmergency)
        {
            ValidateCategory(name, details, totalValue, hasEmergency);
            Name = name;
            Details = details;
            TotalValue = totalValue;
            HasEmergency = hasEmergency;
        }

        public string Name { get; set; }
        public string Details { get; set; }
        public int TotalValue { get; set; }
        public bool HasEmergency { get; set; }

        public void Update(string name, string details, int totalValue, bool hasEmergency)
        {
            ValidateCategory(name, details, totalValue, hasEmergency);
        }

        private void ValidateCategory(string name, string details, int totalValue, bool hasEmergency)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("The name is invalid");

            if (string.IsNullOrEmpty(details))
                throw new InvalidOperationException("The details is invalid");

            if (totalValue <= 0)
                throw new InvalidOperationException("The total value is invalid");

            if (hasEmergency == null)
                throw new InvalidOperationException("The Plan's emergency is invalid");
        }
    }
}

