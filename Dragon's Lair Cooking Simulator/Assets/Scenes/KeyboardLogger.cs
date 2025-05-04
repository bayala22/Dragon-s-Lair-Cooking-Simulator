using UnityEngine;
using UnityEngine.UI; // For UI Image and Outline
using UnityEngine.SceneManagement; // For scene transitions
using TMPro; // For TextMeshPro

public class KeyboardLogger : MonoBehaviour
{
    public TextMeshProUGUI displayText; // Assign TextMeshProUGUI for typed input in the Inspector
    public TextMeshProUGUI popupText; // Assign TextMeshProUGUI for pop-up in the Inspector
    public Image onionImage; // Assign the original onion UI Image in the Inspector
    public Image choppedOnionImage; // Assign the chopped onion UI Image in the Inspector
    public Image cuttingBoardImage; // Assign the cutting board UI Image in the Inspector
    private string inputString = ""; // Stores the accumulated input
    private Outline onionOutline; // Reference to the onion's Outline component
    private Outline cuttingBoardOutline; // Reference to the cutting board's Outline component
    private bool hasTypedOnion = false; // Tracks if "ONION" has been typed
    private bool hasTypedChop = false; // Tracks if "CHOP" has been typed

    void Start()
    {
        // Get the Outline component from the onion Image
        if (onionImage != null)
        {
            onionOutline = onionImage.GetComponent<Outline>();
            if (onionOutline != null)
            {
                onionOutline.enabled = false; // Ensure outline is off initially
            }
            else
            {
                Debug.LogError("Onion Image needs an Outline component.");
            }
        }
        else
        {
            Debug.LogError("Onion Image is not assigned in the Inspector.");
        }

        // Initialize chopped onion Image
        if (choppedOnionImage != null)
        {
            choppedOnionImage.enabled = false; // Ensure chopped onion is hidden initially
        }
        else
        {
            Debug.LogError("Chopped Onion Image is not assigned in the Inspector.");
        }

        // Get the Outline component from the cutting board Image
        if (cuttingBoardImage != null)
        {
            cuttingBoardOutline = cuttingBoardImage.GetComponent<Outline>();
            if (cuttingBoardOutline != null)
            {
                cuttingBoardOutline.enabled = false; // Ensure outline is off initially
            }
            else
            {
                Debug.LogError("Cutting Board Image needs an Outline component.");
            }
        }
        else
        {
            Debug.LogError("Cutting Board Image is not assigned in the Inspector.");
        }

        // Initialize pop-up text
        if (popupText != null)
        {
            popupText.text = "TYPE 'ONION'"; // Set initial pop-up text
        }
        else
        {
            Debug.LogError("Popup Text is not assigned in the Inspector.");
        }
    }

    void Update()
    {
        // Handle letter inputs
        foreach (char c in Input.inputString)
        {
            if (char.IsLetter(c))
            {
                inputString += c; // Append the letter
                displayText.text = inputString; // Update input UI Text
                Debug.Log("Letter pressed: " + c); // Log to Console
                CheckForOnionAndChop(); // Check for "ONION" or "CHOP"
            }
        }

        // Handle Backspace
        if (Input.GetKeyDown(KeyCode.Backspace) && inputString.Length > 0)
        {
            inputString = inputString.Substring(0, inputString.Length - 1); // Remove last character
            displayText.text = inputString; // Update input UI Text
            Debug.Log("Backspace pressed, string now: " + inputString); // Log to Console
            CheckForOnionAndChop(); // Check for "ONION" or "CHOP"
        }

        // Handle W key for WinScreen transition
        if (Input.GetKeyDown(KeyCode.W))
        {
            SceneManager.LoadScene("WinScreen"); // Load the win screen scene
            Debug.Log("W pressed! Loading WinScreen scene.");
        }

        // Handle Q key for LoseScreen transition
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("LoseScreen"); // Load the lose screen scene
            Debug.Log("Q pressed! Loading LoseScreen scene.");
        }
    }

    void CheckForOnionAndChop()
    {
        // Check for onion outline
        if (onionImage != null && onionOutline != null)
        {
            bool isOnion = inputString.ToUpper() == "ONION";
            onionOutline.enabled = isOnion && !hasTypedChop; // Disable outline if CHOP is typed
            if (isOnion && !hasTypedOnion)
            {
                hasTypedOnion = true; // Mark "ONION" as typed
                inputString = ""; // Clear input
                displayText.text = inputString; // Clear input UI Text
                Debug.Log("ONION typed! Enabling outline on onion Image and clearing typebox.");
            }
        }

        // Check for cutting board outline and onion swap
        if (cuttingBoardImage != null && cuttingBoardOutline != null && choppedOnionImage != null)
        {
            bool isChop = inputString.ToUpper() == "CHOP";
            cuttingBoardOutline.enabled = hasTypedOnion && isChop;
            if (isChop && hasTypedOnion && !hasTypedChop)
            {
                hasTypedChop = true; // Mark "CHOP" as typed
                inputString = ""; // Clear input
                displayText.text = inputString; // Clear input UI Text
                if (onionImage != null)
                {
                    onionImage.enabled = false; // Hide original onion
                }
                choppedOnionImage.enabled = true; // Show chopped onion
                Debug.Log("CHOP typed after ONION! Enabling outline on cutting board Image, hiding onion, showing chopped onion, and clearing typebox.");
            }
        }

        // Update pop-up text based on state
        if (popupText != null)
        {
            if (hasTypedOnion)
            {
                // After typing "ONION", show "TYPE 'CHOP'" until "CHOP" is typed
                popupText.text = "TYPE 'CHOP'";
                popupText.enabled = !hasTypedChop;
            }
            else
            {
                // Before typing "ONION", show "TYPE 'ONION'"
                popupText.text = "TYPE 'ONION'";
                popupText.enabled = true;
            }
        }
    }
}