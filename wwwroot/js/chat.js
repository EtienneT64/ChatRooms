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
    const messageContentText = tinymce.activeEditor.getContent({ format: 'text' });

    if (messageContentText.trim() !== "") { // Check if message content is not empty or just whitespace
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

//connection.on("ReceiveMessage", (userImage, user, timeStamp, message) => {
//    const li = document.createElement("li");
//    const avatarContainer = document.createElement("div");
//    avatarContainer.classList.add("avatar-container");
//    const avatarImg = document.createElement("img");
//    avatarImg.src = userImage;
//    avatarImg.alt = "User Profile Picture";
//    avatarContainer.appendChild(avatarImg);
//    li.appendChild(avatarContainer);

//    const messageContentDiv = document.createElement("div");
//    messageContentDiv.classList.add("message-content");
//    messageContentDiv.innerHTML = `${user} ${timeStamp} ${message}`;
//    li.appendChild(messageContentDiv);

//    document.getElementById("messagesList").appendChild(li);
//});

connection.on("ReceiveMessage", (userImage, user, timeStamp, message) => {
    const li = document.createElement("li");
    const avatarContainer = document.createElement("div");
    avatarContainer.classList.add("avatar-container");
    const avatarImg = document.createElement("img");
    avatarImg.src = userImage;
    avatarImg.alt = "User Profile Picture";
    avatarContainer.appendChild(avatarImg);
    li.appendChild(avatarContainer);

    const messageContentDiv = document.createElement("div");
    messageContentDiv.classList.add("message-content");

    // Create separate elements for username, timestamp, and message
    const userNameElement = document.createElement("span");
    userNameElement.classList.add("user-name");
    userNameElement.innerHTML = user;

    const timeStampElement = document.createElement("span");
    timeStampElement.classList.add("time-stamp");
    timeStampElement.innerHTML = timeStamp;

    const messageElement = document.createElement("div");
    messageElement.classList.add("user-message");
    messageElement.innerHTML = message;

    // Append elements to the message content div
    messageContentDiv.appendChild(userNameElement);
    messageContentDiv.appendChild(document.createTextNode(" ")); // Add space between username and timestamp
    messageContentDiv.appendChild(timeStampElement);
    messageContentDiv.appendChild(document.createElement("br")); // Line break
    messageContentDiv.appendChild(messageElement);

    li.appendChild(messageContentDiv);

    document.getElementById("messagesList").appendChild(li);
});


//connection.on("ReceiveSystemMessage", (userImageUrl, userName, joinMessage, timeStamp) => {
//    const li = document.createElement("li");
//    document.getElementById("messagesList").appendChild(li);
//    li.innerHTML = `${joinMessage}`;
//});

connection.on("ReceiveSystemMessage", (userImage, user, message, timeStamp) => {
    const li = document.createElement("li");
    li.classList.add("system-message"); // Add a class for styling system messages

    const systemMessageDiv = document.createElement("div");
    systemMessageDiv.classList.add("system-message-content");

    const avatarContainer = document.createElement("div");
    avatarContainer.classList.add("avatar-container");
    const avatarImg = document.createElement("img");
    avatarImg.src = userImage;
    avatarImg.alt = "User Profile Picture";
    avatarContainer.appendChild(avatarImg);
    systemMessageDiv.appendChild(avatarContainer);

    const contentContainer = document.createElement("div");
    contentContainer.classList.add("content-container");

    const userNameElement = document.createElement("span");
    userNameElement.classList.add("user-name");
    userNameElement.innerHTML = user;

    const messageElement = document.createElement("span");
    messageElement.classList.add("system-message-text");
    messageElement.innerHTML = message;

    const timeStampElement = document.createElement("span");
    timeStampElement.classList.add("time-stamp");
    timeStampElement.innerHTML = timeStamp;

    contentContainer.appendChild(userNameElement);
    contentContainer.appendChild(document.createTextNode(" "));
    contentContainer.appendChild(messageElement);
    contentContainer.appendChild(document.createTextNode(" "));
    contentContainer.appendChild(timeStampElement);

    systemMessageDiv.appendChild(contentContainer);

    li.appendChild(systemMessageDiv);

    document.getElementById("messagesList").appendChild(li);
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

