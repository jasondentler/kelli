/// <reference path="https://api.stackexchange.com/js/2.0/all.js" />
/// <reference path="~/Scripts/lib/jquery.js"/>

$(function () {

    var accessToken = 'not_authenticated';

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
                .attr('alt', user.display_name)
                .addClass('avatar');

            var site = $('<div />').addClass('site');

            var siteIcon = $('<img />')
                .attr('src', user.site.icon_url)
                .attr('alt', user.site.name);

            site.append(siteIcon);
            site.append(user.site.name);
            
            var name = $('<span />').html(user.display_name).addClass('displayName');

            var item = $('<li />');
            img.appendTo(item);
            site.appendTo(item);
            name.appendTo(item);

            item.data('user', user);

            item.appendTo($('#users'));
        });

        $('#preAuth').hide();
        $('#postAuth').show();

        $('#users li').click(function () {
            var user = $(this).data('user');
            selectUser(user);
        });
    }

    function selectUser(user) {
        console.log('Selected user #' + user.user_id + ' from ' + user.site);
        $('#' + pageData.AccessTokenId).val(accessToken);
        $('#' + pageData.SiteNameId).val(user.site.api_site_parameter);
        $('#' + pageData.UserIdId).val(user.user_id);
        $('#auth').submit();
    }

    function getSiteListMap(data) {
        var items = [];
        $.each(data.items, function (idx, site) {
            items[site.site_url] = site;
        });
        return items;
    }

    function getUserDetails(accessToken, siteUrls) {
        var siteListRequest = $.getJSON('https://api.stackexchange.com/2.0/sites', {
            key: pageData.key,
            filter: 'default',
            page: 1,
            pageSize: 1000
        });

        siteListRequest.fail(function () {
            showError('An error occurred while trying to list the Stack Exchange sites');
        });

        siteListRequest.done(function (data) {
            console.log('Got site list');
            console.log(data);

            var siteListMap = getSiteListMap(data);
            console.log(siteListMap);

            var sites = $.map(siteUrls, function (site_url) {
                return siteListMap[site_url];
            });

            console.log(sites);

            var callbacks = $.map(sites, function (site) {
                var url = 'https://api.stackexchange.com/2.0/me';
                var params = {
                    access_token: accessToken,
                    filter: 'default',
                    key: pageData.key,

                    site: site.api_site_parameter,
                    order: 'desc',
                    sort: 'reputation'
                };
                console.log('Getting /me for ' + site.name);
                return $.getJSON(url, params)
                    .fail(function () {
                        showError('Unable to get your account(s) from ' + site.name);
                    });
            });

            $.when.apply(this, callbacks).done(function () {
                console.log('All /me operations completed');

                var args = Array.prototype.slice.call(arguments);

                var users = new Array();

                $.each(args, function (idx, jqXhrArgs) {
                    var items = jqXhrArgs[0].items;
                    var siteUrl = siteUrls[idx];
                    var site = siteListMap[siteUrl];
                    $.each(items, function (x, user) {
                        user.site = site;
                    });
                    users = users.concat(items);
                });

                console.log(users);

                console.log(JSON.stringify(users));

                showUsers(users);

            });
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

        var host = window.location.hostname;

        if (host == 'localhost') {
            var fakeUsers = [{ "user_id": 837001, "user_type": "registered", "creation_date": 1265907885, "display_name": "Jason Dentler", "profile_image": "http://www.gravatar.com/avatar/2aaf05c5e05389c501b4fd7451abecdb?d=identicon&r=PG", "reputation": 49, "reputation_change_day": 0, "reputation_change_week": 0, "reputation_change_month": 0, "reputation_change_quarter": 0, "reputation_change_year": 48, "last_access_date": 1343839172, "last_modified_date": 1332302705, "is_employee": false, "link": "http://stackoverflow.com/users/837001/jason-dentler", "website_url": "http://jasondentler.com/blog", "account_id": 444895, "badge_counts": { "gold": 0, "silver": 0, "bronze": 5 }, "accept_rate": 80, "site": { "site_type": "main_site", "name": "Stack Overflow", "logo_url": "http://cdn.sstatic.net/stackoverflow/img/logo.png", "api_site_parameter": "stackoverflow", "site_url": "http://stackoverflow.com", "audience": "professional and enthusiast programmers", "icon_url": "http://cdn.sstatic.net/stackoverflow/img/apple-touch-icon.png", "aliases": ["http://www.stackoverflow.com"], "site_state": "normal", "styling": { "link_color": "#0077CC", "tag_foreground_color": "#3E6D8E", "tag_background_color": "#E0EAF1" }, "launch_date": 1221436800, "favicon_url": "http://cdn.sstatic.net/stackoverflow/img/favicon.ico", "related_sites": [{ "name": "Stack Overflow Chat", "site_url": "http://chat.stackoverflow.com", "relation": "chat"}], "markdown_extensions": ["Prettify"]} }, { "user_id": 112565, "user_type": "registered", "creation_date": 1330624749, "display_name": "Jason Dentler", "profile_image": "http://www.gravatar.com/avatar/2aaf05c5e05389c501b4fd7451abecdb?d=identicon&r=PG", "reputation": 1, "reputation_change_day": 0, "reputation_change_week": 0, "reputation_change_month": 0, "reputation_change_quarter": 0, "reputation_change_year": 0, "last_access_date": 1339697458, "last_modified_date": 1332300364, "is_employee": false, "link": "http://serverfault.com/users/112565/jason-dentler", "website_url": "http://jasondentler.com/blog", "account_id": 444895, "badge_counts": { "gold": 0, "silver": 0, "bronze": 2 }, "site": { "site_type": "main_site", "name": "Server Fault", "logo_url": "http://cdn.sstatic.net/serverfault/img/logo.png", "api_site_parameter": "serverfault", "site_url": "http://serverfault.com", "audience": "system administrators and desktop support professionals", "icon_url": "http://cdn.sstatic.net/serverfault/img/apple-touch-icon.png", "site_state": "normal", "styling": { "link_color": "#10456A", "tag_foreground_color": "#444444", "tag_background_color": "#F3F1D9" }, "launch_date": 1243296000, "favicon_url": "http://cdn.sstatic.net/serverfault/img/favicon.ico", "related_sites": [{ "name": "Meta Server Fault", "site_url": "http://meta.serverfault.com", "relation": "meta", "api_site_parameter": "meta.serverfault" }, { "name": "Chat Stack Exchange", "site_url": "http://chat.stackexchange.com", "relation": "chat"}], "twitter_account": "ServerFault", "markdown_extensions": ["Prettify"]} }, { "user_id": 11857, "user_type": "registered", "creation_date": 1343513463, "display_name": "Jason Dentler", "profile_image": "http://www.gravatar.com/avatar/2aaf05c5e05389c501b4fd7451abecdb?d=identicon&r=PG", "reputation": 1, "reputation_change_day": 0, "reputation_change_week": 0, "reputation_change_month": 0, "reputation_change_quarter": 0, "reputation_change_year": 0, "last_access_date": 1343837145, "is_employee": false, "link": "http://stackapps.com/users/11857/jason-dentler", "website_url": "http://jasondentler.com/blog", "account_id": 444895, "badge_counts": { "gold": 0, "silver": 0, "bronze": 0 }, "site": { "site_type": "main_site", "name": "Stack Apps", "logo_url": "http://cdn.sstatic.net/stackapps/img/logo.png", "api_site_parameter": "stackapps", "site_url": "http://stackapps.com", "audience": "apps, scripts, and development with the Stack Exchange API", "icon_url": "http://cdn.sstatic.net/stackapps/img/apple-touch-icon.png", "site_state": "normal", "styling": { "link_color": "#0077DD", "tag_foreground_color": "#555555", "tag_background_color": "#E7ECEC" }, "launch_date": 1274313600, "favicon_url": "http://cdn.sstatic.net/stackapps/img/favicon.ico", "related_sites": [{ "name": "Chat Stack Exchange", "site_url": "http://chat.stackexchange.com", "relation": "chat"}]} }, { "user_id": 56471, "user_type": "registered", "creation_date": 1339454900, "display_name": "Jason Dentler", "profile_image": "http://www.gravatar.com/avatar/2aaf05c5e05389c501b4fd7451abecdb?d=identicon&r=PG", "reputation": 1, "reputation_change_day": 0, "reputation_change_week": 0, "reputation_change_month": 0, "reputation_change_quarter": 0, "reputation_change_year": 0, "last_access_date": 1340923644, "is_employee": false, "link": "http://programmers.stackexchange.com/users/56471/jason-dentler", "website_url": "http://jasondentler.com/blog", "account_id": 444895, "badge_counts": { "gold": 0, "silver": 0, "bronze": 0 }, "site": { "site_type": "main_site", "name": "Programmers", "logo_url": "http://cdn.sstatic.net/programmers/img/logo.png", "api_site_parameter": "programmers", "site_url": "http://programmers.stackexchange.com", "audience": "professional programmers interested in conceptual questions about software development", "icon_url": "http://cdn.sstatic.net/programmers/img/apple-touch-icon.png", "aliases": ["http://programmer.stackexchange.com"], "site_state": "normal", "styling": { "link_color": "#0077CC", "tag_foreground_color": "#5D5D5D", "tag_background_color": "#FFF" }, "closed_beta_date": 1283299200, "open_beta_date": 1283904000, "launch_date": 1292457600, "favicon_url": "http://cdn.sstatic.net/programmers/img/favicon.ico", "related_sites": [{ "name": "Programmers Meta", "site_url": "http://meta.programmers.stackexchange.com", "relation": "meta", "api_site_parameter": "meta.programmers" }, { "name": "Chat Stack Exchange", "site_url": "http://chat.stackexchange.com", "relation": "chat"}], "twitter_account": "StackProgrammer", "markdown_extensions": ["Prettify"]}}];
            showUsers(fakeUsers);
            return;
        }

        SE.authenticate({
            success: function (data) {
                accessToken = data.accessToken;
                var sites = $.map(data.networkUsers, function (networkUser, idx) {
                    return networkUser.site_url;
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

