
[Pickup Blocks by scaN] 

*This mod was made with the assistance of the free (web) versions of ChatGPT, DeepSeek, and Claude.


 TABLE OF CONTENTS
===================

1. Mod Information
   1.1 Usage
   1.2 Configuration
     
2. General Information  
   2.1 Requirements
   2.2 Installation
   
3. Links - Bug Reports


====================================================================================================

1. MOD INFORMATION

This mod allows you to use the "Pick Up" action that is enabled on certain blocks when a Land Claim 
Block is placed, without requiring one. This allows you to use this feature in single-player.

The mod also allows you to configure the time required to pick up the block, including for three blocks 
that had their pickup time hardcoded. The values used by the mod are the game's default values.


----------------------------------------------------------------------------------------------------

1.1 USAGE

- For blocks that have some kind of interface, you need to hold the (E) key.

- For blocks that do not have an interface, you only need to press the (E) key once.

- For the "ceilingLight01_player" block, which is a light bulb, you need to get close to it and 
hold the (E) key. For some reason, this block does not display its tooltip (it also doesn't do 
so in vanilla; this is not caused by the mod).


----------------------------------------------------------------------------------------------------

1.2 Configuration

To configure it, simply go to the mod's folder, open the `Config` folder, then open the `blocks.xml` 
file and change the following value: `@value">15<` to the amount of time you want, in seconds.

Example:

	<!-- Default = 15 seconds -->
	<set xpath="/blocks/block[@name='cementMixer']/property[@name='TakeDelay']/@value">15</set>
	
	<!-- Changed to 5 seconds -->
	<set xpath="/blocks/block[@name='cementMixer']/property[@name='TakeDelay']/@value">5</set>


====================================================================================================

2. GENERAL INFORMATION

- A new savegame is NOT NEEDED.

- The mod was developed and tested in single-player on game version 3.2.0 (b10) on Windows 10.


----------------------------------------------------------------------------------------------------

2.1 REQUIREMENTS

- Easy AntiCheat disabled.

- Game version: Should work in any 3.X version.


----------------------------------------------------------------------------------------------------

2.2 INSTALLATION

1) Make a backup of your game save file.

2) Unzip the downloaded file and put the folder "PickupBlocks" inside your "mods" folder.


====================================================================================================

3. LINKS - BUG REPORTS


# Links:

- NexusMods profile: https://www.nexusmods.com/profile/scaN7dtd

- 7daystodiemods profile: https://7daystodiemods.com/profiles/scan

- Github: https://github.com/scaN-7dtd


# Bug Reports: use the dedicated bug report section on the respective mod page or dm me 
in discord: scan88


====================================================================================================