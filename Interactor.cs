// 2. Interactor.cs
using System;
using UnityEngine;

// Attach this to the Player's Camera (for FPS/TPS) or the Player's body (for Top-Down).

public class Interactor : MonoBehaviour
{
    // Events for the UI to listen to
    public static event Action<string> OnTargetChange; 

    [Header("Interaction Settings")]
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    // Internal state
    private IInteractable currentTarget;

    private void Update()
    {
        FindInteractable();

        if (Input.GetKeyDown(interactKey) && currentTarget != null)
        {
            currentTarget.Interact(this);
        }
    }

    private void FindInteractable()
    {
        // Cast a sphere forward to find objects on the Interactable layer
        bool found = Physics.SphereCast(
            interactionPoint.position, 
            0.3f, 
            interactionPoint.forward, 
            out RaycastHit hit, 
            interactionDistance, 
            interactableMask
        );

        if (found)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            // If we found a new interactable, update the target and tell the UI
            if (interactable != null && interactable != currentTarget)
            {
                currentTarget = interactable;
                OnTargetChange?.Invoke(currentTarget.InteractionPrompt);
            }
        }
        else
        {
            // If we are looking at nothing, clear the target and hide the UI
            if (currentTarget != null)
            {
                currentTarget = null;
                OnTargetChange?.Invoke(null); 
            }
        }
    }

    // Draws a helpful red line in the scene view to debug your interaction range
    private void OnDrawGizmos()
    {
        if (interactionPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position + (interactionPoint.forward * interactionDistance), 0.3f);
    }
}
