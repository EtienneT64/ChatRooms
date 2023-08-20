"use strict";

const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
let userLeftByButton = false;

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.start().then(() => {
    document.getElementById("sendButton").disabled = false;

    const chatroomName = document.getElementById("chatroomName").value;

    connection.invoke("JoinRoom", chatroomName).catch((err) => {
        return console.error(err.toString());
    });

}).catch((err) => {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", (event) => {

    const chatroomName = document.getElementById("chatroomName").value;
    const userId = document.getElementById("userId").value;
    const messageContentElement = document.getElementById('messageContent');
    const messageContent = tinymce.get('messageContent').getContent();

    //tinymce.activeEditor.getContent({ format: 'text' });
    if (messageContent.trim() !== "") { // Check if message content is not empty or just whitespace
        connection.invoke("SendMessageToGroup", chatroomName, userId, messageContent).catch((err) => {
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

connection.on("ReceiveMessage", (user, timeStamp, message) => {
    const li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.innerHTML = `${user} ${timeStamp} ${message}`;
});

connection.on("Send", (message) => {
    const li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.innerHTML = `${message}`;
});

document.getElementById("leaveButton").addEventListener("click",(event) => {
    const chatroomName = document.getElementById("chatroomName").value;
    connection.invoke("LeaveRoom", chatroomName).catch((err) => {
        return console.error(err.toString());
    });

    userLeftByButton = true;
});

// Detect when the user is navigating away from the page
window.addEventListener("beforeunload", (event) => {
    // Invoke the LeaveRoom method
    const chatroomName = document.getElementById("chatroomName").value;

    if (!userLeftByButton) {
        connection.invoke("LeaveRoom", chatroomName).catch((err) => {
            console.error(err.toString());
        });
    }
   
});

