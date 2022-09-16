function confirmPassword() {
    var password = document.getElementById("password-input").value
    var confirmPassword = document.getElementById("confirm-password-input").value

    while (password != confirmPassword) {
        alert("Passwords do not match.")
        confirmPassword = ""
        return false;
    }
    $('#form').submit();
    return true;
}
