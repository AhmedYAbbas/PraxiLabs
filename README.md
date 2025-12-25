# PraxiLabs – Unity WebGL Enemy Wave System

A scalable, performance-focused enemy wave system built in **Unity 3D (URP)** targeting **WebGL**, developed as a technical assessment for PraxiLabs.

This project demonstrates clean architecture, SOLID principles, and practical optimization strategies suitable for WebGL deployment, with an emphasis on maintainability and extensibility.

---

## 🎯 Objectives (Task Alignment)

This implementation satisfies the full scope of the provided technical task:

- SOLID architecture
- Dynamic, infinite enemy wave progression
- Object pooling for all enemies
- Multiple enemy types per wave
- Manual and automatic wave control
- WebGL-optimized runtime behavior
- Clean separation of concerns and design patterns
- URP lighting configuration with performance awareness
- On-screen diagnostics (wave count, enemy count, FPS)

---

## 🧱 High-Level Architecture

The project is structured around **loosely coupled systems** communicating via events rather than direct references.

### No Singletons
All dependencies are injected via the Inspector or passed explicitly.
This improves testability and avoids hidden coupling.

### Event-Driven Design
Core systems communicate using C# events:
- Enemy death
- Wave progression
- UI updates

This keeps systems loosely coupled.

### Object Pooling
Enemies are recycled using a generic object pool to:
- Eliminate runtime allocations
- Maintain smooth WebGL performance

### ScriptableObjects
Used for:
- Enemy types

This allows adding new content without modifying code
(Open/Closed Principle).

### Enemy AI
Enemies use lightweight wandering/idle behavior:
- No NavMesh
- WebGL-friendly

### UI
UI listens to events and has no direct knowledge of game systems.

---

## 🎮 Features

### Enemy Waves
- Progressive wave sizes:
  - Wave 1 → 30 enemies
  - Wave 2 → 50 enemies
  - Wave 3 → 70 enemies
  - Wave 4+ → +10 per wave
- Infinite wave cycling
- 5-second delay after wave completion
- Randomized enemy types per wave

### Controls
- **Stop / Resume Waves**
- **Next Wave (force spawn)**
- **Destroy Current Wave**

### Performance
- Object pooling for enemies
- No per-frame allocations
- Simple shaders & emissive effects
- Minimal physics usage
- Extensive optimizations

---

## 🧠 Design Patterns Used

- **Object Pool** – Enemy reuse
- **Flyweight Factory** - Enemy Instantiation
- **Observer / Events** – UI, wave communication
- **State Machine** – Enemy AI

---

## 💡 URP & Visual Setup

- URP Forward Renderer
- Baked + realtime hybrid lighting
- Emissive materials for enemy glowing eyes
- Shadow settings tuned for WebGL

---

## 🌐 WebGL Considerations

- No runtime allocations in hot paths
- No physics-heavy logic per frame
- GC-safe UI updates

---

## Free Assets Used

- [Animated Low Poly Robot Free - Stylized Sci-Fi Character](https://assetstore.unity.com/packages/3d/characters/robots/animated-low-poly-robot-free-stylized-sci-fi-character-318201)
- [3D Modern Menu UI](https://assetstore.unity.com/packages/tools/gui/3d-modern-menu-ui-116144)
- [Customizable skybox](https://assetstore.unity.com/packages/2d/textures-materials/sky/customizable-skybox-174576)