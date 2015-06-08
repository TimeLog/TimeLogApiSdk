/*
TimeLog.JavaScript.SDK v0.1-alpha
https://github.com/TimeLog/TimeLogApiSdk
Author: Søren Øxenhave, TimeLog A/S.
Attribution 4.0 International (CC BY 4.0)
*/
(function(timelog, $, undefined) {

    timelog.debug = false;

    var x2js = new X2JS();

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
        '<Description>{7}</Description><TimeStamp>{8}</TimeStamp><IsEditable>false</IsEditable><AdditionalText i:nil="true" /><Details i:nil="true" />' +
        '</WorkUnit></work>' +
        '<source>50</source>' +
        '<token xmlns:d4p1="http://www.timelog.com/api/tlp/v1_3" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">' +
        '<d4p1:Initials>{9}</d4p1:Initials><d4p1:Expires>{10}</d4p1:Expires><d4p1:Hash>{11}</d4p1:Hash>' +
        '</token>' +
        '</InsertWork>' +
        '</s:Body></s:Envelope>';

    // Store base information
    var _url = '';
    timelog.url = function () {

        if (timelog.enableLocalStorageCache) {
            _url = localStorage.getItem(localStorageUrlKey);
        }

        return _url;

    };

    var _username = '';
    timelog.username = function () {

        if (timelog.enableLocalStorageCache) {
            _username = localStorage.getItem(localStorageUsernameKey);
        }

        return _username;

    };

    // Store token
    var tokenInitials = '';
    var tokenExpires = '';
    var tokenHash = '';
    timelog.getToken = function () {

        if (timelog.debug) { console.log('[timelog.getToken] Executing'); }

        var token = { Initials: tokenInitials, Expires: new Date('2001-01-01 01:01:01'), Hash: tokenHash };

        if (tokenInitials.length == 0 && timelog.enableLocalStorageCache) {
            var rawToken = x2js.xml_str2json(localStorage.getItem(localStorageTokenKey));

            if (rawToken != undefined) {
                token = rawToken.SecurityToken;
            }
        }

        if (timelog.enableAutoLogin && timelog.enableLocalStorageCache && new Date(token.Expires) < new Date()) {
            if (timelog.debug) { console.log('[timelog.getToken] AutoLogin enabled and token expired'); }
            timelog.tryAuthenticate('', '', '');
        }

        return token;

    };

    // Disable to only use private variables for url, username and token.
    timelog.enableLocalStorageCache = true;

    // Automatically obtain token. Requires enableLocalStorageCache = true
    timelog.enableAutoLogin = true;

    // Private variables
    var localStorageUrlKey = 'TimeLogUrl';
    var localStorageUsernameKey = 'TimeLogUsername';
    var localStoragePasswordKey = 'TimeLogPassword';
    var localStorageTokenKey = 'TimeLogToken';

    // Tries to authenticate and get a token.
    timelog.tryAuthenticateSuccessCallback = undefined;
    timelog.tryAuthenticateFailureCallback = undefined;
    timelog.tryAuthenticate = function(url, username, password) {

        if (timelog.debug) { console.log('[timelog.tryAuthenticate] Executing (url: ' + url + ', username: ' + username + ', password: ' + password + ')'); }
        if (timelog.enableLocalStorageCache) {

            if (url.length == 0) {
                url = localStorage.getItem(localStorageUrlKey);
            } else {
                localStorage.setItem(localStorageUrlKey, url);
            }

            if (username.length == 0) {
                username = localStorage.getItem(localStorageUsernameKey);
            } else {
                localStorage.setItem(localStorageUsernameKey, username);
            }

            if (password.length == 0 && localStorage.getItem(localStoragePasswordKey) != undefined) {
                password = CryptoJS.AES.decrypt(localStorage.getItem(localStoragePasswordKey), '0B1EC554C85C').toString(CryptoJS.enc.Utf8);
            } else {

                localStorage.setItem(localStoragePasswordKey, CryptoJS.AES.encrypt(password, '0B1EC554C85C'));
            }

            if (timelog.debug) { console.log('[timelog.tryAuthenticate] After localStorage fetch (url: ' + url + ', username: ' + username + ', password: ' + password + ')'); }
        }

        $.ajax({
            type: "POST",
            url: getSecurityServiceUrl(url),
            headers: { "SOAPAction": "GetTokenRequest", "Content-Type": "text/xml" },
            data: templateGetToken.replace('{0}', username).replace('{1}', password),
            success: tryAuthenticateSuccess,
            error: tryAuthenticateFailure,
        });

    };

    timelog.isTokenValid = function() {
        return new Date(timelog.getToken().Expires) > new Date();
    }

    function tryAuthenticateSuccess(data) {
        var result = x2js.xml_str2json(new XMLSerializer().serializeToString(data)).Envelope.Body.GetTokenResponse.GetTokenResult;
        if (result.ResponseState.__text == "Success") {

            if (timelog.debug) { console.log('[timelog.tryAuthenticateSuccess] Token obtained'); }

            if (timelog.enableLocalStorageCache) {
                localStorage.setItem(localStorageTokenKey, x2js.json2xml_str(result.Return));
            }

            if (timelog.tryAuthenticateSuccessCallback != undefined) {
                timelog.tryAuthenticateSuccessCallback();
            }

        } else {

            var errorMsg = result.Messages.APIMessage[0].Message.toString().substring(result.Messages.APIMessage[0].Message.toString().indexOf('\''));
            if (timelog.debug) { console.log('[timelog.tryAuthenticateSuccess] Token failed (' + errorMsg + ')'); }

            if (timelog.enableLocalStorageCache) {
                localStorage.removeItem(localStoragePasswordKey);
                localStorage.removeItem(localStorageTokenKey);
            }

            if (timelog.tryAuthenticateFailureCallback != undefined) {
                timelog.tryAuthenticateFailureCallback(errorMsg);
            }

        }
    }

    function tryAuthenticateFailure(jqhxr, textstatus, errrorthrown) {

        if (timelog.debug) { console.log('[timelog.tryAuthenticateFailure] Token failed (' + textStatus + ')'); }

        timelog.tokenInitials = '';
        timelog.tokenTimestamp = '';
        timelog.tokenHash = '';

        if (timelog.enableLocalStorageCache) {
            localStorage.removeItem(localStoragePasswordKey);
            localStorage.removeItem(localStorageTokenKey);
        }

        if (tryAuthenticateFailureCallback != undefined) {
            var errorMsg = textstatus;
            tryAuthenticateFailureCallback(errorMsg);
        }

    }

    function getSecurityServiceUrl(url) {
        var urlSecurityService = '{0}/WebServices/Security/V1_2/SecurityServiceSecure.svc';

        if (url.length == 0 && timeog.enableLocalStorageCache) {
            url = localStorage.getItem('TimeLogUrl');
        }

        url = url.replace('http://', 'https://');
        return urlSecurityService.replace('{0}', url);
    }


}(window.timelog = window.timelog || {}, jQuery));