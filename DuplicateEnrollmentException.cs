using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Exceptions
{
    internal class DuplicateEnrollmentException:ApplicationException
    {
        DuplicateEnrollmentException() { }

        public DuplicateEnrollmentException(string message) : base(message)
        {

        }

    }
}
