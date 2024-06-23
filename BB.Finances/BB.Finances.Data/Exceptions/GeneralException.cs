﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BB.Finances.Data.Exceptions
{
    public class GeneralException : Exception
    {
        public GeneralException()
        {
        }

        public GeneralException(string? message) : base(message)
        {
        }

        public GeneralException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
