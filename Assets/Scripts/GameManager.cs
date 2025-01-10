using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
 
    public int currentmap = 1;
    [SerializeField] GameObject finishPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(Instance);
        finishPanel = GameObject.Find("Canvas");
    }
    private void Start()
    {
        Debug.Log("LoadScene");
        //currentmap = PlayerPrefs.GetInt("map");
        
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }
    public void Continue()
    {
        Time.timeScale = 1f;
    }
    public void ReachFinish()
    {
        StopGame();
        UI_Manager.Instance.ShowPanel();

    }    
}
