using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Quit button clicked! Application would quit in a build.");
        Application.Quit();
    }
}