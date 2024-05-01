# Battleship
Implementation of classic battleship game

## Issues
- To make the enemy smarter, the attacks you perform will be random until you have hit a target, from that moment, you must proceed to attack the adjacent cells. Once the target is sunk, continue attacking randomly.
- Somehow, detect that the ship has been sunk (i.e. an attack has been launched on all cells), change the status from X to D (X means hit and D means Destroyed).
- The code has a high dependency on the specific implementation within the `Main` method. This makes it difficult to modify or scale the program.
- Repeated use of direct manipulations can make the code difficult to read and maintain.
