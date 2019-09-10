var passwordFielsds = document.querySelectorAll(".password");
passwordFielsds.forEach(passwordField => {
    var input = passwordField.querySelector(".password__input");
    var icon = passwordField.querySelector(".password__icon");
    icon.addEventListener("click", function () {
        if (icon.innerHTML == "visibility") {
            icon.innerHTML = "visibility_off";
            input.type = "text";
        }
        else {
            icon.innerHTML = "visibility";
            input.type = "password";        
        }
    }, false)
});