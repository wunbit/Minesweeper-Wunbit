using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


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
    public GameObject downBorder;
    public GameObject topBorder;
    public GameObject leftBorder;
    public GameObject rightBorder;
    public GameObject topLeftBorder;
    public GameObject topRightBorder;
    public GameObject downRightBorder;
    public GameObject downLeftBorder;
    public GameObject startCard;
    public GameObject winCard;
    public GameObject loseCard;

    void Start()
    {
        minesPercent = (float)minePercent/100;
        dimension.x = xSize;
        dimension.y = ySize;
        StartGame();
    }

    public void OnStartCardClick()
    {
        Debug.Log("startcard clicked");
        startCard.SetActive(false);
        //StartGame();
    }

    public void OnRestartCardClick()
    {
        winCard.SetActive(false);
        loseCard.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        
        CreateTiles();
        CreateBorder();
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

        void CreateBorder()
    {
        int bordertop = dimension.y + 1;
        int borderright = dimension.x + 1;
        for  (int x = -1; x < borderright; x++)
        {
            for (int y = -1; y < bordertop; y++)
            {
                if (x == -1 && y == -1)
                {
                    Debug.Log("downleft border true");
                    Instantiate(downLeftBorder, new Vector3(x,y, 0), Quaternion.identity, transform);
                }
                else if (x == -1 && y == bordertop - 1)
                {
                    Debug.Log("topleft border true");
                    Instantiate(topLeftBorder, new Vector3(x,y, 0), Quaternion.identity, transform);
                }
                else if (x == borderright -1 && y == -1)
                {
                    Instantiate(downRightBorder, new Vector3(x,y, 0), Quaternion.identity, transform);
                }
                else if (x == borderright - 1 && y == bordertop - 1)
                {
                    Instantiate(topRightBorder, new Vector3(x,y, 0), Quaternion.identity, transform);
                }
                else if (x == -1)
                {
                    Instantiate(leftBorder, new Vector3(x,y, 0), Quaternion.identity, transform);
                }
                else if (x == borderright - 1)
                {
                    Instantiate(rightBorder, new Vector3(x,y, 0), Quaternion.identity, transform);
                }
                else if (y == -1)
                {
                    Instantiate(downBorder, new Vector3(x,y, 0), Quaternion.identity, transform);
                }
                else if (y == bordertop - 1)
                {
                    Instantiate(topBorder, new Vector3(x,y, 0), Quaternion.identity, transform);
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
