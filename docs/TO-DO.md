# TO-DO

## Decisions to make first

- [ ] View: top-down (Omori / Undertale) or side-view platformer (Hollow Knight)?
- [ ] Combat: turn-based, bullet-hell dodging, or real-time in the world?
- [ ] Art style and base resolution (for pixel art: 320x180 or 640x360, scaled up).

## Project setup

- [x] Feature-based folder structure (see `structure/ProjectStructure.md`).
- [x] Main scene set and project icon path fixed.
- [x] `CONTRIBUTING.md`.
- [ ] If the art is pixel art: set Rendering > Textures > Default Texture Filter to `Nearest`.
- [ ] Input Map actions: `move_left`, `move_right`, `move_up`, `move_down`, `jump`, `attack`, `interact`.
- [ ] Test setup, once there is game logic worth testing.

## Vertical slice (first playable)

- [ ] Player moves inside one room (placeholder square is fine).
- [ ] Camera follows the player.
- [ ] Walls and floor from a `TileMapLayer`.
- [ ] One NPC with one line of dialogue.
- [ ] One enemy and one complete fight.
