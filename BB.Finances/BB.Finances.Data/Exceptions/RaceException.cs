using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Finances.Data.Exceptions
{
    public class RaceException : GeneralException
    {
        public RaceException()
        {
        }

        public RaceException(string? message) : base(message)
        {
        }

        public RaceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
