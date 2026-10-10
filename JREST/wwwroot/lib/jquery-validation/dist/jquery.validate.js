@{
    ViewData["Title"] = "طلب إلحاق الطالب بالسكن";
    Layout = "_HeaderAndFooter";
}

<div class="container my-5">

    <!-- عنوان الصفحة -->
    <h3 class="text-custom-blue fw-bold mb-4 text-end">طلب إلحاق الطالب بالسكن</h3>

    <!-- شريط الخطوات (Stepper) مرتب بشكل صحيح من اليمين (1) إلى اليسار (8) -->
    <div class="stepper-container mb-5">
        <div class="d-flex align-items-center gap-2">
            <!-- الخطوة الأولى النشطة (يمين) - مربوطة بالصفحة الأولى -->
            <a href="@Url.Action(" housing_form", "Home")" class="step-circle active text-decoration-none">1</a>
        <div class="step-line"></div>

        <!-- الخطوة 2 (مربوطة بالصفحة الثانية لتصبح قابلة للنقر) -->
        <a href="@Url.Action(" housing_form_step2", "Home")" class="step-circle text-decoration-none">2</a>
    <div class="step-line"></div>

    <div class="step-circle">3</div>
    <div class="step-line"></div>

    <div class="step-circle">4</div>
    <div class="step-line"></div>

    <div class="step-circle">5</div>
    <div class="step-line"></div>

    <div class="step-circle">6</div>
    <div class="step-line"></div>

    <div class="step-circle">7</div>
    <div class="step-line"></div>

    <!-- الخطوة 8 (يسار) -->
    <div class="step-circle">8</div>
</div>
    </div >

    < !--نموذج إدخال البيانات-- >
    <div class="row justify-content-center">
        <div class="col-md-8 col-lg-6">
            <form>
                <!-- الرقم الجامعي (أرقام فقط) -->
                <div class="mb-3 row align-items-center">
                    <div class="col-sm-4 text-sm-end">
                        <label class="form-label fw-semibold">الرقم الجامعي</label>
                    </div>
                    <div class="col-sm-8">
                        <input type="number" class="form-control custom-input" placeholder="أدخل الرقم الجامعي" />
                    </div>
                </div>

                <!-- الاسم من اربعة مقاطع -->
                <div class="mb-3 row align-items-center">
                    <div class="col-sm-4 text-sm-end">
                        <label class="form-label fw-semibold">الاسم من اربعة مقاطع</label>
                    </div>
                    <div class="col-sm-8">
                        <input type="text" class="form-control custom-input" placeholder="الاسم الرباعي" />
                    </div>
                </div>

                <!-- التخصص الجامعي -->
                <div class="mb-3 row align-items-center">
                    <div class="col-sm-4 text-sm-end">
                        <label class="form-label fw-semibold">التخصص الجامعي</label>
                    </div>
                    <div class="col-sm-8">
                        <input type="text" class="form-control custom-input" placeholder="التخصص" />
                    </div>
                </div>

                <!-- المعدل التراكمي (أرقام مع دعم الكسور) -->
                <div class="mb-3 row align-items-center">
                    <div class="col-sm-4 text-sm-end">
                        <label class="form-label fw-semibold">المعدل التراكمي</label>
                    </div>
                    <div class="col-sm-8">
                        <input type="number" step="0.01" min="0" max="100" class="form-control custom-input" placeholder="مثال: 85.5" />
                    </div>
                </div>

                <!-- معدل الثانوية العامة (أرقام مع دعم الكسور) -->
                <div class="mb-4 row align-items-center">
                    <div class="col-sm-4 text-sm-end">
                        <label class="form-label fw-semibold">معدل الثانوية العامة</label>
                    </div>
                    <div class="col-sm-8">
                        <input type="number" step="0.01" min="0" max="100" class="form-control custom-input" placeholder="مثال: 90.2" />
                    </div>
                </div>

                <!-- زر التالي (مربوط بالصفحة الثانية) -->
                <div class="d-grid col-sm-8 ms-auto">
                    <a href="@Url.Action(" housing_form_step2", "Home")" class="btn btn-next text-white fw-semibold shadow-sm text-center text-decoration-none">التالي</a>
        </div>
    </form>
        </div >
    </div >

</div >