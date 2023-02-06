using OniHealth.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace OniHealth.Domain.DTOs
{
    public class ConsultInsertDTO
    {

        public ConsultDTO Consult { get; set; }
        public ConsultTypeDTO ConsultType { get; set; }
    }
}