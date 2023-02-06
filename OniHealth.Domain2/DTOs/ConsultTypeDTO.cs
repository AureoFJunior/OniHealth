using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OniHealth.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OniHealth.Domain.DTOs
{
    public class ConsultTypeDTO : BaseEntity
    {

        public string Name { get; set; }
        public string Details { get; set; }
    }
}