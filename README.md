# Unity Universal Interaction System

A highly modular, interface-driven Interaction System for Unity. Perfect for FPS, TPS, or Top-Down games, this framework allows players to look at and interact with chests, doors, NPCs, and items without relying on messy tags or hard-coded collision checks.

---

## ✨ Features

* **Interface-Driven Architecture:** Relies on the "IInteractable" interface. The Player script doesn't need to know what a "Chest" or a "Door" is; it simply asks the object to execute its own logic.
* **Smart UI Prompts:** The system automatically broadcasts the specific object's prompt (e.g., "Press [E] to Read Sign" vs. "Press [E] to Pick Up Sword") to your Canvas using a decoupled Event Bus.
* **Generic UnityEvent Wrapper:** Includes an "InteractableObject" MonoBehaviour. Designers can drop this onto any 3D model and use UnityEvents to trigger animations, sounds, or inventory additions without writing a new script.
* **Single-Use Toggle:** Built-in support for single-use interactions (like grabbing a health potion) that automatically disables further interaction once triggered.

---

## 🧠 Design Notes

Beginners often handle interactions by adding massive "OnTriggerEnter" blocks to the Player script. This creates a "God Object" anti-pattern where the Player script is responsible for opening doors, managing dialogue, and looting chests.

This system uses the **Command Pattern** approach via the "IInteractable" interface. 
The "Interactor" (Player) casts a physics ray forward. If it hits an object on the Interactable layer, it reads the "InteractionPrompt" and tells the UI to display it. When the player presses 'E', the "Interactor" simply calls "Interact()". The object itself holds the logic for what happens next, keeping your Player script lightweight and strictly focused on movement.

---

## 📂 Included Scripts

* "IInteractable.cs" - The interface contract that any interactive object must implement.
* "Interactor.cs" - The script attached to the Player/Camera. Handles raycasting, user input, and broadcasting UI updates.
* "InteractableObject.cs" - A generic, ready-to-use component for basic objects. Allows designers to map logic via UnityEvents.
* "InteractionUI.cs" - A Canvas script that listens to the Interactor and smoothly fades/toggles the text prompts on screen.

---

## 🧩 How To Use

1. **Setup the Layers:** Go to the top right of the Unity Editor and add a new Layer called "Interactable".
2. **Setup the Player:** Create an empty GameObject inside your Player's Camera (or body) and name it "InteractPoint". Attach the "Interactor.cs" script to your Player. Assign the "InteractPoint", set the distance to 3, and set the LayerMask to your new "Interactable" layer.
3. **Setup the UI:** Add a UI Panel and Text element to your Canvas. Attach "InteractionUI.cs" to the Canvas and link the Panel and Text references.
4. **Create an Object:** Place a Cube in your scene (make sure it has a Collider). Set its Layer to "Interactable". Attach the "InteractableObject.cs" script.
5. **Configure the Object:** Change the prompt to "Open Box". In the UnityEvent box, drag the Cube itself in and select "GameObject.SetActive" to false. 
6. **Play:** Walk up to the box, see the "Press [E] to Open Box" prompt appear, press E, and watch the box disappear!

---

## 🚀 Possible Extensions

* **Hold-to-Interact:** Modify the "Interactor" update loop to check "Input.GetKey" and fill a radial UI progress bar before triggering the "Interact()" method.
* **Custom Implementations:** Instead of using the generic "InteractableObject", create specific scripts (like "NPC.cs") that inherit from "MonoBehaviour" AND implement "IInteractable" to trigger your **Dialogue System**.
* **Outline Highlighting:** Add logic to the "Interactor" so that when "currentTarget != null", it grabs the MeshRenderer of the target and applies a glowing outline shader material.

---

## 🛠 Unity Version

Tested in Unity6+ (should work without any problems in newer versions).

---

## 📜 License

MIT
