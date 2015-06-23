/// <reference path="timelog.js" />

var debug = localStorage.getItem('TimeLogDebug') || false;

$(document).ready(function() {

    // timelog setup
    timelog.authenticateSuccessCallback = userviewGetTokenSuccess;
    timelog.authenticateFailureCallback = userviewGetTokenError;

	// User View
	$('#userview-toggle').click(userviewToggleClick);
	$('#userview-signout').click(signOut);
	$('#userview-submit').click(userviewSubmitClick);
	$('#userview-username').keyup(userviewKeyUp);
	$('#userview-password').keyup(userviewKeyUp);
	$('#userview-url').val(timelog.url);
	$('#userview-username').val(timelog.username);

	// Work Unit View
	$('#workunitview-back').click(showTrackingView);

	// Task View
	$("#taskview-search").keyup(taskviewSearchKeyUp);
	$("#taskview-search").keydown(taskviewSearchKeyDown);

    // Tracking View
	setInterval(trackingviewTrackTime, 1000);

	// Debug
	if (debug) {
	    timelog.debug = true;
		$('.debug').show();
		$('#debug-taskview').click(showTaskView);
		$('#debug-trackingview').click(showTrackingView);
		$('#debug-workunitview').click(showWorkUnitView);
	}

	// General
	adjustClock();
	setInterval(adjustClock, 1000);

	if (timelog.isTokenValid()) {

	    showSignedInView();
	    if (localStorage.getItem(localStorageTaskID) != undefined) {
	        showTrackingView();
	    }

	} else {
		userviewToggleClick();
	}

});

/* HELPERS
********************************************************/


/* GENERAL
********************************************************/
function showTaskView() {
	$('.taskview').show();
	$('.trackingview').hide();
	$('.workunitview').hide();

	$('#taskview-search').focus();
}

function showTrackingView() {
	$('.taskview').hide();
	$('.trackingview').show();
	$('.workunitview').hide();

	trackingviewTrackTime();
}

function showWorkUnitView() {
	$('.taskview').hide();
	$('.trackingview').hide();
	$('.workunitview').show();
}
function showEmptyView() {
	$('.taskview').hide();
	$('.trackingview').hide();
	$('.workunitview').hide();
}

function signOut() {

    timelog.signOut();
	showEmptyView();
	$('#userview-notloggedin').show();
	$('#userview-loggedin').hide();
	userviewShowLoginDialog();
}

function showSignedInView() {
	$('#userview-notloggedin').hide();
	$('#userview-loggedin').show();
	$('#userview-logindialog').hide();
	showTaskView();
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
function userviewToggleClick() {
	var dialog = $('#userview-logindialog');

	if (dialog.is(":visible")) {
		dialog.hide();
	} else {
		userviewShowLoginDialog();
	}
}

function userviewShowLoginDialog() {
	var dialog = $('#userview-logindialog');
	dialog.show();
	$('#userview-loginstatus').hide();

	if ($('#userview-username').val().length == 0) {
		$('#userview-username').focus();
	} else {
		$('#userview-password').focus();
	}
}

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
	showSignedInView();
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
var localStorageTaskID = 'timelog-taskid';
var localStorageStart = 'timelog-taskstart';
var localStorageName = 'timelog-taskname';

function trackingviewStart(taskID, taskName) {
    localStorage.setItem(localStorageTaskID, taskID);
    localStorage.setItem(localStorageStart, new Date());
    localStorage.setItem(localStorageName, taskName);
    showTrackingView();
}

function trackingviewEnd() {
    localStorage.removeItem(localStorageTaskID);
    localStorage.removeItem(localStorageStart);
    localStorage.removeItem(localStorageName);
    showTaskView();
}

function trackingviewTrackTime() {

    if ($('.trackingview').is(':visible')) {

        var taskid = localStorage.getItem(localStorageTaskID);
        var start = Date.parse(localStorage.getItem(localStorageStart));
        var name = localStorage.getItem(localStorageName);

        if (taskid == undefined || taskid <= 0) {
            trackingviewEnd();
        }

        $('#trackingview-name').html(name);

        var diffMiliseconds = Math.abs(new Date() - start); // in miliseconds
        var diffSeconds = diffMiliseconds / 1000;
        var diffMinutes = diffSeconds / 60;
        var diffHours = diffMinutes / 60;

        var calcMinutes = diffMinutes - (Math.round(diffHours) * 60);

        var diffString = Math.round(diffHours) + 'h' + (Math.round(calcMinutes) > 9 ? '' : '0') + Math.round(calcMinutes) + 'm';

        $('#trackingview-time').html(diffString);

    } else {

        $('#trackingview-name').html('...');
        $('#trackingview-time').html('0h00m');

    }
}

/* TASK VIEW
********************************************************/
var taskviewListCache;
var taskviewIndex = 0;
var taskviewResults = 0;

function taskviewSearchKeyUp(event) {

    if (event.keyCode == 40 && taskviewIndex < taskviewResults - 1) { taskviewIndex = taskviewIndex + 1; taskviewAdjustIndex(); }
    if (event.keyCode == 38 && taskviewIndex > 0) { taskviewIndex = taskviewIndex - 1; taskviewAdjustIndex(); }

    if (event.keyCode == 13) {
        if (timelog.debug) { console.log('Task selected: ' + taskviewListCache[taskviewIndex].TaskID); }
        selectedItem = $($('#taskview-list')[taskviewIndex]);
        trackingviewStart(selectedItem.data('id'), selectedItem.text());
    }

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

function taskviewGetAllocationsSuccess(data) {
    $('#taskview-list').show();
    $('#taskview-list').html('');

    taskviewListCache = data;
    taskviewResults = 0;
    for (index in data) {

        var fullName = data[index].Details.ProjectHeader.Name + ' &raquo; ' + data[index].Name;

		if (fullName.indexOf($('#taskview-search').val()) > -1) {
		    $('#taskview-list').append('<li data-id="' + data[index].TaskID + '">' + fullName + '</li>');
		    taskviewResults = taskviewResults + 1;
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

function taskviewAdjustIndex() {
    $('#taskview-list li').removeClass('task-active');
    if (taskviewResults > 0) {
        $($('#taskview-list li')[taskviewIndex]).addClass('task-active');
    }
}