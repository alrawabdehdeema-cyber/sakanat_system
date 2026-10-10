using System.Diagnostics;

using JREST.Data;

using JREST.Models;

using Microsoft.AspNetCore.Mvc;



namespace JREST.Controllers

{

    public class HomeController : Controller

    {

        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context; // متغير قاعدة البيانات




        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)

        {

            _logger = logger;

            _context = context;

        }



        // الصفحة الرئيسية الافتراضية

        public IActionResult Index()

        {

            return View("Login");

        }



        // صفحة الطالب الرئيسية

        public IActionResult student_home_page()

        {

            return View();

        }



        // صفحة العميد الرئيسية (بدلاً من المشرف)

        public IActionResult dean_home_page()

        {

            return View();

        }



        // صفحة رئيس الشعبة الرئيسية

        public IActionResult head_home_page()

        {

            return View();

        }



        // صفحة اتصل بنا

        public IActionResult contact_us()

        {

            return View();

        }



        // صفحة سياسة الخصوصية

        public IActionResult privacy_policy()

        {

            return View();

        }



        // صفحة الأسعار

        public IActionResult prices()

        {

            return View();

        }



        // صفحة تأكيد قبول الطلب

        public IActionResult confirm_acceptance()

        {

            return View();

        }



        // صفحة تأكيد رفض الطلب

        public IActionResult confirm_rejection()

        {

            return View();

        }



        // صفحة سبب رفض الطلب

        public IActionResult rejection_reason()

        {

            return View();

        }



        // صفحة التواصل مع الطالبة

        public IActionResult contact_student()

        {

            return View();

        }



        // صفحة معلومات الطالبة (اختيار العام والفصل)

        public IActionResult student_history(string name)

        {

            ViewBag.StudentName = name ?? "ساره مهند";

            return View();

        }



        // صفحة إشعارات الطالبة

        public IActionResult student_notifications()

        {

            return View();

        }



        // صفحة إشعارات رئيس الشعبة

        public IActionResult head_notifications()

        {

            return View();

        }



        // صفحة إشعارات العميد الجديدة

        public IActionResult dean_notifications()

        {

            return View();

        }



        // صفحة عرض طلب السكن المعبئ لرئيس الشعبة أو العميد

        public IActionResult view_student_request()

        {

            return View();

        }



        // 1. عرض صفحة تسجيل الدخول (GET)

        [HttpGet]

        public IActionResult Login()

        {

            return View();

        }



        // 2. التحقق من بيانات الدخول باستخدام اسم المستخدم عبر قاعدة البيانات (POST)

        [HttpPost]

        public IActionResult Login(string username, string password)

        {

            // البحث عن المستخدم في قاعدة البيانات مطابقة لاسم المستخدم وكلمة المرور

            var user = _context.Users

                .FirstOrDefault(x => x.Name == username && x.Password == password);



            if (user != null)

            {

                // حفظ بيانات المستخدم ودوره في الجلسة (Session)

                HttpContext.Session.SetString("UserRole", user.Role);

                HttpContext.Session.SetString("UserName", user.Name);

                HttpContext.Session.SetInt32("UserId", user.Id);



                // التوجيه بناءً على الدور المخزن في قاعدة البيانات

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



            // في حال كانت البيانات خاطئة أو غير موجودة في الداتا بيس

            ViewBag.Error = "اسم المستخدم أو كلمة المرور غير صحيحة!";

            return View();

        }



        // الخطوة الأولى

        public IActionResult housing_form(bool isNew = true)

        {

            ViewBag.IsNew = isNew;

            return View();

        }



        // الخطوة الثانية

        public IActionResult housing_form_step2(bool isNew = true)

        {

            ViewBag.IsNew = isNew;

            return View();

        }



        // الخطوة الثالثة

        public IActionResult housing_form_step3(bool isNew = true)

        {

            ViewBag.IsNew = isNew;

            return View();

        }



        // الخطوة الرابعة

        public IActionResult housing_form_step4(bool isNew = true)

        {

            ViewBag.IsNew = isNew;

            return View();

        }



        // الخطوة الخامسة (المعلومات الصحية)

        public IActionResult housing_form_step5(bool isNew = true)

        {

            ViewBag.IsNew = isNew;

            return View();

        }



        // الخطوة السادسة

        public IActionResult housing_form_step6(bool isNew = true)

        {

            ViewBag.IsNew = isNew;

            return View();

        }



        // الخطوة السابعة

        public IActionResult housing_form_step7(bool isNew = true)

        {

            ViewBag.IsNew = isNew;

            return View();

        }



        // الخطوة الثامنة

        public IActionResult housing_form_step8(bool isNew = true)

        {

            ViewBag.IsNew = isNew;

            return View();

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()

        {

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        }

    }

}