# TO-DO

Rules for this file:
- **Doing** holds at most 1-2 items.
- Every item should be finishable in one or two sessions, testable, and change something you can see or hear in the game.
- When an item grows, split it. When it is done, move it to **Done**.

## Doing

- [ ] Player state machine (idle, run, jump_start, rise, fall, land), one state per existing sprite sheet.

## Next

### Player
- [ ] Idle animation (the move sheet is reused as a slow hover for now).
- [ ] Flip the sprite according to the movement direction.
- [ ] Variable jump height (releasing the button shortens the jump).
- [ ] Coyote time and jump buffering.

### Camera
- [ ] Camera follows the player, Hollow Knight style.
- [ ] Per-level camera limits (`limit_left`, `limit_right`, ...) and smoothing.
- [ ] Slight look-ahead in the movement direction.

### World
- [ ] Walls and floor from a `TileMapLayer` (the white room uses plain rectangles for now).

## Later

### Combat
- [ ] `HealthComponent`, `Hitbox`, `Hurtbox` in `game/components/`.
- [ ] Player attack.
- [ ] Player damage, temporary invulnerability and knockback.
- [ ] Player death and respawn.
- [ ] One enemy with simple AI (patrol, then chase).
- [ ] One enemy and one complete fight.

### Levels
- [ ] Second level with a transition (door or screen edge), so `Main` handles scene changes.
- [ ] Checkpoint.
- [ ] Hazard (spikes) that deals damage.
- [ ] One-way platforms.

### UI
- [ ] HUD with a health bar.
- [ ] Main menu and pause menu.
- [ ] Dialogue box (needed before the NPC item below).

### Story
- [V] Write 3-5 lines of background each for the player, the NPC and the enemy in `docs/story.md`.
- [V] One NPC with one line of dialogue.

### Feel
- [ ] Jump and landing sounds (`game/assets/audio/`).
- [ ] Dust particles on landing.

## Project setup

- [ ] Rename `Game1` (`.csproj`, `.sln`, `RootNamespace`, project name in `project.godot`).
- [ ] Check that `.gitignore` contains `.godot/`, `bin/` and `obj/`. Commit `.import` and `.uid` files.
- [ ] Rename `Audio` to `audio` in `game/assets/`.
- [ ] Keep `docs/structure/ProjectStructure.md` in sync with the real folders.
- [ ] More Input Map actions as needed: `attack`, `dash`, `interact`, plus controller buttons.
- [ ] Debug overlay (FPS, current player state).
- [ ] Test desktop export early (C# projects in Godot 4 cannot export to Web).
- [ ] Test setup, once there is game logic worth testing.

## Done

- [x] Player scene with movement, jump and fall sprites.
- [x] White room test level.