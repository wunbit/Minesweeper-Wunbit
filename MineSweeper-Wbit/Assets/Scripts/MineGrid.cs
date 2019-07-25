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
    public static bool gameOver = false;
    public static float minesPercent;

    public static TileScript[,] cellGrid;

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
            for  (int x = 0; x < dimension.x; x++)
            {
                for (int y = 0; y < dimension.y; y++)
                {
                TileScript cell = Instantiate(tilePrefab, new Vector3(x,y,0), Quaternion.identity, transform);
                cell.name = string.Format("(x: {0}, y: {1})", x,y);
                cellGrid[x,y] = cell;
                }
            }
        }
    }

    public static void UncoverMines()
    {
        foreach (TileScript cell in cellGrid)
        {
            if (cell.isMined)
            {
                cell.LoadTexture(0);
            }
        }
    }

    public static void FinishGame()
    {
        gameOver=true;
    }

    public static bool MineAt(int x, int y)
    {
    if (x >= 0 && y >= 0 && x < dimension.x && y < dimension.y)
    {
        return cellGrid[x,y].isMined;
    }
    return false;
    }

    public static int AdjacentMines(int x, int y)
    {
        int count = 0;
        if (MineAt(x,   y+1)) ++count; // top
        if (MineAt(x+1, y+1)) ++count; // top-right
        if (MineAt(x+1, y  )) ++count; // right
        if (MineAt(x+1, y-1)) ++count; // bottom-right
        if (MineAt(x,   y-1)) ++count; // bottom
        if (MineAt(x-1, y-1)) ++count; // bottom-left
        if (MineAt(x-1, y  )) ++count; // left
        if (MineAt(x-1, y+1)) ++count; // top-left
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
            cellGrid[x, y].LoadTexture(AdjacentMines(x, y));
            if (AdjacentMines(x,y) > 0)
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

    public static bool IsFinished()
    {
        foreach (TileScript cell in cellGrid)
        {
            if (cell.SafeTile())
            {
                return false;
            }
        }
        return true;
    }

    public static void FloodedtoClicked()
    {
        foreach (TileScript cell in cellGrid)
        {
            if (cell.IsCovered() == false)
            {
                cell.isClicked = true;
            }
        }
    }



}
