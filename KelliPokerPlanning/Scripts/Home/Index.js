/// <reference path="~/signalr"/>
$(function () {
    // Proxy created on the fly
    var chat = $.connection.setupHub;

    // Declare a function on the chat hub so the server can invoke it
    chat.addMessage = function (message) {
        $('#messages').append('<li>' + message + '</li>');
    };

    $("#broadcast").click(function () {
        // Call the chat method on the server
        chat.send($('#msg').val());
    });

    // Start the connection
    $.connection.hub.start();
});