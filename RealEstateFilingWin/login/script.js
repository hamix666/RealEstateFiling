//const container = document.querySelector('.container');
//const loader = document.getElementById('loader');
//const LoginLink = document.querySelector('.SignInLink');
//const RegisterLink = document.querySelector('.SignUpLink');

//// بعد از لود شدن صفحه، نمایش فرم
//window.addEventListener('DOMContentLoaded', () => {
//    loader.style.display = 'none';
//    container.style.display = 'flex';
//});

//// سوئیچ بین ورود و ثبت‌نام
//RegisterLink.addEventListener('click', () => {
//    container.classList.add('active');
//});
//LoginLink.addEventListener('click', () => {
//    container.classList.remove('active');
//});

//// 📌 ارسال فرم ورود
//const loginForm = document.getElementById('loginForm');
//loginForm.addEventListener('submit', (e) => {
//    e.preventDefault();
//    const username = document.getElementById('username').value.trim();
//    const password = document.getElementById('password').value.trim();

//    // ارسال JSON به C#
//    window.chrome.webview.postMessage(JSON.stringify({
//        action: "login",
//        username,
//        password
//    }));
//});

//// 📌 ارسال فرم ثبت‌نام
//const registerForm = document.getElementById('registerForm');
//registerForm.addEventListener('submit', (e) => {
//    e.preventDefault();
//    const username = document.getElementById('reg_username').value.trim();
//    const password = document.getElementById('reg_password').value.trim();
//    const mobile = document.getElementById('reg_mobile').value.trim();
//    const name = document.getElementById('reg_name').value.trim();

//    // ارسال JSON به C#
//    window.chrome.webview.postMessage(JSON.stringify({
//        action: "register",
//        username,
//        password,mobile,name
//    }));
//});

//// 📌 دریافت پاسخ از C#
//window.chrome.webview.addEventListener('message', event => {
//    if (event.data === "login_success") {
//        Swal.fire('✅ ورود موفق', 'به برنامه خوش آمدید!', 'success');
//    }
//    else if (event.data === "login_error") {
//        Swal.fire('❌ خطا', 'نام کاربری یا رمز عبور اشتباه است!', 'error');
//    }
//    else if (event.data === "register_success") {
//        Swal.fire('🎉 ثبت نام موفق', 'اکنون می‌توانید وارد شوید.', 'success');
//        container.classList.remove('active'); // برگرد به فرم ورود
//    }
//    else if (event.data === "register_exists") {
//        Swal.fire('⚠️ تکراری', 'نام کاربری از قبل وجود دارد!', 'warning');
//    }
//    else if (event.data === "register_error") {
//        Swal.fire('❌ خطا', 'ثبت‌نام با خطا مواجه شد.', 'error');
//    }
//});



var container = document.querySelector('.container');
var loader = document.getElementById('loader');
var loginLink = document.querySelector('.SignInLink');
var registerLink = document.querySelector('.SignUpLink');
var loginForm = document.getElementById('loginForm');
var registerForm = document.getElementById('registerForm');

window.addEventListener('DOMContentLoaded', function () {
    loader.style.display = 'none';
    container.style.display = 'flex';
});

// تغییر حالت بین فرم ورود و ثبت نام
if (registerLink) {
    registerLink.addEventListener('click', function (e) {
        e.preventDefault();
        container.classList.add('active');
    });
}
if (loginLink) {
    loginLink.addEventListener('click', function (e) {
        e.preventDefault();
        container.classList.remove('active');
    });
}

// ارسال فرم ورود
if (loginForm) {
    loginForm.addEventListener('submit', function (e) {
        e.preventDefault();
        var username = document.getElementById('username').value || "";
        var password = document.getElementById('password').value || "";
        var data = JSON.stringify({ action: "login", username: username, password: password });

        if (window.chrome && window.chrome.webview) {
            try {
                window.chrome.webview.postMessage(data);
            } catch (err) {
                alert("خطا در ارسال داده‌ها به برنامه.");
            }
        } else {
            alert("ارتباط با WebView برقرار نیست.");
        }
    });
}

// ارسال فرم ثبت‌نام
if (registerForm) {
    registerForm.addEventListener('submit', function (e) {
        e.preventDefault();
        var username = document.getElementById('reg_username').value || "";
        var password = document.getElementById('reg_password').value || "";
        var name = document.getElementById('reg_name').value || "";
        var mobile = document.getElementById('reg_mobile').value || "";
        var data = JSON.stringify({ action: "register", username: username, password: password,name:name,mobile:mobile });

        if (window.chrome && window.chrome.webview) {
            try {
                window.chrome.webview.postMessage(data);
            } catch (err) {
                alert("خطا در ارسال داده‌ها به برنامه.");
            }
        } else {
            alert("ارتباط با WebView برقرار نیست.");
        }
    });
}

// دریافت پاسخ از برنامه
if (window.chrome && window.chrome.webview) {
    window.chrome.webview.addEventListener('message', function (event) {
        try {
            var msg = event.data;
            if (typeof msg === "string") {
                if (msg === "success") {
                    alert("ورود موفق!");
                } else if (msg === "error") {
                    alert("نام کاربری یا رمز عبور اشتباه است!");
                } else if (msg === "registered") {
                    alert("ثبت‌نام با موفقیت انجام شد!");
                    container.classList.remove('active');
                } else if (msg === "userexists") {
                    alert("این نام کاربری قبلاً وجود دارد!");
                }
            }
        } catch (ex) {
            console.log("خطا در دریافت پیام:", ex);
        }
    });
}

