using System;
using System.ComponentModel.DataAnnotations;

namespace JREST.Models
{
    public class HousingApplication
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; } // لمعرفة أي طالبة قامت بتقديم الطلب

        // حقل تاريخ تقديم الطلب (يُحفظ تلقائياً عند إنشاء الطلب)
        public DateTime SubmissionDate { get; set; } = DateTime.Now;

        // بيانات الخطوة الأولى
        public string UniversityId { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Major { get; set; } = "";
        public decimal Gpa { get; set; }
        public decimal TawjihiGpa { get; set; }

        // بيانات الخطوة الثانية
        public DateTime? EnrollmentDate { get; set; }
        public string StudentPhone { get; set; } = "";
        public string ParentPhone { get; set; } = "";
        public string PassportNumber { get; set; } = "";
        public string MaritalStatus { get; set; } = "";
        public string Religion { get; set; } = "";
        public DateTime? BirthDate { get; set; }
        public string Nationality { get; set; } = "";

        // بيانات الخطوة الثالثة
        public string? AcademicLevel { get; set; }
        public string? AcademicYear { get; set; }
        public string? Semester { get; set; } // <--- الحقل الجديد الخاص بالفصل الدراسي
        public string? HousingName { get; set; }
        public string? StudyType { get; set; }
        public string? College { get; set; }
        public string? Penalties { get; set; }
        public string? PledgeStudentName { get; set; }
        public string? PledgeParentName { get; set; }

        // بيانات الخطوة الرابعة (العنوان السكني الدائم)
        public string? Governorate { get; set; }
        public string? HousingType { get; set; }
        public string? City { get; set; }
        public string? BuildingNumber { get; set; }
        public string? Street { get; set; }
        public string? Neighborhood { get; set; }

        // بيانات الخطوة الخامسة (المعلومات الصحية)
        public string? HealthInsurance { get; set; }
        public string? MedicalCareNeeded { get; set; }
        public bool HasAllergy { get; set; }
        public string? AllergyDetails { get; set; }
        public bool HasDisability { get; set; }
        public string? DisabilityDetails { get; set; }
        public bool NeedsMedicine { get; set; }
        public string? MedicineDetails { get; set; }

        // بيانات الخطوة السادسة (الأمراض المزمنة)
        public bool HasHypertension { get; set; }
        public bool HasPsychologicalDisorder { get; set; }
        public bool HasHeartDisease { get; set; }
        public bool HasEpilepsy { get; set; }
        public bool HasDiabetes { get; set; }
        public bool HasAsthma { get; set; }
        public bool HasCancer { get; set; }

        // بيانات الخطوة السابعة (الصلاحيات والموافقات)
        public bool AllowWeekendLeave { get; set; }
        public bool AllowDayTrips { get; set; }
        public bool AllowMultiDayTrips { get; set; }
        public bool AllowTravelAbroad { get; set; }

        // بيانات الخطوة الثامنة والأخيرة (مسارات الملفات المرفوعة)
        public string? HealthCertificatePath { get; set; }
        public string? FamilyBookPath { get; set; }
        public string? PowerOfAttorneyPath { get; set; }
        public string? GuardianIdPath { get; set; }

        public string Status { get; set; } = "قيد الانتظار"; // حالة الطلب
    }
}