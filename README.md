# 🎮 **SerapKeremZenCoreTools**  

**SerapKeremZenCoreTools** is a toolset designed to accelerate Unity game development and provide a modular structure. These tools are used to manage the core functionalities of a game while supporting **dependency injection** through **Zenject**.  

---

## 📦 **Overview**  
This package is designed to **modularly manage** commonly used systems (`Audio`, `Input`, `UI`, etc.) in Unity projects. Each system has a **specific responsibility** and is **integrated with other systems** using Zenject’s dependency injection. This modular approach makes your project **easier to maintain** and **more scalable**.  

---

## 🛠️ **Scripts and Functionality**  

### 🔊 **Audio System**  
- Centralized sound management for **music, sound effects, and UI sounds**.  
- Supports **object pooling** for better performance.  
- Enables playing, stopping, and adjusting audio settings dynamically.  

### 🎆 **Particle System**  
- Manages **particle effects** with object pooling for optimization.  
- Supports **random particle selection** from predefined lists.  
- Ensures efficient performance with automatic cleanup.  

### ⏳ **Time Manager**  
- Controls **game timers**, **pausing**, and **time freezing**.  
- Provides **critical time threshold detection** for UI warnings.  
- Supports UnityEvent callbacks for timer updates.  

### 🔹 **Pop-Up Manager**  
- Displays **text-based or icon-based pop-ups** with animations.  
- Supports **custom animations** like scaling, fading, and bouncing.  
- Uses object pooling for better efficiency.  

### 🎯 **Input Manager**  
- Handles **mouse clicks and object selection** via raycasting.  
- Supports **selecting, collecting, and dropping objects** dynamically.  
- Uses `ISelectable` and `ICollectable` interfaces for better flexibility.  

### 🏗️ **Object Pooling System**  
- **Reusable object pooling system** for efficient memory management.  
- Supports **dynamic instantiation** and **automatic recycling**.  

### ⏸️ **Pause System**  
- **Manages game pause states**, including UI and logic control.  
- Listens for **Escape key inputs** to trigger pause/resume.  

### 🖥️ **UI Manager**  
- Manages **gameplay UI, settings, win/lose screens**, and more.  
- Provides methods to **toggle different UI panels** dynamically.

 ### 💾 **Save/Load System**
Handles secure data saving and loading with encryption support.
Supports player progress, settings, and game state persistence.
Ensures automatic data backup and error handling for reliability.

### 🏆 **Level Manager**  
- Controls **level progression**, win/lose conditions, and scene management.  
- Integrates with `UIManager` and `AudioManager` through **Zenject**.  

---

## 📥 **How to Install**

### **Via Unity Package Manager**
1. Open Unity and navigate to `Window > Package Manager`.
2. Click the `+` button and select **Add package from Git URL...AC**.
3. Paste this link:  
   `https://github.com/SERAP-KEREM/SerapKeremZenCoreTools.git`
4. Click **Add** and wait for Unity to import the package.

---

## 📜 **License**  
This project is licensed under the MIT License - see the [LICENSE](https://github.com/SERAP-KEREM/SERAP-KEREM/blob/main/MIT%20License.txt) file for details.
