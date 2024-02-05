using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Model
{
    internal class Course
    {
        public Course() { }

    public Course(int courseID, string courseName, int credits, string instructorName)
        {
            CourseID = courseID;
            CourseName = courseName;
            Credits = credits;
            InstructorName = instructorName;
        }

        int courseID;
        string courseName, courseCode, instructorName;

        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public string InstructorName { get; set; }

     
    }
}
