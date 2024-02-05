using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Model
{
 
    internal class Payment
    {
        public Payment() { }
        public Payment(int paymentID, Student student, decimal amount, DateTime paymentDate)
        {
            PaymentID = paymentID;
            Student = student;
            Amount = amount;
            PaymentDate = paymentDate;
        }

        int paymentID;
        decimal amount;
        DateTime paymentDate;
        Student student;

        public int PaymentID { get; set; }
        public Student Student { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public Student GetStudent()
        {
            return Student;
        }

        public decimal GetPaymentAmount()
        {
            return Amount;
        }

        public DateTime GetPaymentDate()
        {
            return PaymentDate;
        }


    }
}
