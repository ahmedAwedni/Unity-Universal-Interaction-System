// 3. InteractableObject.cs
using UnityEngine;
using UnityEngine.Events;

// A generic component you can attach to chests, doors, or items.
// It implements IInteractable and exposes UnityEvents so designers can hook up logic in the inspector.

public class InteractableObject : MonoBehaviour, IInteractable
{
    [Header("UI Settings")]
    [SerializeField] private string prompt = "Interact";
    public string InteractionPrompt => prompt;

    [Header("Events")]
    [Tooltip("Fires when the player successfully interacts with this object.")]
    public UnityEvent onInteract;

    [Header("Settings")]
    [Tooltip("If true, the object can only be interacted with once (e.g., picking up a coin).")]
    public bool isSingleUse = false;
    private bool hasBeenUsed = false;

    public bool Interact(Interactor interactor)
    {
        if (isSingleUse && hasBeenUsed) return false;

        // Fire any logic hooked up in the inspector (Open door, give item, etc.)
        onInteract?.Invoke();

        if (isSingleUse)
        {
            hasBeenUsed = true;
            // Optionally disable the collider so the player can't look at it anymore
            Collider col = GetComponent<Collider>();
            if (col != null) col.enabled = false;
        }

        return true;
    }
}
