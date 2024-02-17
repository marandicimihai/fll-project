// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let stars = document.querySelectorAll(".ratings span");
let products = document.querySelectorAll(".ratings");

for (let star of stars) {
    star.addEventListener("click", function () {

        let children = star.parentElement.children;
        for (let child of children) {
            if (child.getAttribute("data-clicked")) {
                child.removeAttribute("data-clicked");
            }
        }

        this.setAttribute("data-clicked", "true");
        let rating = this.dataset.rating;
        let exhibitId = this.parentElement.dataset.exhibitid;

        console.log(exhibitId + " " + rating);
    });
}