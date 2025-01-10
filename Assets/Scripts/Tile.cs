using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Tile
{
    public TypeTile typeTile;
    [System.Serializable]
    public enum TypeTile
    {
        haveBrick,
        lostBrick,
        wall,
        start,
        end,
        turn,
        none,
    }
    public TypeTile returnType(int num)
    {
        switch (num)
        {
            case 0:
                return TypeTile.haveBrick;
            case 1:
                return TypeTile.lostBrick;
            case 2:
                return TypeTile.wall;
            case 3:
                return TypeTile.start;
            case 4:
                return TypeTile.end;
            case 5: 
                return TypeTile.turn;

            default:
                {
                    Debug.Log("Something fishy here!");
                    return TypeTile.none;
                }


        }
    }
}

