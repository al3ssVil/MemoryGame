# Memory Game - WPF & MVVM (C#)

A classic **Memory Match Game** built with **WPF** using the **MVVM (Model-View-ViewModel)** pattern. This application allows players to sign in, play the game, save/load progress, and view gameplay statistics. It also supports custom board sizes and image categories.



##  Game Overview

The Memory Game challenges players to find matching pairs of images. Players flip two cards at a time and try to match them. If the cards match, they disappear; otherwise, they flip back.

Key features:
- Sign In / Sign Up system with image avatar association
- Game board with 4x4 or customizable grid (MxN)
- Three predefined image categories
- Save/Load game progress per user
- Timed gameplay
- User statistics and win tracking
- Persistent user and game data storage using JSON



##  Sign In / Create Account

Upon starting the app, users are prompted to:

- Select an existing user from a list
- Create a new account by choosing a unique username and an image file (jpg, png, gif)
- Delete an existing user

**Note:** User image paths are stored as relative paths for portability across machines.

![image](https://github.com/user-attachments/assets/b25092a8-94b2-4dfc-b4e0-3126da2b859f)



##  Gameplay

### Menu Options:

- `File`
  - `Category`: Select one of three predefined image categories
  - `New Game`: Start a new game
  - `Open Game`: Resume previously saved game
  - `Save Game`: Save current game state
  - `Statistics`: View user gameplay stats
  - `Exit`: Return to Sign In screen

- `Options`
  - `Standard`: 4x4 board
  - `Custom`: Choose board size (MxN, between 2 and 6; total cells must be even)

- `Help`
  - `About`: View developer info (name, email, group, specialization)

![image](https://github.com/user-attachments/assets/351a9428-8bde-4d79-ae53-72a9aa066566)




##  Game Rules

- Flip two cards to try and find a match
- Matching cards remain visible/inactive
- Non-matching cards flip back on the next selection
- Same card cannot be selected twice in a row
- Game ends when all pairs are found or time runs out
- Each game has a different randomized layout
  
![image](https://github.com/user-attachments/assets/fd4fa575-8228-45ab-b30d-d31f1d1c328d)

⏱ Remaining time is visible during gameplay. If the timer runs out, the game is lost.

![image](https://github.com/user-attachments/assets/c90f3f5a-199a-4362-8d00-807967891844)



##  Saving & Loading

Players can:

- Save the current state of the game (category, card layout, time remaining)
- Resume the game later from the same state
- Each user can only access their own saved games

You can choose between overwriting a single save file or saving multiple sessions (configurable in code).



##  Delete User

Deleting a user will:

- Remove them from the user data file
- Delete their avatar association
- Remove any saved games
- Delete all gameplay statistics


##  Statistics

Track each user's game performance:

- Games Played
- Games Won

All stats are saved to a file and can be viewed in a separate statistics window.

![image](https://github.com/user-attachments/assets/22ef3e26-11e4-4089-aa2b-0039837de269)




##  Technologies Used

- **.NET / C#**
- **WPF (Windows Presentation Foundation)**
- **MVVM Pattern**
- **JSON** for persistent storage
- **Data Binding** for View ↔ ViewModel synchronization

- ##  How to Run

1. Clone the repository
2. Open solution in **Visual Studio**
3. Build and run the application
4. Start playing!


