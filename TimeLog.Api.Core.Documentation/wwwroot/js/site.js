﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var mobileNavButton = document.getElementById("toggle-mobile-nav");

if (mobileNavButton) {
  mobileNavButton.addEventListener("click", function () {
    document.body.classList.toggle("menu-toggled");
  });
}
