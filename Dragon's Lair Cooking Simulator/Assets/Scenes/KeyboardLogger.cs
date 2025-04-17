using UnityEngine;
using TMPro; // For TextMeshPro

public class KeyboardLogger : MonoBehaviour
{
    public TextMeshProUGUI displayText; // Assign a TextMeshProUGUI component in the Inspector
    private string inputString = ""; // Stores the accumulated input

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
            }
        }

        // Handle Backspace
        if (Input.GetKeyDown(KeyCode.Backspace) && inputString.Length > 0)
        {
            inputString = inputString.Substring(0, inputString.Length - 1); // Remove last character
            displayText.text = inputString; // Update UI Text
            Debug.Log("Backspace pressed, string now: " + inputString); // Optional: Log to Console
        }
    }
}