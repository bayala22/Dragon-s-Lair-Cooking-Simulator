using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenManager : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main"); // Load the main scene
        Debug.Log("Back to Main Menu button clicked! Loading Main scene.");
    }
}