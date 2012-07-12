___________________________________________________________________
| 																   |
| __________      __________          .___  .___.__                |
| \______   \ ____\______   \__ __  __| _/__| _/|__| ____   ______ |
|  |       _//  _ \|    |  _/  |  \/ __ |/ __ | |  |/ __ \ /  ___/ |
|  |    |   (  <_> )    |   \  |  / /_/ / /_/ | |  \  ___/ \___ \  |
|  |____|_  /\____/|______  /____/\____ \____ | |__|\___  >____  > |
|        \/              \/           \/    \/         \/     \/   |
| _________________________________________________________________|
		
					 Game Programming 2012
			
					-- Development Team --
						Eeva Erkko
						Xi Wang
						Christopher Sierigk
						Ludwig Ringler
						Thomas Weber

-- 1. STORY --

Bud, Budi and Bro are the biggest friends in the whole robot world. They
stick together like no other robot. But unfortunately they lost Bro, the
head of the three buddies! Immediately Bud with Budi on top go on the
epic adventure to find their friend...

-- 2. GAMEPLAY --

The goal of each level is to reach the level ending door together with
Bud (the lower part) and Budi (the upper part).
Bud and Budi are able to seperate and recombine together. This means,
that the robots have two different states they can be. In each state they
 have different abbilities, which will help you to solve the level:

	Combined State:
		o walk
		o jump
		o shoot Budi up in the air
		o climb ladders
		o use levers
		o push and pull crates
		o move big crates
		o build bridge (with head)
		
	Seperated State:
		o Bud (lower part):
			* walk
			* jump higher than in the combined state
			* push small crates
			
		o Budi (uper part):
			* climb pipes
			* use levers
			* build bridge (with head)

Notice, that Budi can't walk alone. This means, you can move Budi only
combined with Bud. If you get stuck in a level you can always rewind
to an earlier state of the level (see controls).


-- 3. CONTROLS --

	Keyboard:
		o 'arrow'-keys: movement
		o 'space'-key: jump
		o 'x'-key:     seperate / recombine robots and stop pipe climbing
		o 's'-key:     switch between Bud and Budi (if seperated)
		o 'a'-key:     move crates and use levers
		o 'r'-key:     rewind
		o 'esc':       open menu
	
	X-Box Controller:
		o left analog-stick or left digital-pad: movement
		o 'A'-Button:     jump
		o 'Y'-Button:     seperate / recombine robots and stop pipe climbing
		o 'LB'-Button:    switch between Bud and Budi (if seperated)
		o 'X'-Button:     move crates and use levers
		o 'LT'-Trigger':  rewind
		o 'start'-Button: open menu

-- 4. OTHER --

If you are interested, you can see the history of our progress,
design documents and protocols in our wiki:
https://github.com/luddchen/TUGameDev2012/wiki

Thanks to Thomas Bedenk and Johannes Giering for their nice and
helpful criticism and hints during the development.