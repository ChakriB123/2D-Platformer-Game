using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public Button ButtonPlay;
    public Button ButtonQuit;
    public Button ButtonLevels;
    public GameObject LevelSelection;

    private void Awake()
    {
        ButtonPlay.onClick.AddListener(playGame);
        ButtonQuit.onClick.AddListener(QuitGame);
        ButtonLevels.onClick.AddListener(onLevelSelection);
    }

    private void playGame()
    {
        SoundManager.Instance.play(SoundsEnum.ButtonClick);
        LevelManager.Instance.SetLevelStatus("Level1", LevelStatus.Unlocked);
        SceneManager.LoadScene("Level1");
    }
    private void onLevelSelection()
    {
        SoundManager.Instance.play(SoundsEnum.ButtonClick);
        LevelSelection.SetActive(true);
    }
    public void QuitGame()
    {
        SoundManager.Instance.play(SoundsEnum.ButtonClick);
        Application.Quit();
    }
}

