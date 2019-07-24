using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MineGrid : MonoBehaviour
{
    public TileScript tilePrefab;
    public static Vector2Int dimension;
    public int xSize = 10;
    public int ySize = 10;
    [Range(0,100)]
    public int minePercent;
    public static float minesPercent;
    //public int minesPercent;
    public static TileScript[,] cellGrid;

    // Start is called before the first frame update
    void Start()
    {
        minesPercent = (float)minePercent/100;
        dimension.x = xSize;
        dimension.y = ySize;
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

    public static void uncoverMines()
    {
        foreach (TileScript cell in cellGrid)
        {
            if (cell.isMined)
            {
                cell.LoadTexture(0);
            }
        }
    }

    public static bool mineAt(int x, int y)
    {
    if (x >= 0 && y >= 0 && x < dimension.x && y < dimension.y)
    {
        return cellGrid[x,y].isMined;
    }
    return false;
    }

    public static int adjacentMines(int x, int y)
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

    public static void FFuncover (int x, int y, bool[,] visited)
    {
        if (x >= 0 && y >= 0 && x < dimension.x && y < dimension.y)
        {
            if (visited[x,y])
            {
                return;
            }
            cellGrid[x, y].LoadTexture(adjacentMines(x, y));
            if (adjacentMines(x,y) > 0)
            {
                return;
            }
            visited[x,y] = true;
            FFuncover(x-1, y, visited);
            FFuncover(x+1, y, visited);
            FFuncover(x, y-1, visited);
            FFuncover(x, y+1, visited);
        }
    }

    public static bool isFinished() 
    {
        foreach (TileScript cell in cellGrid)
        {
            if (cell.isCovered() && !cell.isMined)
            {
                return false;
            }
        }
        return true;
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
