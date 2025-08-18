using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public Button ButtonPlay;

    private void Awake()
    {
        ButtonPlay.onClick.AddListener(playGame);
    }

    private void playGame()
    {
        SceneManager.LoadScene(1);
    }
}
