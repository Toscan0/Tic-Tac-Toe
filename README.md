# Tic-Tac-Toe

## Requirments

	* Unity 2020.2.7f1
	* Android

## Some Considerations
	
	* The target resolution is: 640 x 960

	* In the easy level, the npc selects a random position
		* The code is prepared to use the an "inversed minimax" where we would pick always the best choice for the player, but this was basically the npc plays the first empty cell, and didn't feel "real".
	* In the medium level, I use a random to choose between random and minimax. The randomness has a 70% probability of choosing minimax. So, it is more medium-hard than medium-easy.
	* In the hard level is the minimax algorithm, with a few optimizations for the first defensive / attacking move 

	* I didn't pay much attention to the aspect of the game, but I did add a few sounds (win, draw, lost and music background). For the line that indicates where was the match, I use animations. However, my animation could be done with a prefab creation I believe this the best way in terms of scalability.

	* For the scoreboard I use playerPrefs