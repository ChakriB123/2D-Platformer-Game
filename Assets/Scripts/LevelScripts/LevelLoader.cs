using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelLoader : MonoBehaviour
{
    private Button button;
    public string levelName;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(onclick);
    }

    private void onclick()
    {
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(levelName);
        switch(levelStatus)
            {
            case LevelStatus.Locked:
                Debug.Log("can't play this level locked");
                break;
            case LevelStatus.Unlocked:
                SoundManager.Instance.play(SoundsEnum.ButtonClick);
                SceneManager.LoadScene(levelName);
                break;
            case LevelStatus.Completed:
                SoundManager.Instance.play(SoundsEnum.ButtonClick);
                SceneManager.LoadScene(levelName);
                break;
        }
       //SceneManager.LoadScene(levelName);
    }
}
