/// <reference path="~/signalr"/>
$(function () {
    // Proxy created on the fly
    var setup = $.connection.setupHub;

    var userNameId = '#' + pageData.userNameId;

    var onUserNameChanged = function () {
        var userName = $(userNameId).val();
        console.log('User name is ' + userName);

        setup.isValidAndAvailable(userName)
            .done(function (data) {
                if (data.userName != $(userNameId).val())
                    return;
                
                $(userNameId).removeClass("invalid");
                $(userNameId).removeClass("unavailable");

                if (!data.isValid)
                    $(userNameId).addClass("invalid");

                if (!data.isAvailable)
                    $(userNameId).addClass("unavailable");
            });
    };


    // Start the connection
    $.connection.hub.start()
        .done(function () {
            $(userNameId).bind('input', onUserNameChanged);
            onUserNameChanged();
        });

});