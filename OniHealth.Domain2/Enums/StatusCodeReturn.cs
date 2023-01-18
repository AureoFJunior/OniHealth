using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniHealth.Domain.Enums
{
    public enum StatusCodeReturn
    {
        BadRequest,
        NotFound,
        Conflict,
        OK,
        InternalServerError
    }
}
