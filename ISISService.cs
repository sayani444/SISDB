﻿using SIS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Service
{
    internal interface ISISService
    {
        void DisplayStudentInfoS();
  
        void DisplayCourseInfoS();

        void DisplayTeacherInfoS();

        void GetEnrollmentsS();
        //Error in GetEnrolledCourses()
        void GetEnrolledCoursesS();

        void UpdateTeacherInfoS();

        void GetPaymentHistoryS();

        void EnrollInCourseS();

        void UpdateStudentInfoS();

        void MakePaymentS();
        //Error in UpdateCourseInfo
        void UpdateCourseInfoS();

        void GetTeacherS();
       
        void GetCourseS();

        void GetPaymentDateS();

        void GetPaymentAmountS();

        void GenerateEnrollmentReportS();

        void CalculateCourseStatisticsS();

        void GetStudentS();
        //Error in GetAssignedCoursesS();
        void GetAssignedCoursesS();
        //Not Working
        void AssignTeacherToCourseS();

    }
}
