var tmi = require("tmi.js");
var starts_with = require("starts-with")

var options = {
    options: {
        debug: true
    },
    connection: {
        reconnect: true
    },
    identity: {
        username: "GameBotFor",
        password: "oauth:cxg65te3f25egkpq95h3xuwx6socce"
    },
    channels: ["tirect"]
};

var client = new tmi.client(options);

// Connect the client to the server..
client.connect();

client.on("chat", function (channel, userstate, message, self) {
    if(self) return;
    if(message === "!join") {
        client.action("tirect", `Hello my dear, ${userstate.username}!`)
    }
    if(message === "!print") {
        client.action("tirect", `${userstate.username}, чего тебе напечатать?`);
    }
    if(starts_with(message, "!p")) {
        client.action("tirect", message.substring(3));        
    }
});