using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInFront : MonoBehaviour
{
    public GameObject curGameObjt;
    public GameObject lateObject;

    private void OnTriggerEnter(Collider other)
    {
        if (curGameObjt != null)
        if (curGameObjt.CompareTag("Brick")||curGameObjt.CompareTag("LoseBrick"))
        {
                
                lateObject = curGameObjt;
                Debug.Log("EnterTrigger" + lateObject.name);
        }
       
        curGameObjt = other.gameObject;
        
    }
}
