/// <reference path="timelog.js" />

var debug = localStorage.getItem('TimeLogDebug') || false;

$(document).ready(function() {

    // timelog setup
    timelog.tryAuthenticateSuccessCallback = userviewGetTokenSuccess;
    timelog.tryAuthenticateFailureCallback = userviewGetTokenError;

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

	// Tracking View
	$("#taskSearch").keyup(function() {

		if ($(this).val().length > 0) {
			$('#taskSearchList').show();
		} else {
			$('#taskSearchList').hide();
		}

	});

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
	} else {
		userviewToggleClick();
	}

});

/* HELPERS
********************************************************/


/* GENERAL
********************************************************/
function showTaskView() {
	$('.taskView').show();
	$('.trackingView').hide();
	$('.workUnitView').hide();
}

function showTrackingView() {
	$('.taskView').hide();
	$('.trackingView').show();
	$('.workUnitView').hide();
}

function showWorkUnitView() {
	$('.taskView').hide();
	$('.trackingView').hide();
	$('.workUnitView').show();
}
function showEmptyView() {
	$('.taskView').hide();
	$('.trackingView').hide();
	$('.workUnitView').hide();
}

function signOut() {
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
var taskListCache;

function trackingviewSearchKeyUp(key) {

}

function getTaskList(search) {

}