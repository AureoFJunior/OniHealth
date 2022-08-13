using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniHealth.Domain
{
    public class NotFoundDatabaseException : Exception
    {
        public NotFoundDatabaseException()
        {
        }

        public NotFoundDatabaseException(string message)
            : base(message)
        {
        }

        public NotFoundDatabaseException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
