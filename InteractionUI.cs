// 4. InteractionUI.cs
using UnityEngine;
using UnityEngine.UI;

// Attach this to a UI Canvas. It listens to the Interactor and displays the prompts.

public class InteractionUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject promptPanel;
    [SerializeField] private Text promptText; // Swap to TextMeshProUGUI if using TMP

    [Header("Settings")]
    [SerializeField] private string defaultKeyText = "[E]";

    private void OnEnable()
    {
        Interactor.OnTargetChange += UpdatePrompt;
    }

    private void OnDisable()
    {
        Interactor.OnTargetChange -= UpdatePrompt;
    }

    private void Start()
    {
        promptPanel.SetActive(false);
    }

    private void UpdatePrompt(string newPrompt)
    {
        if (string.IsNullOrEmpty(newPrompt))
        {
            promptPanel.SetActive(false);
        }
        else
        {
            promptPanel.SetActive(true);
            promptText.text = $"{defaultKeyText} {newPrompt}";
        }
    }
}
