using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    [SerializeField] float maxHeight;
    [SerializeField] float minHeight;
    [SerializeField] float maxWidth;
    [SerializeField] float minWidth;
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        MoveCam();
    }
    public void MoveCam()
    {
        //float change = gameObject.transform.position.x;
        float height = player.transform.position.x + -3.5f;
        float width = player.transform.position.z + 0.9f;
        Debug.Log(height);
        this.gameObject.transform.position = new Vector3(Mathf.Clamp(height, minHeight, maxHeight), 4f , Mathf.Clamp(width, minWidth, maxWidth) );
    }
}
