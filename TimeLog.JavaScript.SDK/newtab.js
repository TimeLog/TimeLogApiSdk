/// <reference path="timelog.js" />

var debug = localStorage.getItem('TimeLogDebug') || false;
var regexDuration = /P((([0-9]*\.?[0-9]*)Y)?(([0-9]*\.?[0-9]*)M)?(([0-9]*\.?[0-9]*)W)?(([0-9]*\.?[0-9]*)D)?)?(T(([0-9]*\.?[0-9]*)H)?(([0-9]*\.?[0-9]*)M)?(([0-9]*\.?[0-9]*)S)?)?/

Number.prototype.pad = function (size) {
    var s = String(this);
    while (s.length < (size || 2)) { s = "0" + s; }
    return s;
}

$(document).ready(function() {

    // timelog setup
    timelog.authenticateSuccessCallback = userviewGetTokenSuccess;
    timelog.authenticateFailureCallback = userviewGetTokenError;

    timelog.getEmployeeWorkSuccessCallback = taskviewGetEmployeeWorkSuccess;
    timelog.getEmployeeWorkFailureCallback = taskviewGetEmployeeWorkFailure;

    // General
    $('input[class*="pauseTrigger"]').focus(function () {
        pauseUpdates();
    });

    $('input[class*="pauseTrigger"]').blur(function () {
        startUpdates();
    });

	// User View
	$('#userview-signout').click(signOut);
	$('#userview-submit').click(userviewSubmitClick);
	$('#userview-username').keyup(userviewKeyUp);
	$('#userview-password').keyup(userviewKeyUp);
	$('#userview-url').val(timelog.url);
	$('#userview-username').val(timelog.username);

	// Work Unit View
	// $('#workunitview-back').click(showTrackingView);

	// Task View
	$("#taskview-search").keyup(taskviewSearchKeyUp);
	$("#taskview-search").keydown(taskviewSearchKeyDown);
	$('#taskview-reloadworkunits').click(taskviewReloadWorkunitsClick);
	$('#taskview-gototimelog').attr('href', timelog.url);

    // Tracking View
	$('#trackingview-time').focus(trackingviewTimeFocus);
	$('#trackingview-time').keyup(trackingviewTimeKeyUp);
	$('#trackingview-time').blur(trackingviewTimeBlur);
	$('#trackingview-comment').focus(trackingviewTimeFocus);
	$('#trackingview-comment').keyup(trackingviewTimeKeyUp);
	$('#trackingview-comment').blur(trackingviewTimeBlur);
	$('#trackingview-cancel').click(trackingviewCancelClick);
	$('#trackingview-done').click(trackingviewDoneClick);

	// Debug
	if (debug) {
	    timelog.debug = true;
		$('.debug').show();
		$('#debug-taskview').click(function () { localStorage.setItem(localStorageView, 'taskview') });
		$('#debug-trackingview').click(function () { localStorage.setItem(localStorageView, 'trackingview') });
		$('#debug-workunitview').click(function () { localStorage.setItem(localStorageView, 'workunitview') });
	}

	if (timelog.isTokenValid()) {

	    localStorage.setItem(localStorageSignedIn, true);
	    localStorage.setItem(localStorageView, viewTask);

	    if (localStorage.getItem(localStorageTaskID) != undefined) {
	        localStorage.setItem(localStorageView, viewTracking);
	    }

	}

    // General
	mainloop();
	setInterval(mainloop, 1000);

});

/* LOCAL STORAGE HANDLING
********************************************************/
var localStorageTaskID = 'TimelogTaskID';
var localStorageTaskStart = 'TimelogTaskStart';
var localStorageTaskName = 'TimelogTaskName';
var localStorageTaskComment = 'TimelogTaskComment';
var localStorageView = 'TimelogView';
var localStorageSignedIn = 'TimelogSignedIn';
var localStoragePaused = 'TimelogPaused';
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
    return localStorage.getItem(localStoragePaused) == 'true';
}

function mainloop() {

    adjustClock();

    $('.taskview').hide();
    $('.trackingview').hide();
    $('.workunitview').hide();

    $('#userview-notloggedin').show();
    $('#userview-loggedin').hide();
    $('#userview-logindialog').show();

    if (localStorage.getItem(localStorageSignedIn) === "true") {
        $('#userview-notloggedin').hide();
        $('#userview-loggedin').show();
        $('#userview-logindialog').hide();

        switch (localStorage.getItem(localStorageView)) {

            case viewTask: {
                $('.taskview').show();

                if (!isPaused()) {
                    var now = new Date();
                    var startDate = new Date(now.getFullYear(), now.getMonth(), now.getDate(), 0, 0, 0);
                    var endDate = new Date(startDate.getTime() + (1 * 24 * 60 * 60 * 1000));
                    timelog.getEmployeeWork(startDate, endDate);
                }

                break;
            }
            case viewTracking: {
                $('.trackingview').show();
                $('#taskview-search').focus();

                var taskid = localStorage.getItem(localStorageTaskID);
                var start = Date.parse(localStorage.getItem(localStorageTaskStart));
                var name = localStorage.getItem(localStorageTaskName);
                var comment = localStorage.getItem(localStorageTaskComment);

                if (taskid == undefined || taskid <= 0) {
                    localStorage.removeItem(localStorageTaskID);
                    localStorage.removeItem(localStorageTaskStart);
                    localStorage.removeItem(localStorageTaskName);
                    localStorage.removeItem(localStorageTaskComment);
                    setView(viewTask);
                }

                if (!isPaused()) {
                    var diffMiliseconds = Math.abs(new Date() - start); // in miliseconds
                    var diffSeconds = diffMiliseconds / 1000;
                    var diffMinutes = diffSeconds / 60;
                    var diffHours = diffMinutes / 60;

                    var calcMinutes = diffMinutes - (Math.floor(diffHours) * 60);
                    var spacer = new Date().getSeconds() % 2 ? 'rgba(0,0,0,0.06)' : 'rgba(0,0,0,0.04)';
                    var diffString = Math.floor(diffHours) + 'h' + (Math.floor(calcMinutes) > 9 ? '' : '0') + Math.floor(calcMinutes) + 'm';

                    $('#trackingview-name').html(name);
                    $('#trackingview-time').val(diffString);
                    $('#trackingview-comment').val(comment);
                    $('#trackingview-time').css('background-color', spacer);
                }

                break;
            }
            default: {

            }
        } 
    } else {
        // Not signed in

        if (!isPaused()) {
            if ($('#userview-url').val().length === 0) {
                $('#userview-url').focus();
            } else if ($('#userview-username').val().length === 0) {
                $('#userview-username').focus();
            } else if ($('#userview-password').val().length === 0) {
                $('#userview-password').focus();
            }
        }
    }
}

/* GENERAL
********************************************************/

function signOut() {
    timelog.signOut();
	
    localStorage.removeItem(localStorageTaskComment);
    localStorage.removeItem(localStorageTaskID);
    localStorage.removeItem(localStorageTaskName);
    localStorage.removeItem(localStorageTaskStart);
    localStorage.removeItem(localStoragePaused);
    localStorage.removeItem(localStorageView);
    localStorage.removeItem(localStorageSignedIn);
    localStorage.removeItem(localStorageCacheExpire);

    localStorage.setItem(localStorageSignedIn, false);
    setView(viewEmpty);
}

function adjustClock() {
	var date = new Date();

	var spacer = date.getSeconds() % 2 ? ' ' : ':';
	var ampm = date.getHours() >= 12 ? 'PM' : 'AM';
	var hours = date.getHours() % 12 != 0 ? (date.getHours() > 12 ? date.getHours() - 12 : date.getHours()) : 12;
	var clock = hours + spacer + formatClock(date.getMinutes()) + ' ' + ampm; // US format
	//var clock = date.toLocaleTimeString(); // local machine - reg ex: ([\d]+:[\d]+):[\d]+ (AM|PM)

	$('.clock').html(clock);
	$('.calendar').html(date.toDateString());
}

function formatClock(number) {
	return number.toString().length > 1 ? number : "0" + number;
}

/* USER VIEW
********************************************************/
function userviewSubmitClick() {
	$('#userview-loginstatus').hide();
	$('#userview-loginstatus').html('');

	var url = $('#userview-url').val();
	var user = $('#userview-username').val();
	var pw = $('#userview-password').val();

	if (user.length == 0 || pw.length == 0 || url.length == 0) {
		userviewSetLoginStatus('All fields are required');
		return;
	}

	timelog.tryAuthenticate(url, user, pw);
}

function userviewGetTokenSuccess() {
    localStorage.setItem(localStorageSignedIn, true);
    setView(viewTask);
}

function userviewSetLoginStatus(msg) {
	$('#userview-loginstatus').html(msg);
	$('#userview-loginstatus').attr('title', msg);
	$('#userview-loginstatus').show();
}

function userviewGetTokenError(errorMsg) {
    userviewSetLoginStatus('Ups! ' + errorMsg);
}

function userviewKeyUp(event) {
	$('#userview-loginstatus').hide();
	if (event.which == 13) { // Enter key
		userviewSubmitClick();
	}
}

/* WORK UNIT VIEW
********************************************************/

/* TRACKING VIEW
********************************************************/
function trackingviewCancelClick() {
    localStorage.removeItem(localStorageTaskID);
    localStorage.removeItem(localStorageTaskStart);
    localStorage.removeItem(localStorageTaskName);
    localStorage.removeItem(localStorageTaskComment);
    setView(viewTask);
}

function trackingviewDoneClick() {

    var taskID = localStorage.getItem(localStorageTaskID);
    var start = localStorage.getItem(localStorageTaskStart);
    var end = new Date();
    var comment = $('#trackingview-comment').val();
    trackingviewPaused = true;

    timelog.insertWorkSuccessCallback = trackingviewInsertWorkSuccess;
    timelog.insertWorkFailureCallback = trackingviewInsertWorkFailure;
    timelog.insertWork(taskID, start, end, comment);

}

function trackingviewInsertWorkSuccess() {
    localStorage.removeItem(localStorageTaskID);
    localStorage.removeItem(localStorageTaskStart);
    localStorage.removeItem(localStorageTaskName);
    localStorage.removeItem(localStorageTaskComment);
    timelog.invalidateGetEmployeeWork();
    setView(viewTask);
}

function trackingviewInsertWorkFailure(msg) {
    trackingviewPaused = false;
    alert(msg);
}

function trackingviewTimeFocus() {
    trackingviewPaused = true;
}

function trackingviewTimeBlur() {
    trackingviewSaveTime();
    trackingviewPaused = false;
}

function trackingviewTimeKeyUp(event) {
    if (event.keyCode == 13) {
        trackingviewSaveTime();
        $('#trackingview-time').blur();
        $('#trackingview-comment').blur();
    }
}

function trackingviewSaveTime() {
    var newValue = $('#trackingview-time').val().toString().toLowerCase();

    var indexH = newValue.indexOf('h');
    var indexM = newValue.indexOf('m');

    if (indexH > 0 && indexM > 0) {

        var hours = parseInt(newValue.substring(0, indexH));
        var minutes = parseInt(newValue.substring(indexH + 1, indexM));

        // Convert to miliseconds
        var milliseconds = (hours * 60 * 60 * 1000) + (minutes * 60 * 1000);

        var newStartDate = new Date();
        newStartDate.setTime(new Date().getTime() - milliseconds);

        localStorage.setItem(localStorageTaskStart, newStartDate);
    }

    localStorage.setItem(localStorageTaskComment, $('#trackingview-comment').val());

    setView(viewTracking);
}


/* TASK VIEW
********************************************************/
var taskviewCompleteListCache;
var taskviewListCache;
var taskviewIndex = 0;
var taskviewResults = 0;

function taskviewSearchKeyUp(event) {

    if (event.keyCode == 40 && taskviewIndex < taskviewResults - 1) { taskviewIndex = taskviewIndex + 1; taskviewAdjustIndex(); }
    if (event.keyCode == 38 && taskviewIndex > 0) { taskviewIndex = taskviewIndex - 1; taskviewAdjustIndex(); }

    if (event.keyCode == 13) {
        if (timelog.debug) { console.log('Task selected: ' + taskviewListCache[taskviewIndex].TaskID); }

        localStorage.setItem(localStorageTaskID, taskviewListCache[taskviewIndex].TaskID);
        localStorage.setItem(localStorageTaskStart, new Date());
        localStorage.setItem(localStorageTaskName, taskviewListCache[taskviewIndex].Details.ProjectHeader.Name + ' &raquo; ' + taskviewListCache[taskviewIndex].FullName);
        localStorage.setItem(localStorageTaskComment, '');

        $('#taskview-search').val('');
        $('#taskview-list').hide();

        setView(viewTracking);
    }

    if (event.keyCode != 46 && event.keyCode != 8 && event.keyCode < 48) { return; } // DELETE + BACKSPACE exception
    if ($('#taskview-search').val().length > 0) {

        timelog.getTasksAllocatedToEmployeeSuccessCallback = taskviewGetAllocationsSuccess;
        timelog.getTasksAllocatedToEmployeeFailureCallback = taskviewGetAllocationsFailure;
        timelog.getTasksAllocatedToEmployee();

    } else {

        $('#taskview-list').hide();

    }

}

function taskviewSearchKeyDown(event) {
    if (event.which == 40 || event.which == 38 || event.which == 13) { event.preventDefault(); }
}

function taskviewReloadWorkunitsClick() {
    timelog.invalidateGetEmployeeWork();
}

function taskviewGetAllocationsSuccess(data) {
    $('#taskview-list').show();
    $('#taskview-list').html('');

    taskviewCompleteListCache = data;
    taskviewResults = 0;
    taskviewListCache = [];

    for (index in data) {

        var fullName = data[index].Details.ProjectHeader.Name + ' &raquo; ' + data[index].FullName;

		if (taskviewResults < 50 && data[index].Details.IsParent != "true" && fullName.toLowerCase().indexOf($('#taskview-search').val().toLowerCase()) > -1) {
		    $('#taskview-list').append('<li data-id="' + data[index].TaskID + '">' + fullName + '</li>');
		    taskviewResults = taskviewResults + 1;
		    taskviewListCache.push(data[index]);
		}

    }

    if (taskviewIndex >= taskviewResults) {
        taskviewIndex = 0;
    }

    if ($('#taskview-list').html().length == 0) {
        $('#taskview-list').html('No tasks found :(');
    }

    taskviewAdjustIndex();
}

function taskviewGetAllocationsFailure(error) {
    $('#taskview-list').show();
    $('#taskview-list').html('<li>' + error + '</li>');
}

function taskviewGetEmployeeWorkSuccess(data) {
    $('#taskview-workunits').show();

    var list = $('#taskview-workunits table tbody');
    list.html('');

    var total = moment.duration(0);

    for (index in data) {

        var duration = moment.duration.fromIsoduration(data[index].Duration);

        list.append('<tr>' +
            '<td title="' + data[index].Details.ProjectHeader.Name + '">' + data[index].Details.ProjectHeader.Name + '</td>' +
            '<td title="' + data[index].Details.TaskHeader.FullName + '">' + data[index].Details.TaskHeader.FullName + '</td>' +
            '<td title="' + data[index].Description + '">' + data[index].Description + '</td>' +
            '<td>' + duration.hours() + ':' + duration.minutes().pad() + '</td><td></td></tr>');

        total = total.add(duration);

    }

    if (list.html().length == 0) {
        list.html('<tr><td></td><td>Nothing yet</td><td></td><td></td><td></td></tr>');
    } else {
        list.append('<tr style="font-weight: bold;"><td></td><td></td><td style="text-align:right;">Total</td><td>' + total.hours() + ':' + total.minutes().pad() + '</td><td></td></tr>');
    }
}

function taskviewGetEmployeeWorkFailure(error) {
    $('#taskview-workunits').show();
    $('#taskview-workunits table tbody').html('<tr><td></td><td>' + error + '</td><td></td><td></td><td></td></tr>');
}

function taskviewAdjustIndex() {
    $('#taskview-list li').removeClass('task-active');
    if (taskviewResults > 0) {
        $($('#taskview-list li')[taskviewIndex]).addClass('task-active');
    }
}