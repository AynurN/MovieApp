using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Exceptions
{
    public class UnsuccesfulOperationException : Exception
    {
        public UnsuccesfulOperationException()
        {
        }

        public UnsuccesfulOperationException(string? message) : base(message)
        {
        }
    }
}
