/// <reference path="https://api.stackexchange.com/js/2.0/all.js" />
/// <reference path="~/Scripts/lib/jquery.js"/>
$(function () {

    function getUserDetails(accessToken) {
        var url = 'https://api.stackexchange.com/2.0/me'; //?order=desc&sort=reputation&site=stackoverflow
        var params = {
            access_token: accessToken,
            filter: 'default',
            key: pageData.key
        };

        $.getJSON(url, params, function (data) {
            console.log('/me Success!');
            console.log(data);
        });
    }

    SE.init({
        clientId: pageData.clientId,
        key: pageData.key,
        channelUrl: pageData.channelUrl,
        complete: function (data) {
            console.log('Initialized');
            console.log(data);
            $('#login').removeAttr('disabled');
        }
    });

    $('#login').click(function () {
        SE.authenticate({
            success: function (data) {
                getUserDetails(data.accessToken);
            },
            error: function (data) {
                $('#authError')
                    .html('Sorry. Authentication failed. ' + data.errorName + ':' + data.errorMessage)
                    .show();
            },
            scope: [],
            networkUsers: false
        });
    });



});