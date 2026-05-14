// 1. IInteractable.cs
using UnityEngine;

// Any script that implements this interface can be interacted with by the player.
public interface IInteractable
{
    // The text that will show up on the UI (e.g., "Open Chest", "Talk to Blacksmith")
    string InteractionPrompt { get; }
    
    // The method called when the player presses the interact button
    // Returns true if the interaction was successful
    bool Interact(Interactor interactor);
}
