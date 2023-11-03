# VR Arki Academy
Developed as my final project at Vancouver Film School in the VR/AR Design & Development program.
- Tools: Unity Engine, C#
- Project Type: VR
- Started: May 2022
- Finished: August 2022
## About
Design and Build your dream house in VR.
## Idea
![storyboard picture](https://elaineserrano.com/assets/images/FirstApproach.jpg)
## User Journey
The idea kept evolving over time, to end with a tool that can be use intuitively for every person without a background in design.
1. The user starts inside a studio in front of a desk, with the main menu above.
![User Journey 1](https://elaineserrano.com/assets/images/UJ_1.jpg)
2. First look of the Tools Menu
![User Journey 2](https://elaineserrano.com/assets/images/UJ_2.jpg)
3. Build tab, divided in three sections: Floor, Walls, and Furniture.
![User Journey 3](https://elaineserrano.com/assets/images/UJ_3.jpg)
4. Floor. Change size by selecting and moving the arrow handles.
![User Journey 4](https://elaineserrano.com/assets/images/UJ_4.jpg)
5. Walls Page. It displays an inventory of walls.
![User Journey 5](https://elaineserrano.com/assets/images/UJ_6.jpg)
6. Furniture Page. It display a furniture inventory to decorate the room.
![User Journey 6](https://elaineserrano.com/assets/images/UJ_7.jpg)
7. The user can move around the house they are building.
![User Journey 7](https://elaineserrano.com/assets/images/UJ_8.jpg)

## Development
### Main Functionality
The user starts in an empty level with only a plane as the ground where they can build their house. 

1. Select a block on the menu and place it on the grid.
2. Verify that the position the player wants to place that Block is not already occupied by another object. 
3. To give the user a visual representation, the material changes to red wherever the selected Block overlaps with another collider component.

### Edition
Apart from placing an object, the user can also edit the Blocks they have placed on the grid.

![Edition](https://elaineserrano.com/assets/images/RightController.jpg)

- In the right controller, there is one icon that represents the current tool in use. By pressing the grip button on the right controller, they can change tools. The tools are named Select, Build, Transform, Edit, Delete, and, Explore. 
- To delete objects, press the grip button until the delete icon appears. Then, select an object to delete it. At this moment, there is not an undo button, so all changes are permanent. 
- To move one object to another place, press the grip button until the transform button appears. Then, select an object and place it somewhere else.

### Locomotion
The locomotion system implemented here is Teleportation. In this case I limited the movement input to the left controller to set space for other functionality in the right controller. In this part I modified the XR Interaction Toolkit to get the teleportation and view rotation to work together with only the left thumbstick.
