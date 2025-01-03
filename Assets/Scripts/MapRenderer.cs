using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MapRenderer : MonoBehaviour
{
    [SerializeField] string path;
    [SerializeField] string[] listString;
    [SerializeField] GameObject brickPrefab; 
    [SerializeField] GameObject wallPrefab;
    [SerializeField] GameObject autoTurnPrefab;
    [SerializeField] GameObject loseBrickPrefab;    
    [SerializeField] GameObject startPrefab;    
    [SerializeField] GameObject endPrefab;

    Vector3 getStartPoint;
    protected GameObject player;
   
    Tile tile = new Tile();

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        loadString(path);
        Genmap();
        player.transform.position = getStartPoint;
    }


    void Update()
    {

    }

    public void  Renderer (Tile.TypeTile type, int x , int z)
    {
        if(type == Tile.TypeTile.wall)
        {
            Instantiate(wallPrefab, new Vector3(x, 0 , z), this.transform.rotation, this.transform);
        }
        else if(type == Tile.TypeTile.haveBrick)
        {
            GameObject newobj = Instantiate(brickPrefab, new Vector3(x, 0, z), this.transform.rotation, this.transform);
            newobj.name = x.ToString() + z.ToString();
        }
        else if (type == Tile.TypeTile.start)
        {
            getStartPoint = new Vector3(x, 0 , z);
            Instantiate(startPrefab, new Vector3(x, 0, z), this.transform.rotation, this.transform);
            
        }
        else if (type == Tile.TypeTile.lostBrick)
        {
            Instantiate(loseBrickPrefab, new Vector3(x, 0, z), this.transform.rotation, this.transform);
        }
        else if (type == Tile.TypeTile.end)
        {
            Instantiate(endPrefab, new Vector3(x, 0, z), this.transform.rotation, this.transform);
        }
    }
    protected void loadString(string filePath)
    {
        
        string content = File.ReadAllText(filePath);
        listString = content.Split("\r\n" );
    }
    protected void Genmap()
    {
        for( int j = 0; j < listString.Length; j ++)
        {
            string textIndex = listString[j];  
            for (var i = 0; i < textIndex.Length; i++) {
                int num = int.Parse(textIndex[i].ToString());
                Renderer(tile.returnType(num), j , i);
            }
        }
    }
}
