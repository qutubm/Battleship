# Battleship
Game Instructions:
1) As the game starts profiles for 2 players gets created and Board is set with size 10 X 10.
2) On the board rows are Alhpabets (i.e. A, B, C...) and columns are numbers (i.e. 1, 2, 3 ...).
3) Co-ordinates are to be input in <Row>-<Column> fashion i.e. "A-1", "D-5", "G-10" etc.
4) Each player is given 3 ships to place on the board i.e. Carrier (length 5), Destroyer (length 3) and Submarine (length 2)
5) Placement of ships accepts an Offset Co-ordinate (i.e. "A-3", "C-1" etc) and alignment (i.e. 1 for Horizontal and 2 for Vertical).
6) After the ships are placed on the board each player is allowed to put a Hit in the form of Co-ordinate on the opponent's board.
7) The game continues until any one player's all ships gets capsized.
  
Snapshots:
![Snapshot1](https://user-images.githubusercontent.com/23535596/133957052-7dd07b07-11e3-49d2-a8bb-60858a30e791.PNG)
![Snapshot2](https://user-images.githubusercontent.com/23535596/133957055-2d61267a-2d2e-4f36-9f6c-e16267cdcdbf.PNG)
![Snapshot3](https://user-images.githubusercontent.com/23535596/133957057-427c530b-0797-4565-88dd-06c8d2cace01.PNG)

Component Design:
  
1) Board: This is just a class which contains the state of the game for its reepective players.
  
2) Game Facade : This is the interface of the Battleship game and contains methods for setting up profiles, setting up ship locations, attacking hits and to find out whether a winner has emerged. Game Facde is stateless.
  
3) Game Assistant : This component is responsible for setting up the Board for the players and channelising the commands coming from Game Facade. This component determines the winner.
  
4) Command : This component is a set of classes which executes all the instructions coming from Game Facade and it directly if affects the state of the Board.
  
Pseudo Code for the Core logic of the game:
  
  
