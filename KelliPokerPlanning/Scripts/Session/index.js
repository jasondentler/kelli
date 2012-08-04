///<reference path="/Scripts/lib/jquery.js"/>
$(function () {
    $('#qr_large').click(function () {
        $('#qr_large').hide();
    });

    $('#qr').click(function () {
        $('#qr_large').show();
    });
});