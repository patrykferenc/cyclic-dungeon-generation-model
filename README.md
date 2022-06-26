# cyclic-dungeon-generation-model

An implementation of cyclic graph dungeon generation algorithms.
This was made as a personal project during my studies on the WUT.

# Notes

The readme is written in English, but I do have an accopanying "paper" in Polish if you want to (and can) read it,
it can be found in the releases section (TBD).

# Features

This project implements the cyclic generation algorithms described in papers listed in the report (in polish).
The algorithm can be described in a few simple steps:
- First, we generate a graph (laid as a grid) with empty nodes.
- Then, we add a major cycle (connected nodes) to the graph. This way we create a two way connection between the starting room and the final room.
- Next, we apply some rules to the cycle that decide its type - for example if it should have a patrolling monster, locked gates etc.
- After the graph representation is complete, we scale it into a low resolution tilemap - we add doors and corridors between the rooms.
- The last part of the generation is to create the final tilemap. We apply certain rules to rooms to make them more interesting in shape.

It is by no means a full and comprehensive way to generate an algorithm, but it can be expanded and tinkered with to get the desired result.
I expect to add more features in the future and alos improve the codebase a little bit.

## Examples of generated dungeons

TBD :))

# Installation and running

You need to have .NET Core installed. I tested it on Windows 10, 
and MacOS Monterey (M1) but running it on other reasonable platforms should not be a problem.

The recommended way is to just clone the repository and open it with Rider or Visual Studio. This should work out of the box.
You run it just by running the Program.cs file.

# Known bugs

There are a few bugs right now that I have not fixed:
- The caves can sometimes end up broken - the cellular automaton is not yet checked for empty spaces inside the rooms.
- The starting cycle can sometimes be unsolvable and the cycle can not be closed. It is not that easy to fix but a workaround is to just run the generator again - it does work :>.

# License

This project is licensed under the MIT license, excluding the report.

# Contributions

You are free to contribute to this project if you want, but remember that this is still a work in progress so I might change things pretty dramatically :))
