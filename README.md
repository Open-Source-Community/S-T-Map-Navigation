
# Map-Navigator

![Screenshot 2025-04-18 224425](https://github.com/user-attachments/assets/1e51eb83-f426-4bb0-bfdb-b079db49c01f)


Map Navigator is a C# Windows Forms application that helps users find the shortest or fastest path between two points on a custom map. Users can either draw their own maps using the built-in Map Drawer or upload existing map images that meet the specified requirements.


## Features
- ***Map Creation Tools:***

    - Draw custom maps with roads, intersections, and landmarks

    - Define Traffic levels with the different colors

    - Save and load custom map files

- ***Pathfinding:***
    - Shortest path (least distance)

    - Fastest route (considering speed limits)

    - visual path display

    - Dijkstra's/A* hybrid algorithm
## Requirements

### Map Image Requirements (for uploaded maps):

- PNG, JPEG, or JPG format

- 1280×720 resolution

- Color Indexed Image Specification

    - ***Required Colors (Indexed Palette):***
  
     | Rgb| Meaning |
     |----------|----------|
     |255 , 0 , 0 | Very Busy|
     |238 , 232 , 232| Place|
     |189 , 198 , 197| Regular Path|
     |162 , 193 , 221| Mildly busy|
     |255 , 255 , 255| Obstacle|
     |255 , 0 , 128 |Exit/Enter points|

    - ***Use this file to convert to indexed image***
      
     res/Assets/custom_path_colors.act
     




## Run Locally

 1. Clone Repository

        git clone https://github.com/Open-Source-Community/S-T-Map-Navigation.git


2. Open 'Map Creation Tool.sln' 

3. Run

## Troubleshooting  

| Problem | Solution |
|---------|----------|
| Can't find a path | Check all roads are connected |
| Program runs slow | Try a smaller map |
| Map won't import | Make sure the image meets requirements |





    
