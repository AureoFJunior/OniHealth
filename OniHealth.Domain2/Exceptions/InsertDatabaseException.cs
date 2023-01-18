using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniHealth.Domain
{
    public class InsertDatabaseException : Exception
    {
        public InsertDatabaseException()
        {
        }

        public InsertDatabaseException(string message)
            : base(message)
        {
        }

        public InsertDatabaseException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
