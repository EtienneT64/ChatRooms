﻿"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${user}: ${message}`;

    //clearing code
    document.getElementById("messageContent").value = ''; // Clear the input field

    // Scroll to the bottom
    var messagesList = document.getElementById("messagesList");
    messagesList.scrollTop = messagesList.scrollHeight;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {

    var chatroomId = document.getElementById("chatroomId").value;
    var userId = document.getElementById("userId").value;
    var messageContent = document.getElementById("messageContent").value;
    //connection.invoke("SendMessageToGroup", chatroomName, message).catch(function (err) {
    //    return console.error(err.toString());
    //});
    connection.invoke("SendMessage", Number(chatroomId), userId, messageContent).catch(function (err) {
        return console.error(err.toString());
    });

    //document.getElementById("messageForm").submit();

    event.preventDefault();
});