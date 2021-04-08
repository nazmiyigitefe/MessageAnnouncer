# ChatAnnoyer
Annoy the chat for your players (It is beneficial for you)

Features:
  - MOTD messages (sent when a user connects)
  - Broadcasts (messages every X seconds)
  - Placeholder support ({ONLINEPLAYERS}, {SERVERNAME}, etc)
  - Coloured messages support

Example configuration:
```
seconds_between_messages: 30
motd:
  - text: "Welcome to {SERVERNAME}! How are you doing today?"
    colour: "#FFFFFF"
  - text: "There are {ONLINEPLAYERS}/{MAXPLAYERS} players."
    colour: "#FFFFFF"
  - text: "Have fun!"
    colour: "#FFFFFF"
broadcasts:
  - text: "Welcome to {SERVERNAME}!"
    colour: "#FFFFFF"
    image_url: "http://stockimages.com/randompic.png"
  - text: "Join our discord! /discord"
    colour: "#FFFFFF"
```
