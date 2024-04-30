3902 team 6 project


We have designed three levels, where level 1 and level 2 are adventure levels, 
and level 3 is a time level, level 4 is adventure level.(we delete test room)

for MainMenu, PauseMenu, GameOver and VictoryMenu:

We created three pages, including the main page, the pause page and the Victory page. 
The main page contains three levels, and players can choose from any of the three levels. 
After entering the game, you can switch to the pause page by pressing C, the pause page includes returning to the main menu and continuing the game. 
After completing the level, the Victory screen appears, which includes a restart and return to the main menu.
If you die more than 3 times, you will enter game over menu, you can choose restart or back to mainmenu.
(tip: we use m to mute sound and type again to open)

For level 1:
We achieved collisions with enemies and blocks. Implements interaction with items and blocks. 
The map is completely created. Mario has 3 health points, and every time he collids with an enemy, he loses 1 health. 
The enemy can also attack; Each time Mario collides with a Fireball, he loses 1 health. Mario can eat coins on the map. 
There are dungeon levels, where Mario can enter the dungeon by pressing the S key while standing on the pipe, and exit the dungeon by pressing the B key at the exit.

for level 2:
Based on level 1, we added more monsters and plants, and Mario can get bigger after eating the flowers and add 1 health. 
And we added fog, which brightens the area when Mario has explored it. To make it easier, we didn't make the fog particularly dark.

In level 3:
Level 3 is a time-limited level, which states that the player must complete it by a specified time, or they will fail. 
These include very difficult fireball attacks and blocking blocks.

In level 4:
Level 4 is a muti-enemies level, which has a 5 types enemies and can have an enemy can shoot fireball.

For Block:
We added a variety of different blocks, including the mushroom block, which Mario bumps into will pop up the mushroom, 
and Mario can eat the mushroom to become bigger and gain a life. Star block: When Mario hits a block, a star will pop out, and Mario can be invincible for 10 seconds if he eats the star. 
Flower block, Mario hit the block will pop flowers, Mario eat the flower can cause a fireball.
Coin block, after Mario hited this block, it will pop a cion, Mario eat it can increase score.

For CheatCode:
The game has added cheaters, which currently include increasing the player's health through "increase health". 
Change the player's speed using "speed X"(X is integer)
Change the player's jump ability using "jumps X"(X is integer)

For Score:
we desgin a coin is 100
          a enemy is 1000
          a item is 500

For player Control:

player1:
using "W A S D" to move
using "num 1" to shoot fireball
using "left shift" to sprint

player2:
using "Arrows on the keyboard" to move
using "space" to shoot fireball
using "right shift" to sprint

keyboard "r" can reset player
keyboard "e" can damage player
