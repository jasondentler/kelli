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

    function showUsers(users) {
        $.each(users, function (idx, user) {
            var img = $('<img />')
                .attr('src', user.profile_image)
                .attr('alt', user.display_name);

            var name = $('<span />').html(user.display_name).addClass('displayName');
            var site = $('<span />').html(user.site).addClass('site');

            var item = $('<li />');
            img.appendTo(item);
            name.appendTo(item);
            site.appendTo(item);

            item.appendTo($('#users'));
        });

        $('#preAuth').hide();
        $('#postAuth').show();
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

            var args = Array.prototype.slice.call(arguments);

            var users = new Array();

            $.each(args, function (idx, jqXhrArgs) {
                var items = jqXhrArgs[0].items;
                var site = sites[idx];
                $.each(items, function (x, user) {
                    user.site = site;
                });
                users = users.concat(items);
            });

            console.log(users);

            console.log(JSON.stringify(users));

            showUsers(users);

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