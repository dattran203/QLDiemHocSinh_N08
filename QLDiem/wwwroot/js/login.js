// animations

const loginText = document.querySelector(".title-text .login");
const loginForm = document.querySelector("form.login");
const loginBtn = document.querySelector("label.login");
/*const signupBtn = document.querySelector("label.signup");
const signupLink = document.querySelector("form .signup-link a");
signupBtn.onclick = (() => {
    loginForm.style.marginLeft = "-50%";
    loginText.style.marginLeft = "-50%";
});*/
loginBtn.onclick = (() => {
    loginForm.style.marginLeft = "0%";
    loginText.style.marginLeft = "0%";
});
/*signupLink.onclick = (() => {
    signupBtn.click();
    return false;
});*/


// quan li thong tin

const data = [
    { username: 'nhom9@gmail.com', password: '123456789' },
]

function submit_Login(e) {
    event.preventDefault();
    const user = document.getElementById('username-login').value;
    const pass = document.getElementById('password-login').value;

    for (let i = 0; i < data.length; i++) {
        if (user != data[i].username) continue;
        if (user == data[i].username && pass != data[i].password) {
            alert('Mật khẩu sai.');
            return;
        }
        if (user == data[i].username && pass == data[i].password) {
            return RedirectToAction("Index", "Home");        }
    }
    alert('Tên đăng nhập sai.');
}

function submit_Signup() {
    const user = document.getElementById('username-signup').value;
    const pass = document.getElementById('password-signup').value;
    const pass_cf = document.getElementById('confirm-password').value;

    if (user.length == 0) {
        alert('Bạn chưa nhập tên đăng nhập.');
        retrun;
    }

    if (pass.length < 8) {
        alert('Mật khẩu phải trên 8 ký tự. Hãy thử lại.');
        return;
    }
    else if (pass != pass_cf) {
        alert('Các mật khẩu đã nhập không khớp. Hãy thử lại.');
        return;
    }
    else {
        for (let i = 0; i < data.length; i++)
            if (user == data[i].username) {
                alert('Tên đăng nhập đã được sử dụng. Hãy thử lại.');
                return;
            }
        data.push({ username: user, password: pass });
        loginBtn.click();
        alert("Đăng kí thành công.");
    }
}