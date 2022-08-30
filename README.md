======================================IMPORTANT=================================

If you used version 3.0 to shuffle your cities, please use that version to restore your default cities, or do so manually before using any newer versions. If you do not, more recent versions will not be able to automatically restore your city files to default.

# SimCopterRandomizer
Replaces the tweak files with randomized values. Includes a reset option to return everything back to default values.

Place the executable in the SIMCOPTER\tweak folder, and then use the buttons to choose which aspects of the game to randomize.

Career randomizes the settings of every city when playing a Career Game. These settings include the difficulty and frequency of missions, how many points you require to finish a city, and how much money you receive when completing the city.

Missions randomizes the settings of missions in every game mode. These settings include money and point rewards for completing missions; including catching speeders and shooting down UFOs, point and money penalties for failing missions, and timers for some of the time-sensitive missions.

Helicopters randomizes the settings of helicopters in every game mode. The helicopter specific settings include movement speeds, purchase price, repair and refuel costs, how much damage the helicopter can take, and how much fuel the helicopter can hold. Settings that affect all helicopters include acceptable landing conditions, descent speed, rescue harness and bambi bucket rope physics, bambi bucket fill and empty speed, water cannon distance and recoil force, height ranges which fire does damage, helicopter value depreciation, and distance maintenance cost multipliers.

Fire randomizes the remaining aspects of fires. These settings include dousing progress rewards, lifetime of fires, speed and likelihood of fires spreading, and general size of individual fires.

AutoMissions randomizes Fire Scan and Road Scan values. What these do is currently unknown, and if you have any insights, please let me know so that this document may be updated.

Customized mode allows for customization of each individual item in each tweak file within the ranges originally defined in the files. Any items which are checked off will be skipped and remain unchanged.

The optional seed value can be any string. If you want a random seed, leave the box blank.

=======================Career City Shuffling=============================

New tab to shuffle the order of Career Cities. Also makes the corresponding flyover Smacker videos follow. The Seed box can be used for the shuffler as well.

The names on the city selection menu during the career will not change to reflect the different cities.

If you wish to replace the files manually, first navigate to SIMCOPTER\smk and delete the files with the names "cityX_b.smk" and "cityX_s.smk" the originals will be in a hidden folder named "Backup" in the same location.
Then navigate to SIMCOPTER\cities\career and delete all of the cities, the original files will be in a hidden folder named "Backup" at that location.

Please delete the Backup folders after restoring your files.

=======================Sound Shuffling=============================

Similarly to the career city shuffling, the sound effects within the game can now be shuffled at random.

This includes sounds that are unused by default. Sounds may be used at triggers not appropriate to their length. Your enjoyment may vary.

The "Vehicle Sounds" selection includes helicopter, car and menu related sounds.

The "People Sounds" selection includes all sounds made by on-foot characters, including your character when outside the helicopter.

The "English Sounds" selection includes dispatch, megaphone and service speech.

If you wish to restore thee files manually, first navigate to SIMCOPTER\sound, delete all of the ".wav" files, then do the same in SIMCOPTER\sound\people and SIMCOPTER\sound\english, all files, sorted in the sam hierarchy, will be located in the hidden "Backup" folder in SIMCOPTER\sound.

Please delete the backup folder when you have finished restoring your files.

Disclaimer: This feature currently only works with English versions of the game. If you wish to have sound shuffling work for other languages please message me on GitHub or on Discord at Yossitaru#0389.
