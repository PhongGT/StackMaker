using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MapRenderer : MonoBehaviour
{
    public static MapRenderer Instance;
    [SerializeField] string path;
    
    [SerializeField] string[] listString;
    [SerializeField] GameObject brickPrefab; 
    [SerializeField] GameObject wallPrefab;
    [SerializeField] GameObject autoTurnPrefab;
    [SerializeField] GameObject loseBrickPrefab;    
    [SerializeField] GameObject startPrefab;    
    [SerializeField] GameObject endPrefab;
    [SerializeField]List<GameObject> list;

    Vector3 getStartPoint;
    protected GameObject player;
   
    Tile tile = new Tile();

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        list = new List<GameObject>();
    }
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        NewMap(CombinePath());
    }
    
    public void  Renderer (Tile.TypeTile type, int x , int z)
    {
        GameObject newobj;
        if (type == Tile.TypeTile.wall)
        {
            newobj = Instantiate(wallPrefab, new Vector3(x, 0 , z), this.transform.rotation, this.transform);
            list.Add(newobj);
        }
        else if(type == Tile.TypeTile.haveBrick)
        {
            newobj = Instantiate(brickPrefab, new Vector3(x, 0, z), this.transform.rotation, this.transform);
            newobj.name = x.ToString() + z.ToString(); list.Add(newobj);

        }
        else if (type == Tile.TypeTile.start)
        {
            getStartPoint = new Vector3(x, 0 , z);
            newobj = Instantiate(startPrefab, new Vector3(x, 0, z), this.transform.rotation, this.transform);
            list.Add(newobj);

        }
        else if (type == Tile.TypeTile.lostBrick)
        {
            newobj = Instantiate(loseBrickPrefab, new Vector3(x, 0, z), this.transform.rotation, this.transform);
            list.Add(newobj);
        }
        else if (type == Tile.TypeTile.end)
        {
            newobj = Instantiate(endPrefab, new Vector3(x, 0, z), this.transform.rotation, this.transform);
            list.Add(newobj);
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
    public string CombinePath()
    {
        string test = (path + "test" + GameManager.Instance.currentmap.ToString() + ".txt").ToString();
        Debug.Log(test);
        return test;
    }
    public void NewMap(string path)
    {
        loadString(path);
        Genmap();
        player.transform.position = getStartPoint;
    }
}
