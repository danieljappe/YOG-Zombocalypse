using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f;
        // Reload the currently active scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartGameWithTag()
    {
        // Find the canvas with the "Respawn" tag
        GameObject canvasObject = GameObject.FindGameObjectWithTag("Respawn");

        // Check if the canvas was found
        if (canvasObject != null)
        {
            // Get the Canvas component from the canvas GameObject
            Canvas canvas = canvasObject.GetComponent<Canvas>();

            // Check if the Canvas component was found
            if (canvas != null)
            {
                // Call the RestartGame method
                RestartGame();
            }
            else
            {
                Debug.LogWarning("Canvas component not found on GameObject with tag 'Respawn'.");
            }
        }
        else
        {
            Debug.LogWarning("GameObject with tag 'Respawn' not found.");
        }
    }
}
