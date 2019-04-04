# Interview Minefield
This is a project to implement a Console application of a "Minefield" type game as part of an assesment of my current C# Skills.

It uses [TinyIOC](https://github.com/grumpydev/TinyIoC/) to provide a simple IOC container. This may be excessive on a small project like this, but it makes the main app code easier to read.

## Model
The main state of the game is stored in two simple state classes GameState and PlayerState, the player being a property of the game state.
The GameState containe a GameGrid that is implemented using lists as `List<List<Cell>>` this has been deliberately chosen instead of encapuslating the grid as a class, as it allows for manipulation of the grid using LINQ queries while simultaneously allowing access to the data using a simple array notation `GameGrid[row][column]`, which is useful when changing single cells.

## Implementation
This has been broken down in such a way that it should be possible to convert this from its current *Console App* implementation to either a Window App, DirectX or Unity2d Game. The Input and Output are implemented to satisfy the interfaces `IInputManager` and `IDisplayManager`. 

The inital game state is created using Builder class `GameBuilder`, and each step of the game is progressed by calling the `GameStateProcessor`.

The MainGame loop takes instances of `IGameBuilder`, `IGameStateProcessor`, `IInputManager` and `IDisplayManager` injected to the constructor and controls the game, while checking the game state for win and fail conditions.


# Know issues
* Using more than 26 columns in a grid will cause issues with the Chess Notation function (there's no validation to stop it)

