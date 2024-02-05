# Battery

This component can take a certain amount of [energy][power-mechanic] per tick from the [generator][generator-component]. It will store this energy internally. While taking energy to store this component will generate some heat. As soon as a battery is full it will stop loading.

If the components of your ship require more energy than the generator does provide energy will be drain from the battery. The battery does only provide a certain value of power per tick.

You can install more than one battery on your ship if there are enough slots useable.

[generator-component]: ./GeneratorComponent.md
[power-mechanic]: ../mechanics/PowerMechanic.md