using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Next_Btn : MonoBehaviour
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
        //click.callback.AddListener(NextMap);
    }
  
    public void NextMap()
    {
        GameManager.Instance.currentmap = 2;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.Instance.Continue();
    }
}
