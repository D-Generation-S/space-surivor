# Power Mechanic

This document will describe how power will be generated and used.

Power will be defined by the generator. Each active component will have a power requirement.

The generator will try to power those components based on there priority. If the generator does not produce enough electricity to keep an component running it will try to get some additional energy from your [battery][battery-component] banks, if any installed. IF there is still not enough electricity or your battery can't output enough the component will be disabled for this tick.

If the generator does produce more power than all active componentes combined can consume it will try store this inside of your [battery][battery-component] banks, if installed. Since batteries are limited in the amount of power the can consume every additional electricity will be gone.

[battery-component]: ../components/BatteryComponent.md