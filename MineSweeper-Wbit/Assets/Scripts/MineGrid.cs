using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MineGrid : MonoBehaviour
{
    public TileScript tilePrefab;
    public Vector2Int dimension;
    public int xSize = 10;
    public int ySize = 10;
    public int minesPercent;
    public TileScript[,] cellGrid;

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

    public void uncoverMines()
    {
        foreach (TileScript cell in cellGrid)
        {
            if (cell.isMined)
            {
                cell.LoadTexture(0);
            }
        }
    }

    public bool mineAt(int x, int y) 
    {
    if (x >= 0 && y >= 0 && x < dimension.x && y < dimension.y)
    {
        return cellGrid[x, y].isMined;
    }
    return false;
    }

    public int adjacentMines(int x, int y)
    {
        int count = 0;
        if (mineAt(x,   y+1)) ++count; // top
        if (mineAt(x+1, y+1)) ++count; // top-right
        if (mineAt(x+1, y  )) ++count; // right
        if (mineAt(x+1, y-1)) ++count; // bottom-right
        if (mineAt(x,   y-1)) ++count; // bottom
        if (mineAt(x-1, y-1)) ++count; // bottom-left
        if (mineAt(x-1, y  )) ++count; // left
        if (mineAt(x-1, y+1)) ++count; // top-left
        return count;
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
