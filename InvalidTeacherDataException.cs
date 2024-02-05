using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Exceptions
{
    internal class InvalidTeacherDataException:ApplicationException
    {
        InvalidTeacherDataException() { }

        public InvalidTeacherDataException(string message) : base(message)
        {

        }
    }
}
