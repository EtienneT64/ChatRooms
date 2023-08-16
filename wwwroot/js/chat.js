"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    //var formattedMessage = formatMessage(message); // Function to format the message
    li.innerHTML = `${user}: ${message}`;

    // Clearing code

});

//function formatMessage(message) {
//    var formattedMessage = message
//        //.replace(/\*\*(.*?)\*\*/g, '<strong>$1</strong>') // Replace **text** with <strong>text</strong>
//        //.replace(/\*(.*?)\*/g, '<em>$1</em>'); // Replace *text* with <em>text</em>
//    return formattedMessage;
//}

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;

    var chatroomId = document.getElementById("chatroomId").value;
    var userId = document.getElementById("userId").value;

    connection.invoke("JoinRoom", Number(chatroomId)).catch(function (err) {
        return console.error(err.toString());
    });

}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {

    var chatroomId = document.getElementById("chatroomId").value;
    var userId = document.getElementById("userId").value;
    var messageContentElement = document.getElementById("messageContent");
    var messageContent = tinymce.get('messageContent').getContent();
    //connection.invoke("SendMessageToGroup", chatroomName, message).catch(function (err) {
    //    return console.error(err.toString());
    //});
    if (messageContent.trim() !== "") { // Check if message content is not empty or just whitespace
        connection.invoke("SendMessageToGroup", Number(chatroomId), userId, messageContent).catch(function (err) {
            return console.error(err.toString());
        });

        // Optionally, you could also clear the input field here
        document.getElementById("messageContent").value = '';

        event.preventDefault();
    } else {
        // Show an error message to the user indicating that the message is empty
        // For example, you can create an error element and insert it into the DOM
        //var errorElement = document.createElement("p");
        //errorElement.style.color = "red";
        //errorElement.textContent = "Please enter a message before sending.";
        //document.getElementById("errorContainer").appendChild(errorElement);

        console.error("blank message")
    }
});
