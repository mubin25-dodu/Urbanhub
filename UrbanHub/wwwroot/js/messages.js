const chatBody = document.getElementById('chatBody');
const msgInput = document.getElementById('msgInput');

function sendMessage() {
    const text = msgInput.value.trim();
    if (text === "") return;

    const bubble = document.createElement('div');
    bubble.className = 'bubble-sent';
    bubble.innerHTML = text;

    chatBody.appendChild(bubble);

    msgInput.value = "";
    chatBody.scrollTop = chatBody.scrollHeight;
}

msgInput.addEventListener("keypress", function (event) {
    if (event.key === "Enter") {
        sendMessage();
    }
});