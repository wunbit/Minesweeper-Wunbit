using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileScript : MonoBehaviour
{
    Vector2Int id;
    public bool isMined = false;
    public bool isFlagged = false;
    public bool flaggedMine = false;
    Action<int,int> onClick;
    public bool isClicked = false;
    public Sprite defaultTexture;
    public Sprite[] emptyTextures;
    public Sprite mineTexture;
    public Sprite flagTexture;
    public MineGrid mineGrid;
    public GameObject wincardref;
    public GameObject losecardref;

    void Start()
    {
        isMined = UnityEngine.Random.value < MineGrid.minesPercent;
    }
    public void LoadTexture(int adjacentCount)
    {
        if (isMined)
        {
            GetComponent<SpriteRenderer>().sprite = mineTexture;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount];
        }
    }

    public bool IsCovered()
    {
        if (GetComponent<SpriteRenderer>().sprite.texture.name == "tile")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool SafeTile()
    {
        if (flaggedMine)
        {
            return false;
        }
        if (IsCovered() && !isMined)
        {
            return true;
        }
        return false;
    }
    public void FlagTile()
    {
        GetComponent<SpriteRenderer>().sprite = flagTexture;
        isFlagged = true;
        if (isMined)
        {
            flaggedMine = true;
        }
    }
    public void UnFlagTile()
    {
        GetComponent<SpriteRenderer>().sprite = defaultTexture;
        isFlagged = false;
        if (flaggedMine)
        {
            flaggedMine = false;
        }
    }
    void OnMouseOver()
    {
        if (MineGrid.gameOver == false)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (!isFlagged)
                {
                    if (isMined)
                    {
                        MineGrid.UncoverMines();
                        mineGrid.LoseGame();
                        losecardref.SetActive(true);
                    }
                    else
                    {
                        isClicked = true;
                        int x = (int)transform.position.x;
                        int y = (int)transform.position.y;
                        LoadTexture( MineGrid.AdjacentMines(x, y));
                        MineGrid.FFuncover(x,y, new bool[MineGrid.dimension.x, MineGrid.dimension.y]);
                        MineGrid.FloodedtoClicked();
                        if (MineGrid.IsFinished())
                        {
                            mineGrid.FinishGame();
                        }
                    }
                }
            }
            if(Input.GetMouseButtonDown(1))
            {
                if (!isClicked)
                {
                    if (!isFlagged)
                    {
                        FlagTile();
                        if (MineGrid.IsFinished())
                        {
                            mineGrid.FinishGame();
                        }
                    }
                    else
                    {
                        UnFlagTile();
                    }
                }
            }
        }
    }
}