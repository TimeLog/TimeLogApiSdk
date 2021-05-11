// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var mobileNavButton = document.getElementById("toggle-mobile-nav");

if (mobileNavButton) {
  mobileNavButton.addEventListener("click", function () {
    document.body.classList.toggle("menu-toggled");
  });
}

// Handle the toggling of sub menus
var mq = window.matchMedia("(max-width: 799px)");
var topLevelLinks = Array.from(
  document.getElementsByClassName("top-level__link")
);

function clickHandler(eventObj) {
  eventObj.preventDefault();
  // This will not work in IE11
  var parent = eventObj.target.closest(".top-level__item");
  parent.classList.toggle("active");
}

if (mq.matches && topLevelLinks) {
  topLevelLinks.forEach(function (item) {
    item.addEventListener("click", clickHandler);
  });
}

// Setup a timer
var timeout;

// Listen for resize events
window.addEventListener(
  "resize",
  function (event) {
    // If there's a timer, cancel it
    if (timeout) {
      window.cancelAnimationFrame(timeout);
    }

    // Setup the new requestAnimationFrame()
    timeout = window.requestAnimationFrame(function () {
      if (mq.matches && topLevelLinks) {
        topLevelLinks.forEach(function (item) {
          item.addEventListener("click", clickHandler);
        });
      } else {
        topLevelLinks.forEach(function (item) {
          item.removeEventListener("click", clickHandler);
        });
      }
    });
  },
  false
);
