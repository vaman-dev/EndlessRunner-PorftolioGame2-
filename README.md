# 🚀 Neon Rush: The Endless Runner

Welcome to **Neon Rush**, an action-packed Endless Runner game built with Unity!  
Dash through neon-lit highways, dodge enemy cars, and survive as long as you can. This project showcases advanced game programming concepts like **Object Pooling**, **Finite State Machines (FSM)**, and **Enemy AI with NavMesh Agents**.

---

## 🔧 Tech Stack

- **Engine**: Unity 6
- **Language**: C#
- **AI Navigation**: Unity NavMesh
- **Design Patterns**:
  - 🌀 **Object Pooling System (OPS)** – For performance-friendly road and enemy car spawning
  - 🧠 **Finite State Machine (FSM)** – To control complex enemy behavior states (Idle, Chase, Smash!)
  - 🎯 **Component-Based Architecture** – Clean, scalable logic separation
  
---

## 🧠 Core Features

### ♻️ Smart Object Pooling (OPS)
Rather than instantiating and destroying objects during gameplay (which can be expensive), we utilize an efficient **Object Pool Manager** that reuses:
- Road tiles
- Enemy cars
- Particle effects (like sparks and crash FX)

### 🧭 Enemy AI with NavMesh
- **Enemy Cars** spawn and **chase** the player using `NavMeshAgents`.
- Enemies spawn based on proximity and player score.
- AI switches between **patrolling**, **chasing**, and **returning** using FSM logic.

### 🔄 Finite State Machine (FSM)
We’ve implemented an FSM for enemy behavior:
- `IdleState`: Waiting to detect the player
- `ChaseState`: Pursuing the player
- `AttackState`: Attempting to ram
- `ReturnState`: Resetting back to pool after action

FSM is built using interface-based patterns for flexibility and extendability.

---

## 🎮 Controls

- **Swipe Up** – Jump  
- **Swipe Down** – Slide  
- **Tilt/Touch Left-Right** – Lane Change  

*Optimized for mobile gameplay with Unity Input System and touch gestures.*

---

## 🗃️ Project Structure Highlights

```bash
Assets/
│
├── Scripts/
│   ├── Managers/
│   │   └── ObjectPoolManager.cs
│   │   └── SpawnManager.cs
│   ├── Enemy/
│   │   └── EnemyFSMController.cs
│   │   └── States/
│   │       ├── IdleState.cs
│   │       ├── ChaseState.cs
│   │       ├── AttackState.cs
│   ├── Player/
│   │   └── PlayerController.cs
│   │   └── PlayerInputHandler.cs
│   └── Utilities/
│       └── SwipeDetector.cs
│
├── Prefabs/
│   ├── RoadTile.prefab
│   ├── EnemyCar.prefab
│   └── PlayerCar.prefab
