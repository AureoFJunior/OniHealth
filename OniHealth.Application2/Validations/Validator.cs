using OniHealth.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OniHealth.Web.Config
{
    public class Validator : IValidator
    {
        public StatusCodeReturn Return { get; private set; }
        private readonly List<string> _messages;

        public string[] Messages => _messages.ToArray();
        public bool HasNotification => Return != StatusCodeReturn.OK;

        public Validator()
        {
            Return = StatusCodeReturn.OK;
            _messages = new List<string>();
        }

        public void AddMessage(string msg)
        {
            Return = StatusCodeReturn.BadRequest;
            _messages?.Add(msg);
        }

        public void AddMessages(IList<string> msgs)
        {
            Return = StatusCodeReturn.BadRequest;
            _messages?.AddRange(msgs);
        }

        public void AddMessages(ICollection<string> msgs)
        {
            Return = StatusCodeReturn.BadRequest;
            _messages?.AddRange(msgs);
        }

        public void AddMessages(ValidationResult validationResult)
        {
            Return = StatusCodeReturn.BadRequest;
            AddMessage(validationResult.ErrorMessage);
        }

        public void AsNotFound()
        {
            Return = StatusCodeReturn.NotFound;
        }
    }
}
