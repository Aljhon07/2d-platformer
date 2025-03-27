using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Elements")]
    public GameObject gameOverUI;
    public GameObject victoryUI;

    private bool gameEnded = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }

    private void Start()
    {
        if (gameOverUI) gameOverUI.SetActive(false);
        if (victoryUI) victoryUI.SetActive(false);
    }

    public void GameOver()
    {
        if (gameEnded) return;
        gameEnded = true;

        Debug.Log("Game Over!");
        if (gameOverUI) gameOverUI.SetActive(true);
    }

    public void Victory()
    {
        if (gameEnded) return;
        gameEnded = true;

        Debug.Log("Victory!");
        if (victoryUI) victoryUI.SetActive(true);
    }

    public void RestartGame()
    {

        if (gameOverUI) gameOverUI.SetActive(false);
        if (victoryUI) victoryUI.SetActive(false);
        gameEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        if (gameOverUI) gameOverUI.SetActive(false);
        if (victoryUI) victoryUI.SetActive(false);
        gameEnded = false;
        SceneManager.LoadScene("MenuScene");

    }
}
