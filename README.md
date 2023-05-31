# Message Announcer
A simple Message Announcer plugin for OpenMod.

## Features:
  - Welcome messages (MOTD)
  - Broadcasts (messages every X seconds)
  - Placeholder support ({ONLINEPLAYERS}, {SERVERNAME}, etc)
  - Colored messages support

## Example configuration:
```
seconds_between_messages: 30
motd:
  - text: "Welcome to {SERVERNAME}! How are you doing today?"
    color: "#FFFFFF"
  - text: "There are {ONLINEPLAYERS}/{MAXPLAYERS} players."
    color: "#FFFFFF"
  - text: "Have fun!"
    color: "#FFFFFF"
broadcasts:
  - text: "Welcome to {SERVERNAME}!"
    color: "#FFFFFF"
    imageURL: "https://static.wikia.nocookie.net/logopedia/images/4/4e/Unturned_%28Icon%29.jpg"
  - text: "Join our discord! /discord"
    color: "#FFFFFF"
```

Forked from [Charterino/ChatAnnoyer](https://github.com/Charterino/ChatAnnoyer)
