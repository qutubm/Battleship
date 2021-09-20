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
  
1) Board: This component is state machine of the game. It encapsulates a data structure respresentation of the board with all persists of the game data (ship placement, hits made by the opponent etc). This component is passive meaning it doesnt have any action methods.
  
2) Game Facade : The facade acts as an interface of the game between the UI and the game engine. It hosts the GameAssistant (two instances, one for each player), which is the driver of the game. This facade is intended to be modified based on the technology stack that the game will be run on. For eg. in case the game engine is hosted as a WEB API, the facade will take the shape of the web api controller. Due to this role, the class is stateless and hosts the engine of the game.
  
3) Game Assistant : This component is intended to be a player proxy in the game. It has access to the board of a specific player and all the actions on the board pass through this component. The GameAssistant also takes up the role to decide if a player has lost the game.
  
4) Command Actions : All actions on the board have been conceptualized as objects. The game has structurally designed action viz., Placing Ships, Hitting a cell on the board etc. 
  
  
A Generic Game workflow :  Although the game has a lot of moving components, it has a common template to follow when performing action on the game. The template can be outlined as below:
	1) The UI issues a request to the Facade based on the bootstrap requirements or request from the user. It also passed the profile of the player for whom the request is to be executed.
	2) The Facade accepts the request and creates a action object based on the request by user. It chooses a GameAssistant mapped to the profile and passed the action to the GameAssistant
	3) The GameAssistant injects the board into the action object and asks the action to do its job. 
  
  
