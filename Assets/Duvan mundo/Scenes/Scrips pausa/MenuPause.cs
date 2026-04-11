using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPausa : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject pauseButton;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        PauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        PauseMenu.SetActive(false);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("cerrando juego");
        Application.Quit();
    }
}
