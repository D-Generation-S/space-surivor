# Heat Mechanic

Each component will generate heat if used. as example if an engine or weapon is fired it will produce heat.

Heat will be removed by the [cooling aggregate][cooling-aggregate] but if heat is still present at the end of the tick it will deal damage to the [hull][health-mechanic].

[cooling-aggregate]: ../components/CoolingAggregateComponent.md
[health-mechanic]: ./HullHealthMechanic.md