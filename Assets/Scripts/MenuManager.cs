// MenuManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField NameInputField; // Reference to the Input Field for the player's name

    public void StartGame()
    {
        // Save the player's name to GameDataX
        if (NameInputField != null && !string.IsNullOrEmpty(NameInputField.text))
        {
            GameData.PlayerName = NameInputField.text; // Updated to GameDataX
        }
        else
        {
            GameData.PlayerName = "Player"; // Updated to GameDataX
        }
        SceneManager.LoadScene(1); // Load the Main scene
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}