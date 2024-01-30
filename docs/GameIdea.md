# Game Idea

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

### Flight Computer (FlyByWire)

The computer will try to cancel every strafe movement if you accelerate into one direction. If you stop rotating it will try to point the ship into the direction you stopped the rotation.

If you stop moving the computer will try to stop the ship velocity.

### Upgrades

Ships will have core, internal and hardpoints slots. The slot count can vary between ships. Those components can either be replaced, added or removes.`FlyByWire`

#### Core

Components of the core category can only be replaced, the following types are present.

- Generator => The power generator of your ship. It will tell you how many components you can use.`FlyByWire`
- Cooling => A device which will remove heat from other components, overheating will damage the ship

### Internal

- Shield generator => An refillable health pool
- Traktor beam => Pull items to your ship
- Hull Reinforcement => Increase health pool

### Hardpoints

- Weapons => Something to shoot on enemies

### Heat

Each component will generate heat if used. as example if an engine or weapon is fired it will produce heat. Same goes for a charging shield generator.

Heat will be removed by the cooling unit but if heat is still present at the end of the tick it will deal damage to the hull.

### Power

Power will be defined by the generator. Each component will have a power requirement. You can only add so many components until you hit the barrier. Each additional added component will be disabled and can't be activated if there is not enough free power available.

### Health (Hull)

The health of a hull is dependent on the base hull health and any hull reinforcement's added.

### Ai

The Ai simple, it will try to collide with the player or shoot him down.

### Enemies

Enemies consist of simple fighters or asteroids flying around.

### Meta Progression

\-