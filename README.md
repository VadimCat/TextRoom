# Escape the Dungeon — TextRoom

## Project Overview
TextRoom is a small instructional text adventure in which the player must escape a dungeon by typing commands. The project is built in Unity and demonstrates how to organise a scalable architecture based on ScriptableObject configurations for rooms, items, and crafting recipes. The gameplay loop supports multiple room scenes that the player completes one after another.

## Environment Requirements
- **Unity**: editor version 6000.0.57f1 (see `ProjectSettings/ProjectVersion.txt`).
- **Platforms**: designed to run in the editor or any vertical 9:16 resolution.

## Quick Start
1. Open the project in the required Unity version.
2. Add the scenes `Bootstrap`, `0.WakeRoomScene`, and `1.LabaratoryRoomScene` to the Build Settings in this order. The sequence is consumed by the game cycle (`Assets/Scripts/AppCycle.cs`).
3. Open the scene `Assets/Scenes/Bootstrap.unity` and press **Play**.
4. Type commands in the text field and confirm them with **Enter**. The message history appears in the left pane, and the **Next Room** button becomes available once you finish a room.

## Gameplay
### Core Commands
| Command | Purpose |
| --- | --- |
| `look` | Describes the current room and lists available objects (`LookCommandProcessor`). |
| `take <item>` | Picks up an item from the environment and moves it to the inventory (`TakeCommandProcessor`). |
| `read <object>` | Reads notes or other contextual information (`ReadCommandProcessor`). |
| `inventory` | Displays the current contents of the inventory (`CheckInventoryMessageProcessor`). |
| `unlock <object>` | Attempts to open a locked object when all required keys are collected (`UnlockCommandProcessor`). |
| `gather <ingredient1> <ingredient2> ...` | Combines items according to a recipe to create a new object (`CraftMessageProcessor`). |

Commands are case-insensitive but require the exact item names.

### Room 1 — Wake Room
- The initial description, note, and locked door are configured in `Room0Config` (`Assets/Configs/Room0/Room0Config.asset`).
- Take the key (`take key`) and read the note (`read note`) to receive a hint.
- Use `unlock door` to open the door and finish the room. The unlock message is stored in `DoorLockedItemConfig.asset`.

### Room 2 — Laboratory Room
- The room configuration is defined in `LabaratoryRoomConfig.asset` and introduces an alchemy puzzle.
- Look around (`look`), collect the ingredients (`take flask`, `take sulfur`, `take crystal`).
- The book (`read book`) reveals the recipe: `gather flask sulfur crystal` crafts the potion (`PotionRecipeConfig.asset`).
- Break the sealed sigil with `unlock sigil` to complete the second room (`SigilLockedItemConfig.asset`).
- After the success message, the transition button and final narration become available (`GameVisualEffects` and the `FinalMessage` field).

## Architecture and Extensibility
- **Game loop** (`GameCycle`, `AppCycle`, `Bootstrap`) controls room sequencing, UI setup, and scene transitions.
- **Command handling** is implemented through the `MessageProcessingPipeline`, which wires together individual command processors. To extend the system, implement a new `IMessageProcessor` and register it in `Bootstrap`.
- **Configurations** for rooms, items, locks, and recipes are stored as ScriptableObjects in `Assets/Configs`, enabling content updates without changing code.
- **UI** uses `MessageHistoryView` and `PlayerInputView` to display messages and handle command input, while message styles are configured via `MessageStyleConfig`.

## Development Tips
- Add new rooms by duplicating an existing `RoomConfig` asset and linking it to a new scene.
- To expand puzzles, create additional `RecipeConfig` assets and `LockedItemConfig` assets that list the required keys.

