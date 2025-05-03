using UnityEngine;
using UnityEngine.UI; // For UI Image and Outline
using UnityEngine.SceneManagement; // For scene transitions
using TMPro; // For TextMeshPro

public class KeyboardLogger : MonoBehaviour
{
    public TextMeshProUGUI displayText; // Assign TextMeshProUGUI for typed input in the Inspector
    public TextMeshProUGUI popupText; // Assign TextMeshProUGUI for pop-up in the Inspector
    public Image onionImage; // Assign the onion UI Image in the Inspector
    private string inputString = ""; // Stores the accumulated input
    private Outline onionOutline; // Reference to the Outline component
    private bool hasTypedOnion = false; // Tracks if "ONION" has been typed

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

        // Handle W key for scene transition
        if (Input.GetKeyDown(KeyCode.W))
        {
            SceneManager.LoadScene("WinScreen"); // Load the win screen scene
            Debug.Log("W pressed! Loading WinScreen scene.");
        }
    }

    void CheckForOnionAndChop()
    {
        if (onionImage != null && onionOutline != null)
        {
            // Check if inputString matches "ONION" (case-insensitive)
            bool isOnion = inputString.ToUpper() == "ONION";
            onionOutline.enabled = isOnion;
            if (isOnion && !hasTypedOnion)
            {
                hasTypedOnion = true; // Mark "ONION" as typed
                Debug.Log("ONION typed! Enabling outline on onion Image.");
            }
        }

        // Update pop-up text based on state
        if (popupText != null)
        {
            if (hasTypedOnion)
            {
                // After typing "ONION", show "TYPE 'CHOP'" until "CHOP" is typed
                popupText.text = "TYPE 'CHOP'";
                popupText.enabled = inputString.ToUpper() != "CHOP";
            }
            else
            {
                // Before typing "ONION", show "TYPE 'ONION'"
                popupText.text = "TYPE 'ONION'";
                popupText.enabled = inputString.ToUpper() != "ONION";
            }
        }
    }
}