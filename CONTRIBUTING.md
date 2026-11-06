# Contributing

## Requirements

- Godot **4.7.2, .NET edition** (the "mono" download, not the standard one).
- .NET SDK 8.
- Any C# editor: VS Code with the C# Dev Kit, Visual Studio or Rider.

## Getting started

1. Clone the repository.
2. Open Godot, click Import and select `project.godot`.
3. Press F5 to run the game.

To check that the C# code compiles without opening Godot, run `dotnet build` in the repository root.

## Branches

- Never commit directly to `main`.
- Create one branch per topic, starting from an up-to-date `main`:
  - `feature/<name>` for new gameplay or content (`feature/player-movement`).
  - `fix/<name>` for bug fixes.
  - `chore/<name>` for setup, tooling and docs.
- Keep pull requests small and about one thing.
- Ask the other person to review before merging.

## Commits

- Write the message in the imperative: "Add player dash", not "Added player dash".
- One logical change per commit.

## Working with Godot and Git

- **Move and rename files inside the Godot editor** (FileSystem dock), not in Explorer or with `git mv`.
  Godot then updates every scene that references the file.
- Commit the `.import` and `.uid` files next to assets and scripts.
  They are how Godot keeps references working across machines.
- Never commit the `.godot/` folder.
  It is a cache that Godot rebuilds on every machine.
- Save and close the editor before switching branches or pulling.
  An open editor can write old versions of files back to disk.
- Two people editing the same `.tscn` file at the same time produces merge conflicts that are hard to fix by hand.
  Agree on who works on which scene, and split big scenes into smaller ones.

## Project structure and code style

See [docs/structure/ProjectStructure.md](docs/structure/ProjectStructure.md) for where files go and how to name them.

## Dependencies

- **C# libraries** come from NuGet.
  Add them with `dotnet add package <Name>`, which updates `Game1.csproj`.
- **Godot plugins** (from the Asset Library or GitHub) go in `addons/` and are committed to the repository.
  Enable them in Project > Project Settings > Plugins.
