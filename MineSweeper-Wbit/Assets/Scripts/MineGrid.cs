using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MineGrid : MonoBehaviour
{
    public TileScript tilePrefab;
    public Vector2Int dimension;
    public int minesPercent;
    public static TileScript[,] cellGrid;

    // Start is called before the first frame update
    void Start()
    {
        CreateTiles();
    }



    void CreateTiles()
    {
        if (cellGrid == null)
        {
            cellGrid = new TileScript[dimension.x, dimension.y];
            CellGridLoop((x, y) =>
            {
                TileScript cell = Instantiate(tilePrefab, new Vector3(x,y,0), Quaternion.identity, transform);
                cell.name = string.Format("(x: {0}, y: {1})", x,y);
                cellGrid[x,y] = cell;
            });
        }
    }


    void Activate(int x, int y)
    {
       // print(string.Format("(x: {0}, y: {1})", x,y));
    }

    void CellGridLoop(Action<int, int> e)
    {
        for  (int x = 0; x < dimension.x; x++)
            {
                for (int y = 0; y < dimension.y; y++)
                {
                    e(x,y);
                }
            }
    }

}
