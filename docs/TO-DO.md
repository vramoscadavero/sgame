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
- [x] Input Map actions: `move_left` (A, Left), `move_right` (D, Right), `jump` (Space, W, Up).
- [ ] More Input Map actions as needed: `attack`, `dash`, `interact`, plus controller buttons.
- [ ] Test setup, once there is game logic worth testing.

## Vertical slice (first playable)

- [x] Player walks, jumps and falls with gravity inside one room (`game/levels/white_room`).
- [ ] Idle animation (the move sheet is reused as a slow hover for now).
- [ ] Coyote time and jump buffering.
- [ ] Camera follows the player.
- [ ] Walls and floor from a `TileMapLayer` (the white room uses plain rectangles for now).
- [ ] One NPC with one line of dialogue.
- [ ] One enemy and one complete fight.
