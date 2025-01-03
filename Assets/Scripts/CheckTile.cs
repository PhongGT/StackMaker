using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CheckTile : MonoBehaviour
{
    // Start is called before the first frame update
    int point = 0;
    void Start()
    {
        if(!PlayerPrefs.HasKey("Point"))
        { PlayerPrefs.SetInt("Point", point);}
        point = PlayerPrefs.GetInt("Point");
        
       
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Brick")) 
            {
            point = point + 1;
          
        }
    }
   
}
