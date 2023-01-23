using OniHealth.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OniHealth.Web.Config
{
    public interface IValidator
    {
        StatusCodeReturn Return { get; }
        string[] Messages { get; }
        bool HasNotification { get; }
        void AddMessage(string msg);
        void AddMessages(IList<string> msgs);
        void AddMessages(ICollection<string> msgs);
        void AddMessages(ValidationResult validationResult);
        void AsNotFound();
        void AsNotFound(string msg);
    }
}
