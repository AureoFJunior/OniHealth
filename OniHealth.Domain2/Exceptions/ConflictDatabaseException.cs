using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniHealth.Domain
{
    public class ConflictDatabaseException : Exception
    {
        public ConflictDatabaseException()
        {
        }

        public ConflictDatabaseException(string message)
            : base(message)
        {
        }

        public ConflictDatabaseException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
