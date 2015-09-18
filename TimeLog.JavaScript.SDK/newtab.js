var timelog;
if (typeof require !== 'undefined') {
    timelog = require('timelog');
}


var debug = localStorage.getItem('TimeLogDebug') || false;

// ReSharper disable once NativeTypePrototypeExtending
Number.prototype.pad = function (size) {
    var s = String(this);
    while (s.length < (size || 2)) { s = "0" + s; }
    return s;
}

$(document).ready(function () {

    // General
    $('input[class*="pauseTrigger"]').focus(function () {
        pauseUpdates();
    });

    $('input[class*="pauseTrigger"]').blur(function () {
        startUpdates();
    });

    $('#version-text').click(function () { $('.versions').toggle(); });

    // Debug
    if (debug) {
        timelog.debug = true;
        $('.debug').show();
    }

    // General
    mainloop();
    setInterval(mainloop, 1000);
    startUpdates();

    // Get the recent work units on every tab open
    //var now = new Date();
    //var startDate = new Date(now.getFullYear(), now.getMonth(), now.getDate(), 0, 0, 0);
    //var endDate = new Date(startDate.getTime() + (1 * 24 * 60 * 60 * 1000));
});

/* LOCAL STORAGE HANDLING
********************************************************/
var localStorageView = 'TimelogView';
var localStoragePaused = 'TimelogPaused';
var localStorageLoading = 'TimelogLoading';
var localStorageCacheExpire = 'TimelogCacheExpire';

/* VIEWS
********************************************************/
var viewTask = 'taskView';
var viewTracking = 'trackingView';
var viewWorkUnit = 'workunitView';
var viewEmpty = 'empty';

function setView(viewName) {
    localStorage.setItem(localStorageView, viewName);
    mainloop();
}

/* MAIN LOOP... Keeps multiple tabs in sync
********************************************************/
function pauseUpdates() {
    localStorage.setItem(localStoragePaused, 'true');
}

function startUpdates() {
    localStorage.setItem(localStoragePaused, 'false');
}

function isPaused() {
    return localStorage.getItem(localStoragePaused) === 'true';
}

function startLoading() {
    localStorage.setItem(localStorageLoading, 'true');
}

function stopLoading() {
    localStorage.setItem(localStorageLoading, 'false');
}

function isLoading() {
    return localStorage.getItem(localStorageLoading) === 'true';
}

function mainloop() {

    adjustClock();

    $('.taskview').hide();
    $('.trackingview').hide();
    $('.workunitview').hide();

    if (isLoading()) {
        $('#loading-status').show();
    } else {
        $('#loading-status').hide();
    }

    switch (localStorage.getItem(localStorageView)) {

        case viewTask: {

            break;
        }
        case viewTracking: {

            break;
        }
        default: {

        }
    }
}

/* GENERAL
********************************************************/

function signOut() {
    timelog.signOut();

    localStorage.removeItem(localStoragePaused);
    localStorage.removeItem(localStorageView);
    localStorage.removeItem(localStorageCacheExpire);

    setView(viewEmpty);
}

function adjustClock() {
    var date = new Date();

    var spacer = date.getSeconds() % 2 ? ' ' : ':';
    var ampm = date.getHours() >= 12 ? 'PM' : 'AM';
    var hours = date.getHours() % 12 !== 0 ? (date.getHours() > 12 ? date.getHours() - 12 : date.getHours()) : 12;
    var clock = hours + spacer + formatClock(date.getMinutes()) + ' ' + ampm; // US format
    //var clock = date.toLocaleTimeString(); // local machine - reg ex: ([\d]+:[\d]+):[\d]+ (AM|PM)

    $('.clock').html(clock);
    $('.calendar').html(date.toDateString());
}

function formatClock(number) {
    return number.toString().length > 1 ? number : "0" + number;
}