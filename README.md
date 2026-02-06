# SuperMiner

A compact Unity minesweeper-style prototype built around Zenject and a lightweight service layer.

## Highlights
- Procedural board fill with safe first click flow
- State-driven game loop (Ready, Playing, Win, Lose)
- Clean service boundaries for input, board data, and view rendering
- SimpleInput integration for mouse/button handling

## Controls
- Left mouse button: open cell
- Right mouse button: mark cell

## Quick Start
1. Open the project in Unity.
2. Load your main scene.
3. Press Play.

## Structure
- `Assets/CodeBase/Infrastructure/StateMachine`: game states and transitions
- `Assets/CodeBase/Infrastructure/Services`: input, board logic, view, and assets provider
- `Assets/CodeBase/Gameplay`: bootstrapping and gameplay wiring

## Tech
- Unity
- Zenject
- SimpleInput

## Notes
If you add new UI or board prefabs, register them in the scene installer so the state machine can access them.
