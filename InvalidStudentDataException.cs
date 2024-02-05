using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Exceptions
{
    internal class InvalidStudentDataException:ApplicationException
    {
        InvalidStudentDataException() { }

        public InvalidStudentDataException(string message) : base(message)
        {

        }
    }
}
