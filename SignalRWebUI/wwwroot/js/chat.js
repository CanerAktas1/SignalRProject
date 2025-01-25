var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7074/SignalRHub").build();
document.getElementById("sendButton").disabled = true;
connection.on("ReceiveMessage", (user, message) => {
    var currentTime = new Date();
    var currentHour = currentTime.getHours();
    var currentMinutes = currentTime.getMinutes();
    var li = document.createElement("li");
    var span = document.createElement("span");
    span.style.fontWeight = "bold";
    span.textContent = user;
    li.appendChild(span);
    li.innerHTML +=`: ${message} - ${currentHour}:${currentMinutes}`;
    document.getElementById("messageList").appendChild(li);
});

connection.start().then(() => {
    document.getElementById("sendButton").disabled = false;
}).catch((err) => { console.log(err.toString()); });

document.getElementById("sendButton").addEventListener("click", (event) => {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch((err) => { console.log(err.toString()); });
    event.preventDefault();
});