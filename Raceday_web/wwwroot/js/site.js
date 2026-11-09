// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll("input[type=file]").forEach(input => {
        input.addEventListener("change", () => {
            const file = input.files && input.files[0];
            if (file && file.size > 2 * 1024 * 1024) {
                alert("Please choose an image smaller than 2MB.");
                input.value = "";
            }
        });
    });
});