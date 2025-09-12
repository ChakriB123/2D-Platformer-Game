using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public string[] Levels;
    public static LevelManager Instance { get { return instance; } }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }    
 
    }
    public void Start()
    {
        SetLevelStatus(Levels[0], LevelStatus.Unlocked);
        if (GetLevelStatus(Levels[1]) == LevelStatus.Locked)
        {
            SetLevelStatus(Levels[1], LevelStatus.Unlocked);
        }
    }

    public void MarkCurrentLevelCompelete()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SetLevelStatus(currentScene.name, LevelStatus.Completed);
       

        int currentSceneIndex = Array.FindIndex(Levels , level => level == currentScene.name);
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex < Levels.Length)
        {
            SetLevelStatus(Levels[nextSceneIndex], LevelStatus.Unlocked);
        }
    }

    public LevelStatus GetLevelStatus(string levelName)
    {
        LevelStatus levelstatus = (LevelStatus)PlayerPrefs.GetInt(levelName, 0);
        return levelstatus;
    }

    public void SetLevelStatus(string levelName, LevelStatus levelStatus) {

        PlayerPrefs.SetInt(levelName,(int)levelStatus);
        Debug.Log("setting level: " + levelName + "Level Status:" + levelStatus);
    
    }
}

