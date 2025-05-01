using UnityEngine;
using UnityEngine.UI; // For UI Image and Outline
using UnityEngine.SceneManagement; // For scene transitions
using TMPro; // For TextMeshPro

public class KeyboardLogger : MonoBehaviour
{
    public TextMeshProUGUI displayText; // Assign TextMeshProUGUI component in the Inspector
    public Image onionImage; // Assign the onion UI Image in the Inspector
    private string inputString = ""; // Stores the accumulated input
    private Outline onionOutline; // Reference to the Outline component

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
    }

    void Update()
    {
        // Handle letter inputs
        foreach (char c in Input.inputString)
        {
            if (char.IsLetter(c))
            {
                inputString += c; // Append the letter
                displayText.text = inputString; // Update UI Text
                Debug.Log("Letter pressed: " + c); // Log to Console
                CheckForOnion(); // Check if "ONION" is typed
            }
        }

        // Handle Backspace
        if (Input.GetKeyDown(KeyCode.Backspace) && inputString.Length > 0)
        {
            inputString = inputString.Substring(0, inputString.Length - 1); // Remove last character
            displayText.text = inputString; // Update UI Text
            Debug.Log("Backspace pressed, string now: " + inputString); // Log to Console
            CheckForOnion(); // Check if "ONION" is still valid
        }

        // Handle W key for scene transition
        if (Input.GetKeyDown(KeyCode.W))
        {
            SceneManager.LoadScene("WinScreen"); // Load the win screen scene
            Debug.Log("W pressed! Loading WinScreen scene.");
        }
    }

    void CheckForOnion()
    {
        if (onionImage != null && onionOutline != null)
        {
            // Check if inputString matches "ONION" (case-insensitive)
            onionOutline.enabled = inputString.ToUpper() == "ONION";
            if (onionOutline.enabled)
            {
                Debug.Log("ONION typed! Enabling outline on onion Image.");
            }
        }
    }
}