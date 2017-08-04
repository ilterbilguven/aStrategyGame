aStrategyGame is a demo game project which has 3 main features: Building Production, Unit Production, Unit Movement

Building Production:
You can create buildings in the game map by using production (left) menu.

Unit Production:
You can create units in the game map by using production (right) menu.
For that, you need to select a building that has units. 
Then, right click on the game map to assign a spawn point.
After that, you can create units.

Unit Movement:
Select a unit by left click on the game map. Then right click on the game map to move it.
Unit will find the shorthest path by using A* search algorithm.

---

Production Menu gets the prefabs in the Assets/Resources/Buildings folder and shows them.
Unit Production Menu will show units if there is some prefabs in the Assets/Resources/Units/*BuildingName* folder.

Building Structure:

Building (gameObject)
|-- Sprite
|-- Collider (also there is a kinematic rigidbody and a script inherited Building class.)

Unit Structure:
Unit (gameObject) [also there is a MoveUnit script that handles the movement of the unit]
|-- Sprite
|-- Collider

---

Known Issues:
- Design patterns are poorly implemented.
- Aspect ratio is 16:9, and the grid is 32*27
- Spawn point is not shown visually.