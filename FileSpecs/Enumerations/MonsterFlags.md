# Monster flags

Each monster has special flags that are stored in a 8 bit value. Each bit represents a specific property of the monster.

The following table is not complete yet.

Bit | Property
----|----
0 | Undead, can be killed by holy spells
1 | **Unknown** (used by Luminor, Guard demon and gargoyle type monsters)
2 | Boss (immune to Fear, Paralyze, Petrify, DissolveVictim, Madness, Drugs, Irritation and won't flee)
3 | Spider/lizard (is set for all of the mentioned kind except for Gigantula). Moranians don't count as lizards though. :P
4..7 | Unused

Note: The boss flag also protects against holy spells if the monster is also marked as undead.

Note: It is a known bug that Reg is not marked as boss so you could cast Fear on him and he will flee with all the items. So I guess he should have bit 3 set as well. Possibly bit 1 was set for him by mistake.
