# MemoryGame

A classic memory-matching game built with WPF and following the MVVM pattern.

## Overview
MemoryGame is a desktop application built in C# using Windows Presentation Foundation (WPF).  
It presents a grid of hidden cards, and the player's goal is to find and match pairs of identical images.  

The game supports:
- Player login
- Avatar selection
- Adjustable board size
- Adjustable timer
- Save/load functionality
- Statistics


## Features
- **Player accounts:** Login and select avatars.  
- **Adjustable board:** Choose grid size (e.g., 4x4, 6x6).  
- **Adjustable timer:** Countdown timer that can be set by the player to limit game duration.  
- **Save/Load:** Continue your game later.  
- **MVVM pattern:** Clean separation of UI and logic.  
- **User-friendly interface:** Simple and intuitive design with WPF.


## Getting Started
Follow these instructions to run the project locally.

### Prerequisites
- Windows OS  
- Visual Studio with WPF support  
- .NET framework compatible with WPF (e.g., .NET 6 or .NET Framework 4.x)

### Installation
1. Clone the repository:
```bash
git clone https://github.com/al3ssVil/MemoryGame.git
```

2. Open the solution in Visual Studio.

3. Build the project.

## Usage

1. Launch the application.  

2. Create or select a player profile and choose an avatar.

<img width="606" height="490" alt="image" src="https://github.com/user-attachments/assets/33b294cb-922a-458c-8a24-fc725ed0852e" />

3. Pick a board size and timer:  
   - Use the default board (4x4) with the default timer (60 seconds). You click Standard and after New game 
   - Or customize the board size and timer to your preference, after that, you presse Apply Custom Size and New Game.
<img width="1155" height="866" alt="image" src="https://github.com/user-attachments/assets/cd3fc2f3-fdf6-4958-a82a-eb2aaac087c0" />
<img width="1156" height="869" alt="image" src="https://github.com/user-attachments/assets/3dbfc70e-c876-4a03-8e88-2c4dfadcab3b" />

4. Click cards to reveal them and find matching pairs.  
<img width="1164" height="863" alt="image" src="https://github.com/user-attachments/assets/73bfaafa-94a3-4629-a8bd-98d752ae3189" />

5. Save your progress and load previous games as needed.  

6. When the game ends:  
   - If all pairs are matched within the timer, you win.  
   - Player statistics are updated automatically: total games played and games won.
     <img width="1158" height="858" alt="image" src="https://github.com/user-attachments/assets/810a2233-6edc-4e6f-b5c3-8cc4c00ac929" />

## Architecture & Technologies

WPF (Windows Presentation Foundation) – UI framework.

MVVM (Model-View-ViewModel) – Design pattern for separation of concerns.

C# – Primary programming language.

Project Structure:

Models → Game data and player profiles

ViewModels → Logic and data binding

Views → XAML files for UI

Persistence: Game progress saved via local file storage or serialization.
