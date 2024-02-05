using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Exceptions
{
    internal class StudentNotFoundException:ApplicationException
    {
        StudentNotFoundException() { }

        public StudentNotFoundException(string message) : base(message)
        {

        }
    }
}
