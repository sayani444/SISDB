using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Exceptions
{
    internal class CourseNotFoundException:ApplicationException
    {
        CourseNotFoundException() { }

        public CourseNotFoundException(string message) : base(message)
        {

        }
    }
}
