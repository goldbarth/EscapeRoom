# Escape Room - C#

A small console game built to learn C#.
Choose the room size, move around with the arrow keys, collect a key, and reach the exit.
Menus and the tutorial are in German.

This is the original C# project in a series of experiments with the same game idea in different languages.

## Gameplay

- Choose a room width from 30 to 60 and a height from 15 to 28.
- The player, key, and exit receive random positions.
- Move the player to the key to unlock the exit on the right wall.
- Reach the open exit to trigger the ending and win screen.
- Console colors, character graphics, and sound effects provide feedback.

The game takes place in one rectangular room.
The objective is to collect the key and leave; there are no additional puzzle systems or levels.

## Requirements and running

- Windows for the sound effects, which use `Console.Beep(int, int)`.
- An SDK capable of building the project's `net6.0` target and a .NET 6 runtime.
- An interactive console with Unicode and cursor-positioning support, large enough for the selected room and its borders.

```sh
git clone https://github.com/goldbarth/EscapeRoom.git
cd EscapeRoom
dotnet run --project EscapeRoom/EscapeRoom.csproj
```

To build without starting the game:

```sh
dotnet build EscapeRoom.sln
```

The project has no external NuGet package dependencies.

## Controls

| Context | Key | Action |
| --- | --- | --- |
| Main menu | `1` | Start a game |
| Main menu | `2` | Open the quit confirmation |
| Main menu | `3` | Show the tutorial |
| Room setup | Integers, then Enter | Enter width and height |
| In game | Arrow keys | Move the player |
| Quit confirmation | `J` / `N` | Confirm / cancel |

The player is drawn as `Ð`, the key as `¥`, and walls as `█`.
Collecting the key automatically opens the exit; no separate interaction key is needed.

## Code structure

All source files are in `EscapeRoom/`.

| File | Responsibility |
| --- | --- |
| `Program.cs` | Entry point and cursor visibility |
| `Application.cs` | Title screen, tutorial, menus, quit confirmation, and outro |
| `Game.cs` | Room setup, game loop, exit unlocking, and win sequence |
| `Room.cs` | Room dimensions and wall/floor drawing |
| `Player.cs` | Movement, collision checks, rendering, and key collection |
| `Key.cs`, `Exit.cs` | Random placement and drawing of the key and exit |

## Project status

This repository preserves an early learning project.
The gameplay loop is implemented, but there is no automated test suite.
The console is redrawn using absolute cursor positions, and Windows-specific sound calls prevent unmodified gameplay from being portable to every .NET platform.
Menu input validation also needs work: an invalid main-menu key can leave the application stuck in its validation loop.

## Related projects

| Repository | Language | Scope |
| --- | --- | --- |
| [EscapeRoom](https://github.com/goldbarth/EscapeRoom) | C# | Original console game with German menus |
| [EscapeRoom2.0](https://github.com/goldbarth/EscapeRoom2.0) | C++ | Windows console reimplementation with English menus |
| [EscapeRoom3.0](https://github.com/goldbarth/EscapeRoom3.0) | Rust | Early menu prototype; gameplay is not implemented |
