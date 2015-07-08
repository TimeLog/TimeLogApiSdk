/*
TimeLog.JavaScript.SDK v0.1-alpha
https://github.com/TimeLog/TimeLogApiSdk
Author: Søren Øxenhave, TimeLog A/S.
Attribution 4.0 International (CC BY 4.0)
*/
(function(timelog, $, undefined) {

    timelog.debug = false;

    // ReSharper disable once InconsistentNaming
    var x2Js = new X2JS();

    var templateGetToken =
        '<s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/"><s:Body>' +
        '<GetToken xmlns="http://www.timelog.com/api/tlp/v1_2"><user>{0}</user><password>{1}</password></GetToken>' +
        '</s:Body></s:Envelope>';
    var templateGetTasksAllocated =
        '<s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/"><s:Body>' +
        '<GetTasksAllocatedToEmployee xmlns="http://www.timelog.com/api/tlp/v1_6">' +
        '<initials>{0}</initials>' +
        '<token xmlns:d4p1="http://www.timelog.com/api/tlp/v1_3" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">' +
        '<d4p1:Initials>{1}</d4p1:Initials><d4p1:Expires>{2}</d4p1:Expires><d4p1:Hash>{3}</d4p1:Hash>' +
        '</token>' +
        '</GetTasksAllocatedToEmployee>' +
        '</s:Body></s:Envelope>';
    var templateInsertWork =
        '<s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/"><s:Body>' +
        '<InsertWork xmlns="http://www.timelog.com/api/tlp/v1_6">' +
        '<work xmlns:i="http://www.w3.org/2001/XMLSchema-instance"><WorkUnit>' +
        '<GUID>{0}</GUID><AllocationGUID>{1}</AllocationGUID><TaskID>{2}</TaskID><EmployeeInitials>{3}</EmployeeInitials>' +
        '<Duration>{4}</Duration><StartDateTime>{5}</StartDateTime><EndDateTime>{6}</EndDateTime>' +
        '<Description>{7}</Description><TimeStamp i:nil="true" /><IsEditable>false</IsEditable><AdditionalText i:nil="true" /><Details i:nil="true" />' +
        '</WorkUnit></work>' +
        '<source>50</source>' +
        '<token xmlns:d4p1="http://www.timelog.com/api/tlp/v1_3" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">' +
        '<d4p1:Initials>{8}</d4p1:Initials><d4p1:Expires>{9}</d4p1:Expires><d4p1:Hash>{10}</d4p1:Hash>' +
        '</token>' +
        '</InsertWork>' +
        '</s:Body></s:Envelope>';
    var templateGetEmployeeWork =
        '<s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/"><s:Body>' +
        '<GetEmployeeWork xmlns="http://www.timelog.com/api/tlp/v1_6">' +
        '<initials>{0}</initials>' +
        '<startDate>{1}</startDate>' +
        '<endDate>{2}</endDate>' +
        '<token xmlns:d4p1="http://www.timelog.com/api/tlp/v1_3" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">' +
        '<d4p1:Initials>{3}</d4p1:Initials>' +
        '<d4p1:Expires>{4}</d4p1:Expires>' +
        '<d4p1:Hash>{5}</d4p1:Hash>' +
        '</token>' +
        '</GetEmployeeWork>' +
        '</s:Body></s:Envelope>';

    /* BASE INFORMATION
    ********************************************************/
    var localStorageUrlKey = 'TimeLogUrl';
    var localStorageUsernameKey = 'TimeLogUsername';
    var localStoragePasswordKey = 'TimeLogPassword';
    var localStorageTokenKey = 'TimeLogToken';
    var localStorageTasksAllocatedKey = 'TimeLogTasksAllocated';
    var localStorageTasksAllocatedCacheExpireKey = 'TimeLogTasksAllocatedCacheExpire';
    var localStorageEmployeeWorkKey = 'TimeLogEmployeeWork';
    var localStorageEmployeeWorkCacheExpireKey = 'TimeLogEmployeeWorkCacheExpire';
    var url = '';
    var username = '';

    timelog.url = function () {

        if (timelog.enableLocalStorageCache) {
            url = localStorage.getItem(localStorageUrlKey);
        }

        return url;

    };

    timelog.username = function () {

        if (timelog.enableLocalStorageCache) {
            username = localStorage.getItem(localStorageUsernameKey);
        }

        return username;

    };

    /* SETTINGS
    ********************************************************/

    // Disable to only use private variables for url, username and token.
    timelog.enableLocalStorageCache = true;

    // Automatically obtain token. Requires enableLocalStorageCache = true
    timelog.enableAutoLogin = true;


    /* HELPERS
    ********************************************************/
    function getSecurityServiceUrl() {
        var urlSecurityService = '{0}/WebServices/Security/V1_2/SecurityServiceSecure.svc';

        if ((url == undefined || url.length === 0) && timelog.enableLocalStorageCache) {
            url = localStorage.getItem('TimeLogUrl');
        }

        url = url.replace('http://', 'https://');
        return urlSecurityService.replace('{0}', url);
    }

    function getProjectManagementServiceUrl() {
        var urlProjectManagementService = '{0}/WebServices/ProjectManagement/V1_6/ProjectManagementServiceSecure.svc';

        if ((url == undefined || url.length === 0) && timelog.enableLocalStorageCache) {
            url = localStorage.getItem('TimeLogUrl');
        }

        url = url.replace('http://', 'https://');
        return urlProjectManagementService.replace('{0}', url);
    }

    // cc by-sa 3.0 license
    // http://stackoverflow.com/a/8809472/2830691
    timelog.generateUUID = function() {
        var d = new Date().getTime();
        var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = (d + Math.random() * 16) % 16 | 0;
            d = Math.floor(d / 16);
            return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
        });
        return uuid;
    };

    /* TOKEN AND AUTHENTICATION HANDLING
    ********************************************************/
    var tokenInitials = '';
    var tokenExpires = new Date('2001-01-01 01:01:01');
    var tokenHash = '';

    timelog.getToken = function () {

        var token = { Initials: tokenInitials, Expires: tokenExpires, Hash: tokenHash };

        if (tokenInitials.length === 0 && timelog.enableLocalStorageCache) {
            if (timelog.debug) { console.log('[timelog.getToken] Restoring from localStorage'); }
            var rawToken = x2Js.xml_str2json(localStorage.getItem(localStorageTokenKey));

            if (rawToken != undefined) {
                token = rawToken.SecurityToken;
            }

            tokenInitials = token.Initials;
            tokenExpires = token.Expires;
            tokenHash = token.Hash;
        }

        if (timelog.enableAutoLogin && timelog.enableLocalStorageCache && new Date(token.Expires) < new Date() && localStorage.getItem(localStoragePasswordKey) != undefined) {
            if (timelog.debug) { console.log('[timelog.getToken] AutoLogin enabled and token expired'); }
            timelog.tryAuthenticate('', '', '');
        }

        return token;

    };

    // Tries to authenticate and get a token.
    timelog.authenticateSuccessCallback = undefined;
    timelog.authenticateFailureCallback = undefined;

    function tryAuthenticateSuccess(data) {
        var result = x2Js.xml_str2json(new XMLSerializer().serializeToString(data)).Envelope.Body.GetTokenResponse.GetTokenResult;
        if (result.ResponseState.__text === "Success") {

            if (timelog.debug) { console.log('[timelog.tryAuthenticateSuccess] Token obtained'); }

            tokenInitials = result.Return.SecurityToken.Initials;
            tokenExpires = result.Return.SecurityToken.Expires;
            tokenHash = result.Return.SecurityToken.Hash;

            if (timelog.enableLocalStorageCache) {
                localStorage.setItem(localStorageTokenKey, x2Js.json2xml_str(result.Return));
            }

            if (timelog.authenticateSuccessCallback != undefined) {
                timelog.authenticateSuccessCallback();
            }

        } else {

            var errorMsg = result.Messages.APIMessage[0].Message.toString().substring(result.Messages.APIMessage[0].Message.toString().indexOf('\''));
            if (timelog.debug) { console.log('[timelog.tryAuthenticateSuccess] Token failed (' + errorMsg + ')'); }

            if (timelog.enableLocalStorageCache) {
                localStorage.removeItem(localStoragePasswordKey);
                localStorage.removeItem(localStorageTokenKey);
            }

            if (timelog.authenticateFailureCallback != undefined) {
                timelog.authenticateFailureCallback(errorMsg);
            }

        }
    }

    function tryAuthenticateFailure(jqhxr, textstatus) {

        if (timelog.debug) { console.log('[timelog.tryAuthenticateFailure] Token failed (' + textstatus + ')'); }

        tokenInitials = '';
        tokenExpires = new Date('2001-01-01 01:01:01');
        tokenHash = '';

        if (timelog.enableLocalStorageCache) {
            localStorage.removeItem(localStoragePasswordKey);
            localStorage.removeItem(localStorageTokenKey);
        }

        if (timelog.tryAuthenticateFailureCallback != undefined) {
            var errorMsg = textstatus;
            timelog.tryAuthenticateFailureCallback(errorMsg);
        }

    }

    timelog.tryAuthenticate = function(url, username, password) {

        if (timelog.debug) { console.log('[timelog.tryAuthenticate] Executing (url: ' + url + ', username: ' + username + ', password: ' + password + ')'); }
        if (timelog.enableLocalStorageCache) {

            if (url.length === 0) {
                url = localStorage.getItem(localStorageUrlKey);
            } else {
                localStorage.setItem(localStorageUrlKey, url);
            }

            if (username.length === 0) {
                username = localStorage.getItem(localStorageUsernameKey);
            } else {
                localStorage.setItem(localStorageUsernameKey, username);
            }

            if (password.length === 0 && localStorage.getItem(localStoragePasswordKey) != undefined) {
                password = CryptoJS.AES.decrypt(localStorage.getItem(localStoragePasswordKey), '0B1EC554C85C').toString(CryptoJS.enc.Utf8);
            } else if (password.length > 0) {
                localStorage.setItem(localStoragePasswordKey, CryptoJS.AES.encrypt(password, '0B1EC554C85C'));
            }

            if (timelog.debug) { console.log('[timelog.tryAuthenticate] After localStorage fetch (url: ' + url + ', username: ' + username + ', password: ' + password + ')'); }
        }

        if (url != undefined && username != undefined && url.length > 0 && username.length > 0 && password.length > 0) {
            $.ajax({
                type: "POST",
                url: getSecurityServiceUrl(),
                headers: { "SOAPAction": "GetTokenRequest", "Content-Type": "text/xml" },
                data: templateGetToken.replace('{0}', username).replace('{1}', password),
                success: tryAuthenticateSuccess,
                error: tryAuthenticateFailure
            });
        }

    };

    timelog.isTokenValid = function() {
        return new Date(timelog.getToken().Expires) > new Date();
    }

    timelog.signOut = function () {

        if (timelog.debug) { console.log('[timelog.signOut] Executing'); }

        tokenInitials = '';
        tokenExpires = new Date('2001-01-01 01:01:01');
        tokenHash = '';

        if (timelog.enableLocalStorageCache) {
            localStorage.removeItem(localStoragePasswordKey);
            localStorage.removeItem(localStorageTokenKey);

            localStorage.removeItem(localStorageEmployeeWorkCacheExpireKey);
            localStorage.removeItem(localStorageEmployeeWorkKey);
            localStorage.removeItem(localStorageTasksAllocatedCacheExpireKey);
            localStorage.removeItem(localStorageTasksAllocatedKey);
        }

    }

    /* TASKS - getTasksAllocatedToEmployee
    ********************************************************/
    var getTasksAllocatedToEmployeeList = undefined;
    var getTasksAllocatedToEmployeeRunning = false;

    timelog.invalidateGetTasksAllocatedToEmployee = function () {

        if (timelog.debug) { console.log('[timelog.invalidateGetTasksAllocatedToEmployee] Executed'); }

        getTasksAllocatedToEmployeeList = undefined;

        if (timelog.enableLocalStorageCache) {
            localStorage.removeItem(localStorageTasksAllocatedKey);
        }
    }

    timelog.getTasksAllocatedToEmployeeSuccessCallback = undefined;
    timelog.getTasksAllocatedToEmployeeFailureCallback = undefined;

    function getTasksAllocatedToEmployeeSuccess(data) {

        getTasksAllocatedToEmployeeRunning = false;

        if (data === '') {
            if (timelog.getTasksAllocatedToEmployeeSuccessCallback != undefined) {
                timelog.getTasksAllocatedToEmployeeSuccessCallback(getTasksAllocatedToEmployeeList);
                return;
            }
        }

        var result = x2Js.xml_str2json(new XMLSerializer().serializeToString(data)).Envelope.Body.GetTasksAllocatedToEmployeeResponse.GetTasksAllocatedToEmployeeResult;
        if (result.ResponseState.__text === "Success") {
            if (timelog.debug) { console.log('[timelog.getTasksAllocatedToEmployeeSuccess] Data downloaded'); }

            if (result.Return.Task == undefined) {
                getTasksAllocatedToEmployeeList = [];
            } else {
                getTasksAllocatedToEmployeeList = Array.isArray(result.Return.Task) ? result.Return.Task : [result.Return.Task];
            }

            if (timelog.enableLocalStorageCache) {
                localStorage.setItem(localStorageTasksAllocatedKey, JSON.stringify(getTasksAllocatedToEmployeeList));
                var expireDate = new Date();
                expireDate = expireDate.setTime(expireDate.getTime() + 4 * 60 * 60 * 1000); // 4 hours
                localStorage.setItem(localStorageTasksAllocatedCacheExpireKey, JSON.stringify(expireDate));
            }

            if (timelog.getTasksAllocatedToEmployeeSuccessCallback != undefined) {
                timelog.getTasksAllocatedToEmployeeSuccessCallback(getTasksAllocatedToEmployeeList);
            }

        } else {
            var errorMsg = result.Messages.APIMessage[0].Message.toString().substring(result.Messages.APIMessage[0].Message.toString().indexOf('\''));
            if (timelog.debug) { console.log('[timelog.getTasksAllocatedToEmployeeSuccess] Failed in API (' + errorMsg + ')'); }

            if (errorMsg.toLowerCase().indexOf('expired') > 0) {
                timelog.signOut();
            }

            if (timelog.enableLocalStorageCache) {
                localStorage.removeItem(localStorageTasksAllocatedKey);
            }

            if (timelog.getTasksAllocatedToEmployeeFailureCallback != undefined) {
                timelog.getTasksAllocatedToEmployeeFailureCallback(errorMsg);
            }
        }
    }

    function getTasksAllocatedToEmployeeFailure(jqhxr, textstatus) {

        getTasksAllocatedToEmployeeRunning = false;

        if (timelog.debug) { console.log('[timelog.getTasksAllocatedToEmployeeFailure] Failed (' + textstatus + ')'); }

        if (timelog.enableLocalStorageCache) {
            localStorage.removeItem(localStorageTasksAllocatedKey);
        }

        if (timelog.getTasksAllocatedToEmployeeFailureCallback != undefined) {
            timelog.getTasksAllocatedToEmployeeFailureCallback(textstatus);
        }

    }

    timelog.getTasksAllocatedToEmployee = function() {

        if (getTasksAllocatedToEmployeeRunning) {
            if (timelog.debug) { console.log('[timelog.getTasksAllocatedToEmployee] Already running skipping...') }
            return;
        }

        getTasksAllocatedToEmployeeRunning = true;

        if (getTasksAllocatedToEmployeeList == undefined && timelog.enableLocalStorageCache && localStorage.getItem(localStorageTasksAllocatedKey) != undefined) {
            getTasksAllocatedToEmployeeList = JSON.parse(localStorage.getItem(localStorageTasksAllocatedKey));
        }

        if (timelog.enableLocalStorageCache && new Date(parseInt(localStorage.getItem(localStorageTasksAllocatedCacheExpireKey))) < new Date()) {
            if (timelog.debug) { console.log('[timelog.getTasksAllocatedToEmployee] Cache expired redownloading'); }
            timelog.invalidateGetTasksAllocatedToEmployee();
        }

        if (getTasksAllocatedToEmployeeList != undefined) {
			getTasksAllocatedToEmployeeSuccess('');
			return;
		}

        if (timelog.debug) { console.log('[timelog.getTasksAllocatedToEmployee] Executing'); }

        $.ajax({
            type: "POST",
            url: getProjectManagementServiceUrl(),
            headers: { "SOAPAction": "GetTasksAllocatedToEmployeeRequest", "Content-Type": "text/xml" },
            data: templateGetTasksAllocated.replace('{0}', timelog.getToken().Initials).
                                            replace('{1}', timelog.getToken().Initials).
                                            replace('{2}', timelog.getToken().Expires).
                                            replace('{3}', timelog.getToken().Hash),
            success: getTasksAllocatedToEmployeeSuccess,
            error: getTasksAllocatedToEmployeeFailure
        });

    }

    /* TASKS - insertWork
    ********************************************************/
    timelog.insertWorkSuccessCallback = undefined;
    timelog.insertWorkFailureCallback = undefined;

    function insertWorkSuccess(data) {

        var result = x2Js.xml_str2json(new XMLSerializer().serializeToString(data)).Envelope.Body.InsertWorkResponse.InsertWorkResult;
        if (result.ResponseState.__text === "Success") {
            if (timelog.debug) { console.log('[timelog.insertWork] Work unit inserted'); }

            if (timelog.insertWorkSuccessCallback != undefined) {
                timelog.insertWorkSuccessCallback();
            }
        } else {
            var errorMsg = result.Return.BatchContainerOfWorkUnit.Message;
            if (errorMsg == undefined) {
                errorMsg = result.Messages.APIMessage[0].Message.toString().substring(result.Messages.APIMessage[0].Message.toString().indexOf('\''));
            }

            if (timelog.debug) { console.log('[timelog.insertWork] Failed in API (' + errorMsg + ')'); }

            if (errorMsg != undefined && errorMsg.toLowerCase().indexOf('expired') > 0) {
                timelog.signOut();
            }

            if (timelog.insertWorkFailureCallback != undefined) {
                timelog.insertWorkFailureCallback(errorMsg);
            }
        }

    }

    function insertWorkFailure(jqhxr, textstatus) {

        if (timelog.debug) { console.log('[timelog.insertWork] Failed (' + textstatus + ')'); }
        if (timelog.insertWorkFailureCallback != undefined) {
            timelog.insertWorkFailureCallback(textstatus);
        }

    }

    timelog.insertWork = function (taskId, start, end, comment) {

        var startDate = new Date(Date.parse(start));
        var endDate = new Date(Date.parse(end));

        if (timelog.debug) { console.log('[timelog.insertWork] Executing (taskID: ' + taskId + ', startDate: ' + startDate + ', endDate: ' + endDate + ', comment: ' + comment + ')'); }

        // Calculate duration
        var diffMiliseconds = Math.abs(endDate - startDate);
        var diffSeconds = diffMiliseconds / 1000;
        var diffMinutes = diffSeconds / 60;
        var diffHours = diffMinutes / 60;
        var calcMinutes = diffMinutes - (Math.floor(diffHours) * 60);

        // PnYnMnDTnHnMnS
        // P indicates the period (required)
        // nY indicates the number of years
        // nM indicates the number of months
        // nD indicates the number of days
        // T indicates the start of a time section (required if you are going to specify hours, minutes, or seconds)
        // nH indicates the number of hours
        // nM indicates the number of minutes
        // nS indicates the number of seconds
        var duration = 'PT' + Math.floor(diffHours) + 'H' + Math.floor(calcMinutes) + 'M';

        $.ajax({
            type: "POST",
            url: getProjectManagementServiceUrl(),
            headers: { "SOAPAction": "InsertWorkRequest", "Content-Type": "text/xml" },
            data: templateInsertWork.replace('{0}', timelog.generateUUID()).
                                            replace('{1}', '00000000-0000-0000-0000-000000000000'). // AllocationGuid
                                            replace('{2}', taskId). // Task ID
                                            replace('{3}', timelog.getToken().Initials). // Employee initials
                                            replace('{4}', duration). // Duration
                                            replace('{5}', startDate.toISOString()). // Start Date Time
                                            replace('{6}', endDate.toISOString()). // End Date Time
                                            replace('{7}', comment).
                                            replace('{8}', timelog.getToken().Initials).
                                            replace('{9}', timelog.getToken().Expires).
                                            replace('{10}', timelog.getToken().Hash),
            success: insertWorkSuccess,
            error: insertWorkFailure
        });

    }

    /* TASKS - getEmployeeWork
    ********************************************************/
    var getEmployeeWorkList = undefined;
    var getEmployeeWorkRunning = false;

    timelog.invalidateGetEmployeeWork = function () {
        getEmployeeWorkList = undefined;

        if (timelog.enableLocalStorageCache) {
            localStorage.removeItem(localStorageEmployeeWorkKey);
        }
    }

    timelog.getEmployeeWorkSuccessCallback = undefined;
    timelog.getEmployeeWorkFailureCallback = undefined;

    function getEmployeeWorkSuccess(data) {

        getEmployeeWorkRunning = false;

        if (data === '') {
            if (timelog.getEmployeeWorkSuccessCallback != undefined) {
                timelog.getEmployeeWorkSuccessCallback(getEmployeeWorkList);
                return;
            }
        }

        var result = x2Js.xml_str2json(new XMLSerializer().serializeToString(data)).Envelope.Body.GetEmployeeWorkResponse.GetEmployeeWorkResult;
        if (result.ResponseState.__text === "Success") {
            if (timelog.debug) { console.log('[timelog.getEmployeeWork] Data downloaded'); }

            if (result.Return.WorkUnit == undefined) {
                getEmployeeWorkList = [];
            } else {
                getEmployeeWorkList = Array.isArray(result.Return.WorkUnit) ? result.Return.WorkUnit : [result.Return.WorkUnit];
            }

            if (timelog.enableLocalStorageCache) {
                localStorage.setItem(localStorageEmployeeWorkKey, JSON.stringify(getEmployeeWorkList));
                var expireDate = new Date();
                expireDate = expireDate.setTime(expireDate.getTime() + 4 * 60 * 60 * 1000); // 4 hours
                localStorage.setItem(localStorageEmployeeWorkCacheExpireKey, JSON.stringify(expireDate));
            }

            if (timelog.getEmployeeWorkSuccessCallback != undefined) {
                timelog.getEmployeeWorkSuccessCallback(getEmployeeWorkList);
            }

        } else {
            var errorMsg = result.Messages.APIMessage[0].Message.toString().substring(result.Messages.APIMessage[0].Message.toString().indexOf('\''));
            if (timelog.debug) { console.log('[timelog.getEmployeeWork] Failed in API (' + errorMsg + ')'); }

            if (errorMsg.toLowerCase().indexOf('expired') > 0) {
                timelog.signOut();
            }

            if (timelog.enableLocalStorageCache) {
                localStorage.removeItem(localStorageEmployeeWorkKey);
            }

            if (timelog.getEmployeeWorkFailureCallback != undefined) {
                timelog.getEmployeeWorkFailureCallback(errorMsg);
            }
        }

    }

    function getEmployeeWorkFailure(jqhxr, textstatus) {

        getEmployeeWorkRunning = false;

        if (timelog.debug) { console.log('[timelog.getEmployeeWork] Failed (' + textstatus + ')'); }

        if (timelog.enableLocalStorageCache) {
            localStorage.removeItem(localStorageEmployeeWorkKey);
        }

        if (timelog.getEmployeeWorkFailureCallback != undefined) {
            timelog.getEmployeeWorkFailureCallback(textstatus);
        }

    }

    timelog.getEmployeeWork = function (start, end) {

        if (getEmployeeWorkRunning) {
            if (timelog.debug) { console.log('[timelog.getEmployeeWorkRunning] Already running skipping...') }
            return;
        }

        getEmployeeWorkRunning = true;

        if (getEmployeeWorkList == undefined && timelog.enableLocalStorageCache && localStorage.getItem(localStorageEmployeeWorkKey) != undefined) {
            getEmployeeWorkList = JSON.parse(localStorage.getItem(localStorageEmployeeWorkKey));
        }

        if (timelog.enableLocalStorageCache && new Date(parseInt(localStorage.getItem(localStorageEmployeeWorkCacheExpireKey))) < new Date()) {
            if (timelog.debug) { console.log('[timelog.getEmployeeWork] Cache expired redownloading'); }
            timelog.invalidateGetEmployeeWork();
        }

        if (getEmployeeWorkList != undefined) {
            getEmployeeWorkSuccess('');
            return;
        }

        var startDate = new Date(Date.parse(start));
        var endDate = new Date(Date.parse(end));

        if (timelog.debug) { console.log('[timelog.getEmployeeWork] Executing (startDate: ' + startDate + ', endDate: ' + endDate + ')'); }

        $.ajax({
            type: "POST",
            url: getProjectManagementServiceUrl(),
            headers: { "SOAPAction": "GetEmployeeWorkRequest", "Content-Type": "text/xml" },
            data: templateGetEmployeeWork.replace('{0}', timelog.getToken().Initials).
                                            replace('{1}', startDate.toISOString()). // Start Date Time
                                            replace('{2}', endDate.toISOString()). // End Date Time
                                            replace('{3}', timelog.getToken().Initials).
                                            replace('{4}', timelog.getToken().Expires).
                                            replace('{5}', timelog.getToken().Hash),
            success: getEmployeeWorkSuccess,
            error: getEmployeeWorkFailure
        });

    }

}(window.timelog = window.timelog || {}, jQuery));