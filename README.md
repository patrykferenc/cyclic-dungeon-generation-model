# cyclic-dungeon-generation-model

An implementation of cyclic graph dungeon generation algorithms.
This was made as a personal project during my studies on the WUT.

# Notes

The readme is written in English, but I do have an accompanying "paper" in Polish if you want to (and can) read it,
it can be found in the [releases section](https://github.com/patrykferenc/cyclic-dungeon-generation-model/releases/tag/v0.1.0-preview).

# Features

This project implements the cyclic generation algorithms described in the papers listed in the report (in Polish).
The algorithm can be described in a few simple steps:
- First, we generate a graph (laid as a grid) with empty nodes.
- Then, we add a major cycle (connected nodes) to the graph. This way we create a two-way connection between the starting room and the final room.
- Next, we apply some rules to the cycle that decide its type - for example, if it should have a patrolling monster, locked gates etc.
- After the graph representation is complete, we scale it into a low-resolution tilemap - we add doors and corridors between the rooms.
- The last part of the generation is to create the final tilemap. We apply certain rules to rooms to make them more interesting in shape.

It is by no means a full and comprehensive way to generate a dungeon, but it can be expanded and tinkered with to get the desired result.
I expect to add more features in the future and also improve the codebase a little bit.

## Examples of generated dungeons

Depending on the set theme and attributes the model can produce different results.
Some of the obtained results are listed here:

![Big abandoned castle](https://user-images.githubusercontent.com/81482531/175813315-484c386e-ed3a-4f46-bb4f-9c864678b4cd.png)
![Small castle](https://user-images.githubusercontent.com/81482531/175813374-e5cb38fa-e4cd-4186-a594-e38956d65160.png)
![Big cave](https://user-images.githubusercontent.com/81482531/175813317-8d4aedbd-ba88-40ac-adf0-f2bac87619df.png)

# Installation and running

You need to have .NET Core installed. I tested it on Windows 10, 
and macOS Monterey (M1) but running it on other reasonable platforms should not be a problem.

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
