using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOverController : MonoBehaviour
{
    [SerializeField] string LoadLevel;
    public GameObject LevelCompletePanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("LevelComplete");      
            LevelCompletePanel.SetActive(true);
            Invoke("AfterLevelComplete", 2f);
          
        }
    }
    public void AfterLevelComplete()
    {
        LevelManager.Instance.MarkCurrentLevelCompelete();
        SceneManager.LoadScene(LoadLevel);
    }
}
