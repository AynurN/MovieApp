using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Exceptions
{
    public class GenreAlreadyExistsException : Exception
    {
        public int StatusCode { get; set; }
        public string PropName { get; set; }

        public GenreAlreadyExistsException()
        {
        }

        public GenreAlreadyExistsException(string? message) : base(message)
        {
        }
        public GenreAlreadyExistsException(string? message, int statusCode, string propName) : base(message)
        {
            StatusCode = statusCode;
            PropName = propName;
        }
    }
}
