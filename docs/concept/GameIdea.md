# Game Idea

- TOC
{:toc}

## Story

You got an important document about and enemy outpost. You enter the jump to deliver the information but somehow it got interrupted and enemies try to hunt you down. You need to survive this until the drive is back online and you can jump again.

## Game loop

### Initial

If you start your first round you will get a small text description about your situation, followed by the jump.

### Continued

You enter the game with an animation of the interrupted jump. You need to defend yourself until the jump drive is back online. Waves of enemies will try to hunt you down while doing so. Killing enemies might give you new parts to install into your ship.

## Mechanics

### Movement

The movement of the ships is based on a simplified newtonian physic model. If you accelerate into one direction your travel will continue. Same goes for any rotation.

To help the player navigating around the game will allow you to use a `FlyByWire` system. This will try to cancel any unwanted movement to any direction.

For more information check the game mechanic section of this document or go to the [Flight Computer][flight-computer] documentation.

### Upgrades

Ships will have core, internal and hardpoint slots. The slot count can vary between ships. Those components can either be replaced, added or removes.`FlyByWire`

### Components

Check the [component][component-overview] list to see all the implemented or planned components.

### Game Mechanics

For more information check the [Game Mechanic Overview][game-mechanic-overview]

### Ai

The Ai simple, it will try to collide with the player or shoot him down.

### Enemies

Enemies consist of simple fighters or asteroids flying around.

### Meta Progression

\-

[component-overview]: ../components/ComponentOverview.md
[game-mechanic-overview]: ../mechanics/GameMechanicOverview.md