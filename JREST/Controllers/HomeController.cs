using System.Diagnostics;
using JREST.Data;
using JREST.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JREST.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View("Login");
        }

        public IActionResult student_home_page()
        {
            return View();
        }

        // --- لوحة تحكم العميد (مع دعم الفلاتر التفاعلية للعدادات) ---
        public async Task<IActionResult> dean_home_page(string status)
        {
            var query = _context.HousingApplications.AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                if (status == "مقبول مبدئياً")
                {
                    query = query.Where(h => h.Status == "مقبول مبدئياً");
                }
                else if (status == "مقبول")
                {
                    query = query.Where(h => h.Status == "مقبول");
                }
                else if (status == "مرفوض من العميد")
                {
                    query = query.Where(h => h.Status == "مرفوض من العميد");
                }
            }
            else
            {
                query = query.Where(h => h.Status == "مقبول مبدئياً");
            }

            var applications = await query.OrderByDescending(h => h.Id).ToListAsync();

            ViewBag.DeanPendingCount = await _context.HousingApplications.CountAsync(h => h.Status == "مقبول مبدئياً");
            ViewBag.DeanApprovedCount = await _context.HousingApplications.CountAsync(h => h.Status == "مقبول");
            ViewBag.DeanRejectedCount = await _context.HousingApplications.CountAsync(h => h.Status == "مرفوض من العميد");
            ViewBag.CurrentFilter = status ?? "مقبول مبدئياً";

            return View(applications);
        }

        // --- قبول الطلب نهائياً من قبل العميد وإرسال إشعار للطالبة ولرئيس الشعبة ---
        [HttpPost]
        public async Task<IActionResult> DeanApproveApplication(int id)
        {
            var application = await _context.HousingApplications.FindAsync(id);
            if (application != null)
            {
                application.Status = "مقبول";
                _context.HousingApplications.Update(application);

                var studentNotification = new Notification
                {
                    UserId = application.UserId,
                    Message = $"عزيزتي الطالبة، تهانينا! تم قبول طلب السكن الخاص بك نهائياً للفصل ({application.Semester}) للعام الدراسي ({application.AcademicYear}).",
                    CreatedAt = DateTime.Now
                };
                _context.Notifications.Add(studentNotification);

                var headUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Role.ToLower().Trim() == "head");

                if (headUser != null)
                {
                    var headNotification = new Notification
                    {
                        UserId = headUser.Id,
                        Message = $"تم قبول طلب السكن للطالبة ({application.FullName}) من قبل العميد بنجاح.",
                        CreatedAt = DateTime.Now
                    };
                    _context.Notifications.Add(headNotification);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("dean_home_page");
        }

        // --- قبول جميع الطلبات المحددة جماعياً من قبل العميد ---
        [HttpPost]
        public async Task<IActionResult> DeanApproveMultiple(List<int> selectedIds)
        {
            if (selectedIds != null && selectedIds.Any())
            {
                var applications = await _context.HousingApplications
                    .Where(a => selectedIds.Contains(a.Id) && a.Status == "مقبول مبدئياً")
                    .ToListAsync();

                var headUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Role.ToLower().Trim() == "head");

                foreach (var application in applications)
                {
                    application.Status = "مقبول";
                    _context.HousingApplications.Update(application);

                    var studentNotification = new Notification
                    {
                        UserId = application.UserId,
                        Message = $"عزيزتي الطالبة، تهانينا! تم قبول طلب السكن الخاص بك نهائياً للفصل ({application.Semester}) للعام الدراسي ({application.AcademicYear}).",
                        CreatedAt = DateTime.Now
                    };
                    _context.Notifications.Add(studentNotification);

                    if (headUser != null)
                    {
                        var headNotification = new Notification
                        {
                            UserId = headUser.Id,
                            Message = $"تم قبول طلب السكن للطالبة ({application.FullName}) من قبل العميد بنجاح.",
                            CreatedAt = DateTime.Now
                        };
                        _context.Notifications.Add(headNotification);
                    }
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("dean_home_page");
        }

        // --- رفض جميع الطلبات المحددة جماعياً من قبل العميد ---
        [HttpPost]
        public async Task<IActionResult> DeanRejectMultiple(List<int> selectedIds)
        {
            if (selectedIds != null && selectedIds.Any())
            {
                var applications = await _context.HousingApplications
                    .Where(a => selectedIds.Contains(a.Id) && a.Status == "مقبول مبدئياً")
                    .ToListAsync();

                var headUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Role.ToLower().Trim() == "head");

                foreach (var application in applications)
                {
                    application.Status = "مرفوض من العميد";
                    _context.HousingApplications.Update(application);

                    var studentNotification = new Notification
                    {
                        UserId = application.UserId,
                        Message = $"عزيزتي الطالبة، تم رفض طلب السكن الخاص بك للفصل ({application.Semester}) للعام الدراسي ({application.AcademicYear}) من قبل العميد.",
                        CreatedAt = DateTime.Now
                    };
                    _context.Notifications.Add(studentNotification);

                    if (headUser != null)
                    {
                        var headNotification = new Notification
                        {
                            UserId = headUser.Id,
                            Message = $"تم رفض طلب السكن للطالبة ({application.FullName}) من قبل العميد.",
                            CreatedAt = DateTime.Now
                        };
                        _context.Notifications.Add(headNotification);
                    }
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("dean_home_page");
        }

        // --- صفحة رئيس الشعبة (مع دعم الفلاتر التفاعلية للعدادات) ---
        public async Task<IActionResult> head_home_page(string status)
        {
            var query = _context.HousingApplications.AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                if (status == "قيد الانتظار")
                {
                    query = query.Where(h => h.Status == "قيد الانتظار");
                }
                else if (status == "مقبول مبدئياً")
                {
                    query = query.Where(h => h.Status == "مقبول مبدئياً" || h.Status == "مقبول");
                }
                else if (status == "مرفوض")
                {
                    query = query.Where(h => h.Status == "مرفوض");
                }
            }
            else
            {
                query = query.Where(h => h.Status == "قيد الانتظار");
            }

            var applications = await query.OrderByDescending(h => h.Id).ToListAsync();

            ViewBag.PendingCount = await _context.HousingApplications.CountAsync(h => h.Status == "قيد الانتظار");
            ViewBag.HeadApprovedCount = await _context.HousingApplications.CountAsync(h => h.Status == "مقبول مبدئياً" || h.Status == "مقبول");
            ViewBag.HeadRejectedCount = await _context.HousingApplications.CountAsync(h => h.Status == "مرفوض");
            ViewBag.CurrentFilter = status ?? "قيد الانتظار";

            return View(applications);
        }

        // --- قبول الطلب من قبل رئيس الشعبة (تحويله للعميد بدون إشعار للطالبة) ---
        [HttpPost]
        public async Task<IActionResult> HeadApproveApplication(int id)
        {
            var application = await _context.HousingApplications.FindAsync(id);
            if (application != null)
            {
                application.Status = "مقبول مبدئياً";
                _context.HousingApplications.Update(application);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("head_home_page");
        }

        public IActionResult contact_us()
        {
            return View();
        }

        public IActionResult privacy_policy()
        {
            return View();
        }

        public IActionResult prices()
        {
            return View();
        }

        public IActionResult confirm_acceptance()
        {
            return View();
        }

        public IActionResult confirm_rejection()
        {
            return View();
        }

        // --- فتح صفحة سبب الرفض مع استقبال رقم الطلب ---
        public IActionResult rejection_reason(int id)
        {
            ViewBag.ApplicationId = id;
            return View();
        }

        // --- حفظ سبب الرفض وتحديث حالة الطلب وإرسال إشعار للطالبة ولرئيس الشعبة إذا كان الرفض من العميد ---
        [HttpPost]
        public async Task<IActionResult> SubmitRejectionReason(int id, string reason)
        {
            var application = await _context.HousingApplications.FindAsync(id);
            if (application == null)
            {
                return Json(new { success = false, message = "الطلب غير موجود." });
            }

            if (string.IsNullOrWhiteSpace(reason))
            {
                return Json(new { success = false, message = "الرجاء كتابة سبب الرفض." });
            }

            bool isRejectedByDean = application.Status == "مقبول مبدئياً";
            string rejectionStatus = isRejectedByDean ? "مرفوض من العميد" : "مرفوض";

            application.Status = rejectionStatus;
            _context.HousingApplications.Update(application);

            var studentNotification = new Notification
            {
                UserId = application.UserId,
                Message = $"عزيزتي الطالبة، تم رفض طلب السكن الخاص بك للفصل ({application.Semester}) للعام الدراسي ({application.AcademicYear}). السبب: {reason}",
                CreatedAt = DateTime.Now
            };
            _context.Notifications.Add(studentNotification);

            if (isRejectedByDean)
            {
                var headUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Role.ToLower().Trim() == "head");

                if (headUser != null)
                {
                    var headNotification = new Notification
                    {
                        UserId = headUser.Id,
                        Message = $"تم رفض طلب السكن للطالبة ({application.FullName}) من قبل العميد. السبب: {reason}",
                        CreatedAt = DateTime.Now
                    };
                    _context.Notifications.Add(headNotification);
                }
            }

            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "تم إرسال سبب الرفض بنجاح!" });
        }

        // --- صفحة التواصل مع الطالبة واستقبال UserId ---
        public IActionResult contact_student(int userId)
        {
            ViewBag.TargetUserId = userId;
            return View();
        }

        // --- دالة إرسال الإشعار من رئيس الشعبة ---
        [HttpPost]
        public async Task<IActionResult> SendNotificationToStudent(int userId, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return Json(new { success = false, message = "نص الرسالة فارغ." });
            }

            var notification = new Notification
            {
                UserId = userId,
                Message = $"تم إرسال رسالة من رئيس الشعبة: {message}",
                CreatedAt = DateTime.Now
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "تم إرسال الإشعار بنجاح!" });
        }

        // --- صفحة التواصل مع العميد لرئيس الشعبة ---
        public IActionResult contact_dean()
        {
            return View();
        }

        // --- دالة إرسال الإشعار من رئيس الشعبة إلى العميد ---
        [HttpPost]
        public async Task<IActionResult> SendNotificationToDean(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return Json(new { success = false, message = "نص الرسالة فارغ." });
            }

            // البحث عن حساب العميد في قاعدة البيانات
            var deanUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Role.ToLower().Trim() == "dean");

            if (deanUser == null)
            {
                return Json(new { success = false, message = "لم يتم العثور على حساب العميد." });
            }

            var notification = new Notification
            {
                UserId = deanUser.Id,
                Message = $"تم إرسال رسالة من رئيس الشعبة: {message}",
                CreatedAt = DateTime.Now
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "تم إرسال الإشعار بنجاح!" });
        }

        // --- دالة تغيير حالة الطلب العامة (للاستخدامات الأخرى) ---
        [HttpPost]
        public async Task<IActionResult> UpdateApplicationStatus(int id, string status)
        {
            var application = await _context.HousingApplications.FindAsync(id);
            if (application != null)
            {
                application.Status = status;
                _context.HousingApplications.Update(application);

                var notification = new Notification
                {
                    UserId = application.UserId,
                    Message = $"تم تحديث حالة طلب السكن الخاص بك للفصل ({application.Semester}) للعام الدراسي ({application.AcademicYear}) إلى: ({status}).",
                    CreatedAt = DateTime.Now
                };
                _context.Notifications.Add(notification);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("head_home_page");
        }

        // --- صفحة سجل الطلبات (الهستوري) للطالبة ---
        public async Task<IActionResult> student_history(int? userId, string? name)
        {
            int targetUserId = userId ?? HttpContext.Session.GetInt32("UserId") ?? 0;

            var studentUser = await _context.Users.FindAsync(targetUserId);
            ViewBag.StudentName = studentUser?.Name ?? name ?? "الطالبة";
            ViewBag.TargetUserId = targetUserId;

            if (targetUserId != 0)
            {
                var historyList = await _context.HousingApplications
                    .Where(h => h.UserId == targetUserId && h.Status != "قيد التعبئة")
                    .OrderByDescending(h => h.Id)
                    .ToListAsync();

                return View(historyList);
            }

            return View(new List<HousingApplication>());
        }

        // --- صفحة عرض تفاصيل طلب السكن بناءً على معرف الطلب (id) ---
        public async Task<IActionResult> view_student_request(int id)
        {
            var application = await _context.HousingApplications
                .FirstOrDefaultAsync(h => h.Id == id);

            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // --- صفحة إشعارات الطالبة ---
        public async Task<IActionResult> student_notifications()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId.Value)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return View(notifications);
        }

        // --- صفحة إشعارات رئيس الشعبة ---
        public async Task<IActionResult> head_notifications()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId.Value)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return View(notifications);
        }

        // --- صفحة إشعارات العميد (المحدثة لجلب البيانات الحقيقية) ---
        public async Task<IActionResult> dean_notifications()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId.Value)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return View(notifications);
        }

        public IActionResult success_submission()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Name == username && x.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserRole", user.Role);
                HttpContext.Session.SetString("UserName", user.Name);
                HttpContext.Session.SetInt32("UserId", user.Id);

                if (user.Role == "student" || user.Role == "Student")
                {
                    return RedirectToAction("student_home_page");
                }
                else if (user.Role == "dean" || user.Role == "Dean")
                {
                    return RedirectToAction("dean_home_page");
                }
                else if (user.Role == "head" || user.Role == "Head")
                {
                    return RedirectToAction("head_home_page");
                }
            }

            ViewBag.Error = "اسم المستخدم أو كلمة المرور غير صحيحة!";
            return View();
        }

        // --- دوال مساعدة لتحديد الفصل والسنة الدراسية تلقائياً ---
        private string GetCurrentSemester()
        {
            int month = DateTime.Now.Month;
            if (month >= 9 || month == 1)
            {
                return "الأول";
            }
            else if (month >= 2 && month <= 6)
            {
                return "الثاني";
            }
            else
            {
                return "الصيفي";
            }
        }

        private string GetCurrentAcademicYear()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            if (month >= 9)
            {
                return $"{year}/{year + 1}";
            }
            else
            {
                return $"{year - 1}/{year}";
            }
        }

        // --- نموذج طلب السكن المكون من 8 خطوات ---

        [HttpGet]
        public IActionResult housing_form(bool isNew = true)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            string currentSemester = GetCurrentSemester();
            string currentAcademicYear = GetCurrentAcademicYear();

            var currentTermApp = _context.HousingApplications
                .Where(h => h.UserId == userId.Value && h.AcademicYear == currentAcademicYear && h.Semester == currentSemester)
                .OrderByDescending(h => h.Id)
                .FirstOrDefault();

            if (currentTermApp != null)
            {
                if (currentTermApp.Status == "مقبول")
                {
                    TempData["ErrorMessage"] = $"عزيزتي الطالبة، لقد تم قبول طلب السكن الخاص بك للفصل ({currentSemester}) للعام الدراسي ({currentAcademicYear}) بنجاح، ولا يمكنك تقديم طلب جديد لهذا الفصل.";
                    return RedirectToAction("student_home_page");
                }
                else if (currentTermApp.Status == "مقبول مبدئياً")
                {
                    TempData["ErrorMessage"] = "عزيزتي الطالبة، طلبك قيد التنفيذ، ولا يمكنك تقديم طلب جديد حالياً.";
                    return RedirectToAction("student_home_page");
                }
                else if (currentTermApp.Status == "مرفوض" || currentTermApp.Status == "مرفوض من العميد")
                {
                    TempData["ErrorMessage"] = $"عزيزتي الطالبة، تم رفض طلب السكن الخاص بك للفصل ({currentSemester}) للعام الدراسي ({currentAcademicYear}).";
                    return RedirectToAction("student_home_page");
                }
            }

            ViewBag.IsNew = isNew;
            ViewBag.CurrentSemester = currentSemester;
            ViewBag.CurrentAcademicYear = currentAcademicYear;
            return View();
        }

        [HttpPost]
        public IActionResult housing_form(HousingApplication model)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var activeApp = _context.HousingApplications
                .FirstOrDefault(h => h.UserId == userId.Value &&
                                   (h.Status == "قيد التعبئة" || h.Status == "قيد الانتظار"));

            string currentSemester = GetCurrentSemester();
            string currentAcademicYear = GetCurrentAcademicYear();

            if (activeApp != null)
            {
                activeApp.UniversityId = model.UniversityId;
                activeApp.FullName = model.FullName;
                activeApp.Major = model.Major;
                activeApp.Gpa = model.Gpa;
                activeApp.TawjihiGpa = model.TawjihiGpa;
                activeApp.Semester = currentSemester;
                activeApp.AcademicYear = currentAcademicYear;
                activeApp.Status = "قيد التعبئة";

                _context.HousingApplications.Update(activeApp);
            }
            else
            {
                model.UserId = userId.Value;
                model.Semester = currentSemester;
                model.AcademicYear = currentAcademicYear;
                model.Status = "قيد التعبئة";
                model.SubmissionDate = DateTime.Now;
                _context.HousingApplications.Add(model);
            }

            _context.SaveChanges();
            return RedirectToAction("housing_form_step2");
        }

        private HousingApplication? GetActiveApplication(int userId)
        {
            return _context.HousingApplications
                .Where(h => h.UserId == userId && (h.Status == "قيد التعبئة" || h.Status == "قيد الانتظار"))
                .OrderByDescending(h => h.Id)
                .FirstOrDefault();
        }

        public IActionResult housing_form_step2(bool isNew = true)
        {
            ViewBag.IsNew = isNew;
            return View();
        }

        [HttpPost]
        public IActionResult SaveStep2(bool isNew, DateTime? enrollmentDate, string studentPhone, string parentPhone, string passportNumber, string maritalStatus, string religion, DateTime? birthDate, string nationality)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var application = GetActiveApplication(userId.Value);
            if (application == null) return RedirectToAction("housing_form");

            application.StudentPhone = studentPhone;
            application.ParentPhone = parentPhone;
            application.PassportNumber = passportNumber ?? "";
            application.MaritalStatus = maritalStatus;
            application.Religion = religion;
            application.BirthDate = birthDate;
            application.Nationality = nationality;

            if (!isNew && enrollmentDate.HasValue)
            {
                application.EnrollmentDate = enrollmentDate.Value;
            }

            _context.HousingApplications.Update(application);
            _context.SaveChanges();

            return RedirectToAction("housing_form_step3", new { isNew = isNew });
        }

        public IActionResult housing_form_step3(bool isNew = true)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            ViewBag.IsNew = isNew;
            ViewBag.CurrentSemester = GetCurrentSemester();
            ViewBag.CurrentAcademicYear = GetCurrentAcademicYear();

            var application = GetActiveApplication(userId.Value);
            return View(application);
        }

        [HttpPost]
        public IActionResult SaveStep3(bool isNew, string academicLevel, string academicYear, string housingName, string studyType, string college, string penalties, string pledgeStudentName, string pledgeParentName)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var application = GetActiveApplication(userId.Value);
            if (application == null) return RedirectToAction("housing_form");

            application.AcademicLevel = academicLevel;
            application.AcademicYear = GetCurrentAcademicYear();
            application.Semester = GetCurrentSemester();

            application.HousingName = housingName;
            application.StudyType = studyType;
            application.College = college;
            application.Penalties = penalties;
            application.PledgeStudentName = pledgeStudentName;
            application.PledgeParentName = pledgeParentName;

            _context.HousingApplications.Update(application);
            _context.SaveChanges();

            return RedirectToAction("housing_form_step4", new { isNew = isNew });
        }

        public IActionResult housing_form_step4(bool isNew = true)
        {
            ViewBag.IsNew = isNew;
            return View();
        }

        [HttpPost]
        public IActionResult SaveStep4(bool isNew, string governorate, string housingType, string city, string? buildingNumber, string? street, string? neighborhood)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var application = GetActiveApplication(userId.Value);
            if (application == null) return RedirectToAction("housing_form");

            application.Governorate = governorate;
            application.HousingType = housingType;
            application.City = city;
            application.BuildingNumber = buildingNumber ?? "";
            application.Street = street ?? "";
            application.Neighborhood = neighborhood ?? "";

            _context.HousingApplications.Update(application);
            _context.SaveChanges();

            return RedirectToAction("housing_form_step5", new { isNew = isNew });
        }

        public IActionResult housing_form_step5(bool isNew = true)
        {
            ViewBag.IsNew = isNew;
            return View();
        }

        [HttpPost]
        public IActionResult SaveStep5(bool isNew, string healthInsurance, string medicalCareNeeded, bool hasAllergy, string? allergyDetails, bool hasDisability, string? disabilityDetails, bool needsMedicine, string? medicineDetails)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var application = GetActiveApplication(userId.Value);
            if (application == null) return RedirectToAction("housing_form");

            application.HealthInsurance = healthInsurance;
            application.MedicalCareNeeded = medicalCareNeeded;
            application.HasAllergy = hasAllergy;
            application.AllergyDetails = allergyDetails ?? "";
            application.HasDisability = hasDisability;
            application.DisabilityDetails = disabilityDetails ?? "";
            application.NeedsMedicine = needsMedicine;
            application.MedicineDetails = medicineDetails ?? "";

            _context.HousingApplications.Update(application);
            _context.SaveChanges();

            return RedirectToAction("housing_form_step6", new { isNew = isNew });
        }

        public IActionResult housing_form_step6(bool isNew = true)
        {
            ViewBag.IsNew = isNew;
            return View();
        }

        [HttpPost]
        public IActionResult SaveStep6(bool isNew, bool hasHypertension, bool hasPsychologicalDisorder, bool hasHeartDisease, bool hasEpilepsy, bool hasDiabetes, bool hasAsthma, bool hasCancer)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var application = GetActiveApplication(userId.Value);
            if (application == null) return RedirectToAction("housing_form");

            application.HasHypertension = hasHypertension;
            application.HasPsychologicalDisorder = hasPsychologicalDisorder;
            application.HasHeartDisease = hasHeartDisease;
            application.HasEpilepsy = hasEpilepsy;
            application.HasDiabetes = hasDiabetes;
            application.HasAsthma = hasAsthma;
            application.HasCancer = hasCancer;

            _context.HousingApplications.Update(application);
            _context.SaveChanges();

            return RedirectToAction("housing_form_step7", new { isNew = isNew });
        }

        public IActionResult housing_form_step7(bool isNew = true)
        {
            ViewBag.IsNew = isNew;
            return View();
        }

        [HttpPost]
        public IActionResult SaveStep7(bool isNew, bool allowWeekendLeave, bool allowDayTrips, bool allowMultiDayTrips, bool allowTravelAbroad)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var application = GetActiveApplication(userId.Value);
            if (application == null) return RedirectToAction("housing_form");

            application.AllowWeekendLeave = allowWeekendLeave;
            application.AllowDayTrips = allowDayTrips;
            application.AllowMultiDayTrips = allowMultiDayTrips;
            application.AllowTravelAbroad = allowTravelAbroad;

            _context.HousingApplications.Update(application);
            _context.SaveChanges();

            return RedirectToAction("housing_form_step8", new { isNew = isNew });
        }

        public IActionResult housing_form_step8(bool isNew = true)
        {
            ViewBag.IsNew = isNew;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveStep8(bool isNew, IFormFile healthCertificate, IFormFile familyBook, IFormFile? powerOfAttorney, IFormFile guardianId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var application = GetActiveApplication(userId.Value);
            if (application == null) return RedirectToAction("housing_form");

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            async Task<string> SaveFileAsync(IFormFile file, string? oldFilePath)
            {
                if (file == null || file.Length == 0) return oldFilePath ?? "";

                if (!string.IsNullOrEmpty(oldFilePath))
                {
                    string oldFullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", oldFilePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldFullPath))
                    {
                        System.IO.File.Exists(oldFullPath);
                    }
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return "/uploads/" + uniqueFileName;
            }

            application.HealthCertificatePath = await SaveFileAsync(healthCertificate, application.HealthCertificatePath);
            application.FamilyBookPath = await SaveFileAsync(familyBook, application.FamilyBookPath);
            if (powerOfAttorney != null && powerOfAttorney.Length > 0)
            {
                application.PowerOfAttorneyPath = await SaveFileAsync(powerOfAttorney, application.PowerOfAttorneyPath);
            }
            application.GuardianIdPath = await SaveFileAsync(guardianId, application.GuardianIdPath);

            application.Status = "قيد الانتظار";
            _context.HousingApplications.Update(application);
            await _context.SaveChangesAsync();

            return RedirectToAction("success_submission");
        }
    }
}