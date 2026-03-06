# Gufeng Rhythm Game (Unity Prototype)

A small rhythm game prototype built in **Unity** exploring music-synchronized gameplay systems.  
The project focuses on implementing core rhythm-game architecture such as timing, note spawning, chart recording, and scene flow rather than being a fully polished game.

---

## Overview

This project was created as a learning exercise while exploring Unity gameplay programming and rhythm-game mechanics.

The game features a **7-lane pitch system** where notes scroll toward the player in sync with the music.  
Players press corresponding keys to match the pitch lane when the note reaches the hit area.

Key design goals were to experiment with:

- Audio-synchronized gameplay timing
- Data-driven chart systems
- Input handling for rhythm gameplay
- Scene management and UI flow
- Simple fail / retry mechanics

---

## Implemented Systems

### Music Timing System
A **Conductor system** synchronizes gameplay with the music using Unity's `AudioSource` timing.  
All gameplay elements reference this system to ensure consistent note timing.

### Chart Recording Pipeline
A chart recording tool allows notes to be recorded during gameplay and exported as **JSON chart data**.

Features:
- Record notes using gameplay input
- Export chart data to JSON
- Reload chart data automatically when gameplay starts

### Note Spawning
Notes are spawned dynamically based on chart timestamps and scroll toward the hit area.

Supports:
- lane-based notes
- hold note durations
- configurable scroll speed

### Lane Input System

Seven lanes mapped to keyboard input:

| Lane | Key |
|-----|-----|
| 0 | Z |
| 1 | X |
| 2 | A |
| 3 | S |
| 4 | D |
| 5 | W |
| 6 | E |

The system supports:

- priority handling when multiple keys are pressed  
- hold notes  
- returning to a rest position when no key is held  

### Scene Flow

Basic scene navigation between:

Start Menu → Song Select → Gameplay → Fail / Results

Players can retry the song or return to song select after failing.

---

## Current Status

⚠️ **Prototype / Work in Progress**

The gameplay pipeline is functional, but the project is currently paused while working on other projects.

Known limitations:

- Chart timing still needs refinement
- Note movement and animation smoothing need improvement
- Chart editing workflow is currently manual
- Visual polish and UI improvements are planned but not implemented

The project is kept public to demonstrate Unity gameplay programming experience and system design.

---

## Current Gameplay 

Screenshot of gameplay currently

<img width="1508" height="723" alt="image" src="https://github.com/user-attachments/assets/738c2e33-e371-40a4-b41b-5c6a1745cf91" />


---

## Running the Project

1. Clone the repository

```bash
git clone https://github.com/heathercoyne/gufeng-music-game.git
```

2. Open the project using **Unity Hub**

3. Use the Unity version specified in the project (recommended: Unity LTS)

4. Open the gameplay scene:

```
Assets/Scenes/Song1Gameplay.unity
```

5. Press **Play**

---

## Music File Notice

The original music file used during development is **not included in this repository** due to copyright restrictions.

To run the project with music:

1. Place a music file inside

```
Assets/Music/
```

2. Assign it to the `SongChart` asset in Unity.

Any royalty-free music track can be used.

---

## Project Structure

```
Assets
 ├── Scenes
 │   ├── StartMenu
 │   ├── SongSelect
 │   ├── Song1Gameplay
 │   └── Fail
 │
 ├── Scripts
 │   ├── Gameplay
 │   │   ├── Conductor
 │   │   ├── NoteSpawner
 │   │   ├── PlayerController
 │   │   └── ChartLoader
 │   │
 │   └── UI
 │       └── SceneNav
 │
 ├── Charts
 │   └── song1_recorded.json
 │
 └── Prefabs
```

---

## Development Notes

This project was primarily built to explore rhythm-game programming concepts in Unity.

Key challenges solved during development:

- synchronizing gameplay timing with audio playback
- designing a JSON-based chart recording pipeline
- implementing lane-based input systems
- creating scene navigation and retry systems

---

## Future Improvements

Planned improvements include:

- smoothing note animation and movement
- improving chart timing accuracy
- creating a visual chart editor
- supporting multiple songs
- improving UI and gameplay feedback
- adding lyric-based notes

---

## Author

Heather Xin Coyne

This project is part of my Unity learning process while exploring gameplay programming and rhythm-game systems.
