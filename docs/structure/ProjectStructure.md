# Project structure

The project is organized **by feature**, not by file type.
Everything that belongs to one thing (its scene, its script, its sprites, its data) lives in the same folder.
When you delete or change a feature, you only touch one folder.

```text
sgame/
├── project.godot      Godot project settings
├── Game1.csproj       C# project (NuGet packages go here)
├── docs/              Documentation, roadmap, decisions
├── addons/            Third-party or custom Godot plugins (created when the first one is added)
└── game/              Everything the game loads at runtime
	├── main/          Entry scene (main.tscn + Main.cs), loaded when the game starts
	└── assets/        Shared files that no single feature owns (project icon, fonts, shared sounds)
```

Folders for new features are created when the feature is started, not before.
Examples of what will appear later:

```text
game/
├── player/            player.tscn, Player.cs, sprites, player_stats.tres
├── enemies/
│   └── slime/         slime.tscn, Slime.cs, sprites, slime_stats.tres
├── components/        Reusable nodes shared by many features (HealthComponent, Hitbox, Hurtbox)
├── levels/            Rooms and maps
└── ui/                Menus, HUD, dialogue box
```

## Resources (.tres)

A Resource is a data file that Godot can edit in the inspector, for example the stats of one enemy or one weapon.
See <https://docs.godotengine.org/en/stable/tutorials/scripting/resources.html>
A `.tres` file lives next to the feature that uses it (for example `game/enemies/slime/slime_stats.tres`).
The C# class that defines the resource type lives with the feature that owns it, or in `game/components/` if many features share it.

## Logs

Logs are not stored in the repository.
Godot already writes them to `user://logs` (on Windows: `%APPDATA%\Godot\app_userdata\Game1\logs`).

## Naming conventions

These follow the official Godot style guide.

- Folders, scenes and resources: `snake_case` (`main.tscn`, `slime_stats.tres`).
- C# scripts: `PascalCase`, and the file name must match the class name exactly (`Main.cs` contains `class Main`), otherwise Godot cannot find the class.
