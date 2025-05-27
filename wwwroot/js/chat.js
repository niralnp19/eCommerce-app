"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message, datetime) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    var ul = document.createElement("ul");
    ul.className = "messageBubble";
    li.appendChild(ul);
    var li1 = document.createElement("li");
    li1.className = "msgName";
    var li2 = document.createElement("li");
    li2.className = "sentMsg";
    var li3 = document.createElement("li");
    li3.className = "msgTime";
    ul.appendChild(li1);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li1.textContent = `${user}`;
    ul.appendChild(li2);
    li2.textContent = `${message}`
    ul.appendChild(li3);
    li3.textContent = `${datetime}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var dt = new Date();
    const datetime = dt.toLocaleString();
    console.log(datetime);
    connection.invoke("SendMessage", user, message, datetime).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});