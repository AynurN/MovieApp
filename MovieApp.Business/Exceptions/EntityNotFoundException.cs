using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public int StatusCode { get; set; }

        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string? message) : base(message)
        {
        }
        public EntityNotFoundException(string? message, int statusCode) : base(message)
        {
           StatusCode = statusCode;
        }
    }
}
