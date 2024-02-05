# Flight computer

The flight computer is a special component which introduce different modes to control the ship. Those modes will change the way the ship will react to input. Right now there are two modes available.

## Modes

### Raw Input mode

In the rwa input mode each input given to the ship computer will be forwarded directly to the thrusters. This will give you full control over all the different movements but you will need to counterinteract everything on your own.

### Fly by wire

This flight mode will try to keep your ship on route. It will introduce some strafe thrust to keep your ship flight vector aligned with it's front.

If you stop making rotational or translational input the ship will try to stop as soon as possible.


### Add a mode

To add a note please got to the [developer section][flight-computer-as-developder] of this documentation. 

[flight-computer-as-developder]: ../developer/FlightComputerAsDeveloper.md