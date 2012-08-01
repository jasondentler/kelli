/// <reference path="https://api.stackexchange.com/js/2.0/all.js" />
/// <reference path="~/Scripts/lib/jquery.js"/>
$(function () {

    function showError(errorText) {
        $('#authError')
            .html(errorText)
            .show();
    }

    function hideError() {
        $('#authError').hide();
    }

    function getUserDetails(accessToken, sites) {
        var callbacks = $.map(sites, function (site, idx) {
            var url = 'https://api.stackexchange.com/2.0/me';
            var params = {
                access_token: accessToken,
                filter: 'default',
                key: pageData.key,
                site: site.replace(/\s+/g, ''),
                order: 'desc',
                sort: 'reputation'
            };
            console.log('Getting /me for ' + site);
            return $.getJSON(url, params);
        });

        $.when.apply(this, callbacks).done(function () {
            console.log('All /me operations completed');
            for (var i = 0; i < arguments.length; i++) {
                console.log(arguments[i]);
            }
        }).fail(function () {
            showError('One or more errors occurred while trying to list Stack Exchange users for this account');
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
        hideError();
        SE.authenticate({
            success: function (data) {
                var sites = $.map(data.networkUsers, function (networkUser, idx) {
                    return networkUser.site_name;
                });
                getUserDetails(data.accessToken, sites);
            },
            error: function (data) {
                showError('Sorry. Authentication failed. ' + data.errorName + ':' + data.errorMessage);
            },
            scope: [],
            networkUsers: true
        });
    });



});