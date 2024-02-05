using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Exceptions
{
    internal class InvalidEnrollmentDataException:ApplicationException
    {
        InvalidEnrollmentDataException() { }

        public InvalidEnrollmentDataException(string message) : base(message)
        {

        }
    }
}
