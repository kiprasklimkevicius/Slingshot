
# Slingshot

Sling shot is a game about using the gravtational pull of black holes to get yourself to go faster.

## Story

You are an Intergalactic mail-man who has been tasked to deliver news of an alien attack on Earth. Sadly, at the worst possible time, your fuel injectors have worn out, which made them highly inefficient in burning fuel - pretty much your only way to speed up is to use the gravitational pull of black holes around you. Will you make it to Earth in time to warn them of the attack?

In this game you will be scored based on your time and maximum speed. Difficulty levels affect the amount of space-debree around - the more you shoot the more fuel you can collect, see how fast you can get, and save Earth!

## Player Actions

There are 3 (4) actions that a player can make:
Shooting - Spacebar: this shoots out a 'laser' beam that can destroy asteroids that are coming the players way.

Lateral Movement - Left and Right Arrows: by tapping either arrow the player gains speed in the respective direction. Holding the button down does nothing extra.

Boost - Left-Shift: This button is the 'boost' button. While the player has fuel and holds down the button, they gain momentum along the z-axis. Resulting in a larger velocity vector. 

## Interactions

### Black Hole

This is probably the main mechanic of the game. The black holes have a gravitational pull field around them. As long as the player is inside this field they will get force applied to them. The force is in the direction of the black hole. This is an important part of the gameplay since the player can get really fast if they use this mechanic well. If it is not used well, it can harm the experience and make the game feel unfair. However it's all in how the player executes their movements around the black hole.

How a player should move with the black hole is described in the tutorial.

If the Player collides with the Black Hole - they die.

### Asteroid

An asteroid is a small object that keeps bouncing around the track. If the player hits the asteroid they die.

The player is able to shoot the asteroids down. When their shots connect the asteroid evaporates leaving a jerry can behind.

### Jerry Can

This is a pick-up-able item. It gives the player extra fuel. Which can be used for the Boost action.

### Earth

This is the end-goal of the game. Once the player reaches Earth, the level is succesfully complete. 

## UI and Effects

### UI

The player is greeted with a small scrolling text describing the story of the game. They can skip this text.

After the description, the title screen appears letting the player to choose what they want to play. The choices are the following:
Tutorial - a tutorial level, where the controls and the basic tactics are described for the player.
Easy, Medium, Hard - By choosing either of these, the player chooses to play the actual game with the selected difficulty. The only difference between the difficulties is the amount of asteroids spawned.

### Effects

#### Visual

There are only 2 visual effects. One is when the player shoots down a meteor, a puff of smoke is produced via the particle system. The second is the tutorial guide capsule having air escaping, when it uses Lateral Movement to indicate when and how should the movement be used.

#### Audio

The audio effects are a little wider. First there is the background music that creates the atmosphere of the game.
Second, there is the constant engine sound in the background. This engine sound changes when the Boost is being used. To indicate that the boost is indeed being used. 
Third, the crash sound. This sound is used to indicate that the player has crashed and perished.
Fourth, the sound played upon a Jerry-can pick-up. Its used to indicate the pick-up.

## Last thoughts

The player should be encouraged to go as fast as possible, but from my user-testing, the players seem to think that to beat the game they can just cruise and avoid the harsh black holes. Outsanding design flaw. I am trying to add timer to give players a sense of urgency.

Overall, very fun project to work on. I feel like it's a never ending story - in the developmental sense. I feel I could keep working on it for a while, but I guess thats partly why we have a deadline haha. Thank you for this module, was very fun, I hope to continue like this in the future.


