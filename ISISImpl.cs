using Microsoft.Data.SqlClient;
using SIS.Utility;
using SIS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIS.Exceptions;

namespace SIS.Repository
{
    internal class ISISImpl:ISIS
    {
        public string connectionString = DBConnUtil.GetConnectionString();
        SqlConnection sqlconnection = null;
        SqlCommand cmd = null;

        public ISISImpl()
        {
            sqlconnection = new SqlConnection(connectionString);
            cmd = new SqlCommand();
        }

        public Student DisplayStudentInfo(int student_id )
        { 
            Student student = new Student();
            cmd.CommandText = "select * from Students where student_ID = @ID;";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ID", student_id);
            cmd.Connection = sqlconnection;
            sqlconnection.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    student.StudentID = (int)reader["student_ID"];
                    student.FirstName = (string)reader["first_Name"];
                    student.LastName = (string)reader["last_Name"];
                    student.DateOfBirth = (DateTime)reader["date_Of_Birth"];
                    student.Email = (string)reader["email"];
                    student.PhoneNumber = (string)reader["phone_Number"];
                }
                else
                {
                    sqlconnection.Close();

                    throw new StudentNotFoundException($"Student with ID {student_id} not found.");
                }
            }

            sqlconnection.Close();
            return student;

        }

        public Course DisplayCourseInfo(int course_id)
        {
            Course course = new Course();
            cmd.CommandText = "select * from Courses where course_ID = @ID;";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ID", course_id);
            cmd.Connection = sqlconnection;
            sqlconnection.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    course.CourseID = (int)reader["course_ID"];
                    course.CourseName = (string)reader["course_Name"];
                    course.Credits = (int)reader["credits"];
                    int tid = (int)(reader)["teacher_id"];
                    sqlconnection.Close();
                    cmd.CommandText = "select first_name,last_name from Teacher where teacher_ID = @ID;";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", tid);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    using (SqlDataReader reader1 = cmd.ExecuteReader())
                    {
                        if (reader1.Read())
                        {
                            course.InstructorName = (string)reader1["first_name"];
                            course.InstructorName += " " + (string)reader1["last_name"];
                        }
                                   
                    } 
                }

                else
                {
                    sqlconnection.Close();

                    throw new CourseNotFoundException($"Course with ID {course_id} not found.");
                }
            }
            sqlconnection.Close();
            return course; 
        }

        public Teacher DisplayTeacherInfo(int id)
        {
            Teacher teacher = new Teacher();
            cmd.CommandText = "select * from Teacher where teacher_ID = @ID;";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Connection = sqlconnection;
            sqlconnection.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    teacher.TeacherID = (int)reader["teacher_ID"];
                    teacher.FirstName = (string)reader["first_Name"];
                    teacher.LastName = (string)reader["last_Name"];
                    teacher.Email = (string)reader["email"];
                }
                else
                {
                    sqlconnection.Close();

                    throw new TeacherNotFoundException($"Teacher with ID {id} not found.");
                }
            }

            sqlconnection.Close();


            return teacher;

        }

        public List<Enrollment> GetEnrollments()
        {
            List<Enrollment> enrollmentList = new List<Enrollment>();
            try
            {
                cmd.CommandText = "select * from Enrollments";
                cmd.Parameters.Clear();
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Enrollment enrollment = new Enrollment();
                    enrollment.EnrollmentID = (int)reader["enrollment_id"];
                    Student student = new Student { StudentID = (int)reader["student_id"] };
                    enrollment.Student = student;
                    Course course = new Course { CourseID = (int)reader["course_id"] };
                    enrollment.Course = course;
                    enrollment.EnrollmentDate = (DateTime)reader["enrollment_date"];
       
                    enrollmentList.Add(enrollment);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error Occured:{ex.Message}");
            }
            sqlconnection.Close();
            return enrollmentList;
        }

        public List<Course> GetEnrolledCourses(int student_id)
        { 
            List<Course> courseList = new List<Course>();
            try
            {
                cmd.CommandText = "select * from Courses where course_id in (select course_id from Enrollments where student_id=@id)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id",student_id);
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(!reader.HasRows) 
                {
                    sqlconnection.Close();
                    throw new StudentNotFoundException($"Student with ID {student_id} not found.");
                }
                while (reader.Read())
                {
                    Course course = new Course();
                    course.CourseID = (int)reader["course_id"];
                    course.CourseName = (string)reader["course_name"];
                    course.Credits = (int)reader["credits"];
                    int tid = (int)(reader)["teacher_id"];
                    sqlconnection.Close();
                    cmd.CommandText = "select first_name,last_name from Teacher where teacher_ID = @ID;";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", tid);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader1 = cmd.ExecuteReader();
                    while (reader1.Read())
                    {   
                            course.InstructorName = (string)reader1["first_name"];
                            course.InstructorName += " " + (string)reader1["last_name"];
                        
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error Occured:{ex.Message}");
            }
            sqlconnection.Close();
            return courseList;
        }


        public void UpdateTeacherInfo(int teacherID,string firstname, string lastname, string email)
        {

                cmd.CommandText = "UPDATE Teacher SET first_name = @firstname,last_name=@lastname,email=@email WHERE teacher_ID=@teacherID";

                sqlconnection.Open();
                cmd.Connection = sqlconnection;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@teacherID", teacherID);
                cmd.Parameters.AddWithValue("@firstname", firstname);
                cmd.Parameters.AddWithValue("@lastname", lastname);
                cmd.Parameters.AddWithValue("@email", email);

                int updateTeacherStatus = cmd.ExecuteNonQuery();
                sqlconnection.Close();

                if (updateTeacherStatus > 0)
                {
                    Console.WriteLine("Teacher Updated successfully.");
                }
                else
                {
                    sqlconnection.Close();
                    throw new TeacherNotFoundException($"Teacher with id {teacherID} not found.");
                    
                }

        }

        public List<Payment> GetPaymentHistory(int student_id)
        {
            List<Payment> paymentList = new List<Payment>();

            
                cmd.CommandText = "select * from Payments where student_id=@student_id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@student_id", student_id);
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                sqlconnection.Close ();
                throw new StudentNotFoundException($"Student with id {student_id} n");
                }    
                while (reader.Read())
                {
                    Payment payment = new Payment();

                    payment.PaymentID = (int)reader["payment_id"];
                    Student student = new Student { StudentID = (int)reader["student_id"] };
                    payment.Student = student;
                    payment.PaymentDate = (DateTime)reader["payment_date"];
                    payment.Amount = (decimal)reader["amount"];
                    paymentList.Add(payment);
                }
        
    
            sqlconnection.Close();
            return paymentList;
        }

        public void EnrollInCourse(Course course,int student_id)
        {
            try
            {
                cmd.CommandText = "INSERT INTO enrollments VALUES (@student_id, @course_id,GetDate());";
                sqlconnection.Open();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@student_id", student_id);
                cmd.Parameters.AddWithValue("@course_id", course.CourseID);
                cmd.Connection = sqlconnection;

                int addEnrollmentStatus = cmd.ExecuteNonQuery();
                sqlconnection.Close();
                if (addEnrollmentStatus > 0)
                {
                    Console.WriteLine("Enrolled Successfully");
                }
                else
                {
                    Console.WriteLine("Error.Enrollment unsuccessful.");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void UpdateStudentInfo(int student_id,string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            try
            {
                cmd.CommandText = "UPDATE Students SET first_name = @firstname,last_name=@lastname,email=@email,date_of_birth=@dateOfBirth,phone_Number=@phoneNumber WHERE student_id=@student_id";

                sqlconnection.Open();
                cmd.Connection = sqlconnection;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@student_id", student_id);
                cmd.Parameters.AddWithValue("@firstname", firstName);
                cmd.Parameters.AddWithValue("@lastname", lastName);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
                cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                int updateStudentStatus = cmd.ExecuteNonQuery();
                sqlconnection.Close();

                if (updateStudentStatus > 0)
                {
                    Console.WriteLine("Student Updated successfully.");
                }
                else
                {
                    sqlconnection.Close();
                    throw new StudentNotFoundException($"Student with id {student_id}");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void MakePayment(int student_id,decimal amount)
        {
            try
            {

                cmd.CommandText = "INSERT INTO Payments VALUES (@student_id, @amount,getdate());";
                sqlconnection.Open();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@student_id", student_id);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Connection = sqlconnection;

                int makePaymentStatus = cmd.ExecuteNonQuery();
                sqlconnection.Close();
                if (makePaymentStatus > 0)
                {
                    Console.WriteLine("Payment made Successfully");
                }
                else
                {
                    sqlconnection.Close();
                    throw new StudentNotFoundException($"Student with id {student_id} n");
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"({ex.Message})");
            }
        }

        public void UpdateCourseInfo(int course_id,int credits, string courseName, int teacher_id)
        {
                cmd.CommandText = "UPDATE Courses SET course_Name = @courseName,credits=@credits,teacher_id=@teacher_id WHERE course_id=@course_id";

                sqlconnection.Open();
                cmd.Connection = sqlconnection;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@courseName", courseName);
                cmd.Parameters.AddWithValue("@credits", credits);
                cmd.Parameters.AddWithValue("@teacher_id", teacher_id);
                cmd.Parameters.AddWithValue("@course_id", course_id);
                int updateCourseStatus = cmd.ExecuteNonQuery();
                sqlconnection.Close();

            if (updateCourseStatus > 0)
            {
                Console.WriteLine("Course Updated successfully.");
            }
            else
            {
                sqlconnection.Close();
                throw new CourseNotFoundException($"Course with id {course_id} not found.");
            }
            
        }

        public Teacher GetTeacher(int course_id)
        {
            Teacher teacher=new Teacher();

                cmd.CommandText = "select * from Teacher where teacher_id=(select teacher_id from Courses where course_id=@course_id)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@course_id", course_id);
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows) 
                {
                    sqlconnection.Close();
                    throw new CourseNotFoundException($"Course with id {course_id} not found.");
                }
                while (reader.Read())
                {
                    teacher.TeacherID = (int)reader["teacher_id"];
                    teacher.FirstName = (string)reader["first_name"];
                    teacher.LastName = (string)reader["last_name"];
                    teacher.Email = (string)reader["email"];
                }
          
            sqlconnection.Close();
            return teacher;
        }

        public Course GetCourse(int enrollment_id)
        {
            Course course = new Course();
                cmd.CommandText = "select * from Courses where course_id  = (select distinct course_id from Enrollments where enrollment_id=@enrollment_id)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@enrollment_id", enrollment_id);
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    sqlconnection.Close();
                    throw new InvalidEnrollmentDataException($"Enrollment with id {enrollment_id} not found.");
                }
                while (reader.Read())
                {
                    course.CourseID = (int)reader["course_id"];
                    course.CourseName = (string)reader["course_name"];
                    course.Credits = (int)reader["credits"];
                    int course_id = (int)reader["course_id"];
                    sqlconnection.Close();
                    cmd.CommandText = "select first_name,last_name from teacher where teacher_id = (select distinct teacher_id from Courses where course_id=@course_id)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();

                    SqlDataReader reader1 = cmd.ExecuteReader();
                    string name;
                    while (reader1.Read()) 
                    { 
                      name = (string)reader1["first_name"] + " " + (string)reader1["last_name"];
                      course.InstructorName = name;
                    }
                    
                }
 
            sqlconnection.Close();
            return course;
        }



        public DateTime GetPaymentDate(int payment_id)
        {
            DateTime date = DateTime.Now;

            try
            {
                cmd.CommandText = "select payment_date from Payments where payment_id=@payment_id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@payment_id", payment_id);
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    date = (DateTime)reader["payment_date"];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error Occured:{ex.Message}");
                sqlconnection.Close();
            }
            sqlconnection.Close();
            return date;
        }

        public decimal GetPaymentAmount(int payment_id)
        {
            decimal amount= 0.0m;
            try
            {
                cmd.CommandText = "select amount from Payments where payment_id=@payment_id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@payment_id", payment_id);
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    amount = (decimal)reader["amount"];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error Occured:{ex.Message}");
                sqlconnection.Close();
            }
            sqlconnection.Close();
            return amount;
        }

        public void GenerateEnrollmentReport(Course course)
        {
            int course_id = course.CourseID;

         
                cmd.CommandText = "select * from Students where student_id in (select student_id from Enrollments where course_id=@course_id)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@course_id", course_id);
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    sqlconnection.Close();
                    throw new CourseNotFoundException($"Course with id {course_id} not found.");
                }
                while (reader.Read())
                {
                    Console.WriteLine($"Student ID:{(int)reader["student_id"]}\tFirst Name:{(string)reader["First_Name"]}\tLast Name:{(string)reader["Last_Name"]}\tDate of Birth:{(DateTime)reader["Date_Of_Birth"]}\tEmail:{(string)reader["Email"]}\tPhone Number:{(string)reader["phone_number"]}");
                }
                sqlconnection.Close();

       
            
        }

        public void CalculateCourseStatistics(Course course)
        {
            int course_id = course.CourseID;

            try
            {
                cmd.CommandText = "select count(*) as num from Enrollments where course_id=@course_id group by course_id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@course_id", course_id);
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Number of enrollments:{(int)reader["num"]}");
                }
                sqlconnection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error Occured:{ex.Message}");
                sqlconnection.Close();
            }
        }

        public Student GetStudent(int enrollment_id)
        { 
            Student student=new Student();

                cmd.CommandText = "select * from Students where student_id = (select student_id from Enrollments where enrollment_id=@enrollment_id)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@enrollment_id", enrollment_id);
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    sqlconnection.Close();
                    throw new InvalidEnrollmentDataException($"Enrollment with id {enrollment_id} not found.");
                }
                while (reader.Read())
                {
                    student.StudentID = (int)reader["student_ID"];
                    student.FirstName = (string)reader["first_Name"];
                    student.LastName = (string)reader["last_Name"];
                    student.DateOfBirth = (DateTime)reader["date_Of_Birth"];
                    student.Email = (string)reader["email"];
                    student.PhoneNumber = (string)reader["phone_Number"];
                }
                sqlconnection.Close();

         
            return student;
        }

        public List<Course> GetAssignedCourses(int teacher_id)
        {
            List<Course> courseList = new List<Course>();

          
                cmd.CommandText = "select * from Courses where teacher_id=@teacher_id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@teacher_id", teacher_id);
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    sqlconnection.Close();
                    throw new TeacherNotFoundException($"Teacher with id {teacher_id} not found.");
                }
                while (reader.Read())
                {
                    Course course = new Course();

                    course.CourseID = (int)reader["course_id"];
                    int course_id = (int)reader["course_id"];
                    course.CourseName = (string)reader["course_name"];
                    course.Credits = (int)reader["credits"];
                    sqlconnection.Close();
                    cmd.CommandText = "select first_name,last_name from teacher where teacher_id = (select distinct teacher_id from Courses where course_id=@course_id)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader1 = cmd.ExecuteReader();
                    string name;
                    while (reader1.Read())
                    {
                        name = (string)reader1["first_name"] + " " + (string)reader1["last_name"];
                        course.InstructorName = name;
                    }

                    courseList.Add(course);
                }
        
            sqlconnection.Close();
            return courseList;

        }

        public void AssignTeacherToCourse(Teacher teacher, Course course)
        {
            try
            {
                cmd.CommandText = "Update Courses SET teacher_id=@teacher_id where course_id=@course_id;";
                sqlconnection.Open();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@teacher_id", teacher.TeacherID);
                cmd.Parameters.AddWithValue("@course_id", course.CourseID);
                cmd.Connection = sqlconnection;

                int courseAssignStatus = cmd.ExecuteNonQuery();
                sqlconnection.Close();
                if (courseAssignStatus > 0)
                {
                    Console.WriteLine("Course Assigned Successfully");
                }
                else
                {
                    Console.WriteLine("Error.Course Assignment unsuccessful.");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                sqlconnection.Close();

            }

        }

    }
}
