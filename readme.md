aStrategyGame is a demo game project which has 3 main features: Building Production, Unit Production, Unit Movement

Building Production:
You can create buildings in the game map by using production (left) menu.

Unit Production:
You can create units in the game map by using production (right) menu.
For that, you need to select a building that has units. 
Right click on the game map to change its spawn point.

Unit Movement:
Select a unit by left click on the game map. Then right click on the game map to move it.
Unit will find the shorthest path by using A* search algorithm.

---

Production Menu shows buildings in an infinite scroll view.
Unit Production Menu will show units if there is some prefabs in the Assets/Resources/Units/*BuildingName* folder.

Building Structure:

Building (gameObject)
|-- Sprite
|-- Collider (also there is a kinematic rigidbody and a script inherited Building class.)

Unit Structure:
Unit (gameObject) 
|-- Sprite
|-- Collider (also there is a Unit script that handles the movement of the unit)

---

Known Issues:
- Game only works on all resolutions that has 16:9 aspect ratio
- Since there is no production time for units, if unit button is smashed too many times, some soldiers may overlap each other.
- At Spawn process, you may get an error about "Can't build". It doesn't effect anything.
