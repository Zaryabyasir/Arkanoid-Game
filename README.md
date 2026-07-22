# 2D Block Breaker (Arkanoid) – Unity Personal Project

A polished 2D arcade-style Block Breaker game built in Unity with 3 playable levels. It features progressive brick health mechanics, dynamic power-ups, multi-ball lifetime tracking, custom physics corrections, and smooth visual feedback.

## Gameplay Video
https://github.com/user-attachments/assets/b8ceae71-0893-4363-b8f7-a7dc0811f87c


## Screenshots

### Level 1 - The Basics
<img width="1617" height="907" alt="Level-1" src="https://github.com/user-attachments/assets/d11f6f18-2db8-47e6-97ce-af26ec983c30" />


### Level 2 - Multi-Hit Bricks & Damage Visuals
<img width="1624" height="908" alt="Level-2" src="https://github.com/user-attachments/assets/3a16cc94-d28e-4368-bda5-f6fba6d11225" />


### Level 3 - Yellow Bricks & Multi-Ball Chaos
<img width="1622" height="906" alt="Level-3" src="https://github.com/user-attachments/assets/8775a1c4-62e8-47f4-8d49-6540f10444a3" />


## UI Screens

### Victory Screen
<img width="1919" height="907" alt="Victory Screen" src="https://github.com/user-attachments/assets/693707c7-4c92-4b8b-8d20-8e354e9d8c72" />


### Game Over Screen
<img width="1919" height="906" alt="Game Over" src="https://github.com/user-attachments/assets/09aa92f7-f68b-48b6-9756-637e4d6c5076" />


## Features
* **3 Progressive Levels:** Bricks feature varying durability based on color (Blue = 1 hit, Red = 2 hits, Green = 3 hits, Yellow = 4 hits).
* **Dynamic Power-Ups (15% Drop Chance):**
  * **Multi-Ball:** Spawns two clone balls that launch at opposing angles.
  * **Expand Paddle:** Increases the paddle's width for 5 seconds.
  * **Slow Ball:** Temporarily slows down all active balls to help keep them in play.
* **Multi-Ball Lifecycle Tracking:** Death zone tracker ensuring the player only loses a life when the *last* ball on the screen falls.
* **Infinite Bounce Physics Guard:** Custom horizontal velocity check that automatically nudges the ball if its Y-velocity drops too low, preventing endless horizontal loops.
* **Damage Visuals:** Sprite-swapping system that dynamically displays cracks on multi-hit bricks when they reach their second-to-last hit.
* Audio Feedback: Sound effects for brick destruction, paddle hits, life loss, victory, and game-over events.
* **UI Feedback:** Dynamic heart-based life UI, live score tracking, and clean Game Over / Victory state menus.

## Built With
* Unity
* C#
* Unity New Input System
* TextMeshPro

## What I Learned
This project helped me practice:
* **Managing Race Conditions:** Implementing clean state flags in the `GameManager` to prevent multiple power-ups from spawning on the exact same frame during Multi-Ball chaos.
* **Handling Object Lifecycles & Clones:** Instantiating and tracking active ball clones dynamically in the scene and managing safe cleanup without throwing `MissingReferenceException` errors.
* **Custom Physics & Colliders:** Overriding RigidBody2D velocity vectors to solve horizontal traps and calculating accurate rebound angles based on paddle-hit offsets.

## Controls

### PC
* **A/D or Left/Right Arrow Keys** → Move Paddle
* **A/D or Left/Right Arrow Keys (First tap)** → Launch Ball at start of round

## Author
**Zaryab Yasir**    
