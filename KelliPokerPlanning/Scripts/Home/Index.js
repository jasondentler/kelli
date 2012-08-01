/// <reference path="https://api.stackexchange.com/js/2.0/all.js" />
/// <reference path="~/Scripts/lib/jquery.js"/>
$(function () {
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
                console.log('Success!');
                console.log(data);
            },
            error: function (data) {
                console.log('Error!');
                console.log(data);
            },
            scope: [],
            networkUsers: true
        });
    });

});