using System;
using System.ComponentModel.DataAnnotations;

namespace JREST.Models
{
    public class HousingApplication
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; } // لمعرفة أي طالبة قامت بتقديم الطلب

        // بيانات الخطوة الأولى
        public string UniversityId { get; set; }
        public string FullName { get; set; }
        public string Major { get; set; }
        public decimal Gpa { get; set; }
        public decimal TawjihiGpa { get; set; }

        // بيانات الخطوة الثانية
        public DateTime? EnrollmentDate { get; set; }
        public string StudentPhone { get; set; }
        public string ParentPhone { get; set; }
        public string PassportNumber { get; set; }
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Nationality { get; set; }

        public string Status { get; set; } = "قيد الانتظار"; // حالة الطلب
    }
}