using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialRequisition.Application.Constants
{
    public static class ResponseCodes
    {
        public const int SUCCESS = 200;
        public const int BAD_REQUEST = 400;
        public const int UNAUTHORIZED = 401;
        public const int FORBIDDEN = 403;
        public const int NOT_FOUND = 404;
        public const int RECORD_EXISTS = 409;
        public const int SERVER_ERROR = 500;
    }
}
