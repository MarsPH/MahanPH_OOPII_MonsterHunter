# Monster Hunter  

[![C#](https://img.shields.io/badge/Made%20With-C%23-239120)](https://learn.microsoft.com/en-us/dotnet/csharp/)  

---  

## 🎮 Overview  

**Monster Hunter** is a **2D game** developed for my **OOP II class** to demonstrate **advanced object-oriented programming concepts in C#**. Built solely with **Console and Windows Forms (without a game engine)**, the project forced me to work at a lower level to implement **character movement, combat, and item management**. Despite its challenges, the game deepened my understanding of **OOP principles** and **design patterns** in C#.  

<p align="center">
  <img src="https://media1.giphy.com/media/v1.Y2lkPTc5MGI3NjExb2FmM21kN29oODFmZnZiaHlkZTUwaWYyNjN0NHZlbnpldWRtajltMyZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/TSuAfrSSbZX19Vdwnn/giphy.gif" alt="Monster Hunter Gameplay Preview">
</p>

---  

## 🧩 Key Components  

- **🗺️ Map**  
  - Loads and validates **`.map`** files from the current directory.  
  - Represents the **game world** as a **two-dimensional char array**.  
  - Initializes positions for the **hunter and monsters** based on map data.  

- **🛡️ Character & Hunter**  
  - **Character**: Abstract base class defining common properties (**HP, armor, strength**) and movement logic.  
  - **Hunter**: Inherits from `Character` and implements **movement, combat, item pickup, and interactions with the map**.  

- **👹 Monsters Management**  
  - Static class `Monsters` to **track and locate monsters** on the map.  
  - Provides methods to **add monsters and find them by position**.  

- **⚔️ Combat & Items**  
  - Implements **combat mechanics** between the hunter and monsters.  
  - Includes item classes such as `Shield` and `Pickaxe`.  
  - Contains various **potion classes** (e.g., `PotionStrength`, `PotionPoison`, `PotionInvisible`, `PotionFast`) that use the **`IPotionStates` interface** to modify hunter stats.  

- **🛠️ Utility Classes**  
  - **RandomSingleton**: Provides a **shared random number generator**.  
  - **Program**: Contains the **main game loop, menu, and info board management** through Console output.  

---  

## 📌 Installation & Running the Game  

```bash
git clone https://github.com/your-username/monster-hunter.git
cd monster-hunter
dotnet run
```
Requires **.NET Core SDK** and **Windows Forms** support.  

---  

## 🏆 Contributors  
- **[Mahan](https://github.com/MarsPH)** – Developer   

---  
```mermaid
graph TD
    A["Program (Main Loop)"] --> B["Map"]
    A --> C["Hunter (Character)"]
    C --> D["Items & Potions"]
    D --> F["Shield & Pickaxe"]
    D --> G["Potion Types"]
    A --> E["Monsters Management"]
    H["RandomSingleton"] -.-> D

