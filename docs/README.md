# Data Initialization System Documentation

## Table of Contents

<!-- TOC -->
* [Data Initialization System Documentation](#data-initialization-system-documentation)
  * [Table of Contents](#table-of-contents)
  * [Introduction](#introduction)
  * [Core components](#core-components)
  * [Conclusion](#conclusion)
<!-- TOC -->

---

## Introduction

The **Data Initialization System** is responsible for managing and initializing game data, ensuring a clean and structured approach to data handling. It is designed to be flexible and scalable, allowing storage switching (e.g., Local JSON, Database) with minimal changes to code.

**[CLIENT]** List of System Parts:
- [Player Components](docs/components/Player.md)
- [Inventory Components](docs/components/Inventory.md)
- [Scene Components](docs/components/Scene.md)
- [General Components](docs/components/General.md)
- [NPCs Components](docs/components/General.md)

**[SERVER]** List of System Parts:
- [Middlewares Components](docs/components/Player.md)
- [APIs Components](docs/components/Inventory.md)
---

## Structure of folder

### Animations

Note: Animation clip (*.anim)

### Art

Note: Image, sprite sheet

### Audio

Note: Background music, SFX

### Configuration

Note: Config of monster, skill, drop rate, etc

### Cutscenes

Note: 

### LocalizationFiles

Note: Folder of localization module (Unity), Chinese, Vietnamese document, everything related to foreign language

### Prefabs

Note: Contain prefab to reuse

### Scenes

Note: Contain location, menu scene, Initialization scene is first point for game

### ScriptableObjects

| Folder Name   | Description                                                                                                       |
|---------------|-------------------------------------------------------------------------------------------------------------------|
| Audio         | Background music configuration for each scene will be put at /Audio/AudioCues/Music                               |
| Dialogue      |                                                                                                                   |
| EventChannels | Used when you need to listening or broadcasting event (ToggleLoadingScreen, PauseMenu, etc)                       |
| Gameplay      |                                                                                                                   |
| Input         | Where storage all input event during game (press 1,2,3,4 or move, click, you can references to it and read event) |
| Interaction   |                                                                                                                   |
| Paths         | Declare teleport point (a scene can have many teleport points)                                                    |
| SaveSystem    | Storage config of save system (edit it in inspector)                                                              |
| SceneData     | Data of scene (scene description, etc)                                                                            |
| StateMachine  |                                                                                                                   |
| UI            |                                                                                                                   |

### Scripts

Note: Contain script file (*.cs)

### Settings

Note: Contain settings for game (Audio, Input Actions)

### TextData

Note: Contain data of contributors, etc

---

## How to create a new scene
1. Right-click to choose "ScriptableObject Data"
2. Select `GameSceneType` (Location, Menu, Initialisation, PersistentManagers, Gameplay, Art)
3. 
---

## Conclusion

This system provides a clean and scalable approach to handling game data. By using `DataRepository<T>`, the game can easily switch between storage backends without modifying business logic.