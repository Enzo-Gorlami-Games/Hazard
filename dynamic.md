# Game's Dynamic Components

### Game Objects and Their Numeric values
---
In Hazard, game objects with numeric dynamic values could be:  
1. The number of hazardous objects in a level
2. Time given to complete the level
3. Number of puzzles the player has to solve in a level
4. Number of rooms the player could interact with

Since we are dealing with a children's puzzle game and all the values of above components are highly dependent on one another, I figured it would be difficult to predict the players' performance. Hence, I've come up with a system to determine the numeric values, based on **play tests**.

While play testing, the following parameters should be tracked:

![H_i](https://latex.codecogs.com/svg.image?H_i&space;) - Number of hazardous objects in level *i*  

![T_i](https://latex.codecogs.com/svg.image?T_i&space;) - Time restriction in level *i* (in seconds)

![P_i](https://latex.codecogs.com/svg.image?P_i&space;) - Number of puzzles the player has to solve in level *i*

![R_i](https://latex.codecogs.com/svg.image?R_i&space;) - Number of rooms the player could interact with in level *i*

We could define ![D_i](https://latex.codecogs.com/svg.image?D_i&space;) as the difficulty of level *i* as follows:

![Difficulty equation](https://latex.codecogs.com/svg.image?\frac{H_i}{T_i}*P_i)


