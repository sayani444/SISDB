using SIS.Model;
using SIS.Repository;
using SIS.Service;

namespace SIS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ISISService sisService = new SISService();

            int choice;
            do
            {
                Console.WriteLine("1. Display Student Info");
                Console.WriteLine("2. Display Teacher Info");
                Console.WriteLine("3. Display Course Info");
                Console.WriteLine("4. Get Enrolled Courses");
                Console.WriteLine("5. Update Teacher Info");
                Console.WriteLine("6. Get Payment History");
                Console.WriteLine("7. Enroll in Course");
                Console.WriteLine("8. Update Student Info");
                Console.WriteLine("9. Make Payment");
                Console.WriteLine("10. Update Course Info");
                Console.WriteLine("11. Get Teacher");
                Console.WriteLine("12. Get Course");
                Console.WriteLine("13. Get Payment Date");
                Console.WriteLine("14. Get Payment Amount");
                Console.WriteLine("15. Generate Enrollment Report");
                Console.WriteLine("16. Calculate Course Statistics");
                Console.WriteLine("17. Get Student");
                Console.WriteLine("18. Get Assigned Courses");
                Console.WriteLine("19. Assign Teacher to Course");
                Console.WriteLine("0. Exit");

                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        sisService.DisplayStudentInfoS();
                        break;
                    case 2:
                        sisService.DisplayTeacherInfoS();
                        break;
                    case 3:
                        sisService.DisplayCourseInfoS();
                        break;
                    case 4:
                        sisService.GetEnrolledCoursesS();
                        break;
                    case 5:
                        sisService.UpdateTeacherInfoS();
                        break;
                    case 6:
                        sisService.GetPaymentHistoryS();
                        break;
                    case 7:
                        sisService.EnrollInCourseS();
                        break;
                    case 8:
                        sisService.UpdateStudentInfoS();
                        break;
                    case 9:
                        sisService.MakePaymentS();
                        break;
                    case 10:
                        sisService.UpdateCourseInfoS();
                        break;
                    case 11:
                        sisService.GetTeacherS();
                        break;
                    case 12:
                        sisService.GetCourseS();
                        break;
                    case 13:
                        sisService.GetPaymentDateS();
                        break;
                    case 14:
                        sisService.GetPaymentAmountS();
                        break;
                    case 15:
                        sisService.GenerateEnrollmentReportS();
                        break;
                    case 16:
                        sisService.CalculateCourseStatisticsS();
                        break;
                    case 17:
                        sisService.GetStudentS();
                        break;
                    case 18:
                        sisService.GetAssignedCoursesS();
                        break;
                    case 19:
                        sisService.AssignTeacherToCourseS();
                        break;
                    case 0:
                        Console.WriteLine("Exiting program.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }

            } while (choice != 0);

        }
    }
}
