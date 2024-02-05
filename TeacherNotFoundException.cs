using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Exceptions
{
    internal class TeacherNotFoundException:ApplicationException
    {
        TeacherNotFoundException() { }

        public TeacherNotFoundException(string message) : base(message)
        {

        }
    }
}
