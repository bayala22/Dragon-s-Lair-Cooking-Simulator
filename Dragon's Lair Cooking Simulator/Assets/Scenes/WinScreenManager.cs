using UnityEngine;
using UnityEngine.SceneManagement; // For scene transitions

public class WinScreenManager : MonoBehaviour
{
    public void GoToTitleScreen()
    {
        SceneManager.LoadScene("Main"); // Replace with your main scene’s name
        Debug.Log("Main Menu button pressed! Loading Main.");
    }
}