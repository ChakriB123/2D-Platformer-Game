using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public Button restartButton;    
    public Button MainMenuButton;    

    private void Awake()
    {
        restartButton.onClick.AddListener(ReloadLevel);
        MainMenuButton.onClick.AddListener(MainMenu);
    }
    public void PlayerDied()
    {
        gameObject.SetActive(true);
    }

    private void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
