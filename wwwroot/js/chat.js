"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

let userLeftByButton = false;

connection.on("ReceiveMessage", function (user, timeStamp, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.innerHTML = `${user} ${timeStamp} ${message}`;
});

connection.on("Send", function (message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.innerHTML = `${message}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;

    var chatroomName = document.getElementById("chatroomName").value;

    connection.invoke("JoinRoom", chatroomName).catch(function (err) {
        return console.error(err.toString());
    });

}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {

    var chatroomName = document.getElementById("chatroomName").value;
    var userId = document.getElementById("userId").value;
    var messageContentElement = document.getElementById('messageContent');
    var messageContent = tinymce.get('messageContent').getContent();

    //Just text
    //tinymce.activeEditor.getContent({ format: 'text' });
    if (messageContent.trim() !== "") { // Check if message content is not empty or just whitespace
        connection.invoke("SendMessageToGroup", chatroomName, userId, messageContent).catch(function (err) {
            return console.error(err.toString());
        });

        //clear message
        tinyMCE.activeEditor.setContent('');

        event.preventDefault();
    } else {
        console.error("blank message")
        //add red outline to message field
        //maybe add error message
    }
});

document.getElementById("leaveButton").addEventListener("click", function (event) {
    var chatroomName = document.getElementById("chatroomName").value;
    connection.invoke("LeaveRoom", chatroomName).catch(function (err) {
        return console.error(err.toString());
    });

    userLeftByButton = true;
});

// Detect when the user is navigating away from the page
window.addEventListener("beforeunload", function (event) {
    // Invoke the LeaveRoom method
    var chatroomName = document.getElementById("chatroomName").value;

    if (!userLeftByButton) {
        connection.invoke("LeaveRoom", chatroomName).catch(function (err) {
            console.error(err.toString());
        });
    }
   
});

