
## Table of Contents

<details>

   <summary>Contents</summary>

1. [🎮 Game Overview](#-game-overview)
1. [🕹️ How to Play](#-how-to-play)
   1. [⚠️ Rules](#-rules)
1. [⌨️ Controls](#-controls)
   1. [Player 1 — WASD Keys](#player-1--wasd-keys)
   1. [Player 2 — Numeric Keys](#player-2--numeric-keys)
1. [🏗️ Project Structure](#-project-structure)
1. [⚙️ Scene Setup Notes](#-scene-setup-notes)
1. [🖥️ UI Elements Required](#-ui-elements-required)
1. [🔊 Audio](#-audio)
1. [🚀 Getting Started](#-getting-started)
1. [📄 License](#-license)

</details>
# 🐱 Super Pipes

> **Hit as many slimes as you can before time runs out!**

A fast-paced 2-player local competitive minigame built in **Unity**, where two cat warriors battle to rack up the highest score by smashing slimes that pop up from pipes — each on their own side of the arena.

---

## 🎮 Game Overview

| Field | Details |
|---|---|
| **Engine** | Unity (2D) |
| **Genre** | Local Multiplayer / Arcade / Minigame |
| **Players** | 2 (same keyboard) |
| **Objective** | Score more points than your opponent before time runs out |

---

## 🕹️ How to Play

Each player controls a cat warrior on their own half of the screen. Slimes pop up from pipes at random — press the correct directional key **while the slime is exposed** to smash it and earn a point.

### ⚠️ Rules
- Slimes are **invulnerable** for a brief moment right after surfacing — wait a split second before striking.
- If you miss a slime, it retreats back into the pipe with no penalty.
- Only **one slime appears at a time** per side.
- The player with the **most points** when time runs out wins.

---

## ⌨️ Controls

### Player 1 — WASD Keys

| Key | Action |
|---|---|
| `W` | Attack **Up** pipe |
| `A` | Attack **Left** pipe |
| `S` | Attack **Down** pipe |
| `D` | Attack **Right** pipe |

### Player 2 — Numeric Keys

| Key | Action |
|---|---|
| `1` | Attack pipe **1** |
| `2` | Attack pipe **2** |
| `3` | Attack pipe **3** |
| `5` | Attack pipe **5** |

> Both top-row number keys and the **numpad** are supported for Player 2.

---

## 🏗️ Project Structure

```
SuperPipes/
├── Scripts/
│   ├── GameManager.cs       # Countdown, enemy spawning, score tracking
│   ├── Villano.cs           # Enemy rise/retreat cycle and hit detection
│   ├── Player1Attack.cs     # WASD input handling and sprite feedback (P1)
│   ├── Player2Attack.cs     # Numeric input handling and sprite feedback (P2)
│   ├── Menu_Pause.cs        # Pause / resume / restart / quit logic
│   └── Menu_boton.cs        # Main menu button interactions with audio feedback
```

---

## ⚙️ Scene Setup Notes

- Each player side requires its **own `GameManager` instance** in the hierarchy — scores and spawners are fully independent per side.
- `Villano` objects must be **children of their respective `GameManager`** so that `GetComponentInParent<GameManager>()` resolves correctly on hit.
- Enemy slots **1–4** respond to `"Player"` tag; slots **5+** respond to `"Player2"` tag.
- Assign `AudioSource`, `AudioClip`, and `TextMeshProUGUI` references in the Inspector for each `GameManager`.

---

## 🖥️ UI Elements Required

| Component | Description |
|---|---|
| `PauseMenu` (GameObject) | Panel shown when the game is paused |
| `pauseButton` (GameObject) | Button hidden during pause |
| `textoCuentaAtras` (TMP) | Countdown text (3 → 2 → 1 → ¡GOYA!) |
| `textoPuntos` (TMP) | Live score display per player side |

---

## 🔊 Audio

- **Click sound** plays on every main menu button press, with a `0.2s` delay before scene transition to ensure audio completes.
- **Score sound** (`clipContador`) plays each time a player earns a point via `AudioSource.PlayOneShot`.

---

## Getting Started

1. Open the project in **Unity 2021.3 LTS** or later.
2. Add all game scenes to **Build Settings** in order:
   - `0` — Main Menu
   - `1` — Game Scene
3. Ensure **TextMeshPro** is imported (`Window → Package Manager → TextMeshPro`).
4. Press **Play** in the Editor or build for your target platform.

---

## 📄 License

This project is for educational and personal use. Assets and character designs are original and belong to their respective creators.
