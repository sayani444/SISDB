using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Model
{
    internal class Teacher
    {
        public Teacher() { }
        public Teacher(int teacherID, string firstName, string lastName, string email)
        {
            TeacherID = teacherID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        int teacherID;
        string firstName;
        string lastName;
        string email;

        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

      

    }
}
