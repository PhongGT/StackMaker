using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class Retry_Btn : MonoBehaviour
{
    public EventTrigger eventTrigger;

    private void Awake()
    {
        eventTrigger = GetComponent<EventTrigger>();
       
    }
    private void Start()
    {
        //EventTrigger.Entry click = new EventTrigger.Entry()
        //{
        //    eventID = EventTriggerType.PointerDown,
        //};
        //click.callback.AddListener(RetryMap);
    }

    public void RetryMap()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.Instance.Continue();
    }
}
