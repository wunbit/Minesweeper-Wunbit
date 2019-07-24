using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileScript : MonoBehaviour
{
    Vector2Int id;
    public bool isMined = false;
    Action<int,int> onClick;
    public bool isClicked = false;
    public Sprite[] emptyTextures;
    public Sprite mineTexture;
    public Sprite flagTexture;
    //public MineGrid mineGrid;

    void Start()
    {
        isMined = UnityEngine.Random.value < MineGrid.minesPercent;
        //int x = (int)transform.position.x;
        //int y = (int)transform.position.y;
        //MineGrid.cellGrid[x,y] = this;
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

    public bool isCovered()
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

    void OnMouseUpAsButton()
    {
        
    }


    void OnMouseOver()
    {
             if(Input.GetMouseButtonDown(0))
        {
            if (isMined)
            {
                MineGrid.uncoverMines();
                print("you lose");
            }
            else
            {
                int x = (int)transform.position.x;
                int y = (int)transform.position.y;
                LoadTexture( MineGrid.adjacentMines(x, y));
                MineGrid.FFuncover(x,y, new bool[MineGrid.dimension.x, MineGrid.dimension.y]);
                if (MineGrid.isFinished())
                {
                    print("you win");
                }
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            GetComponent<SpriteRenderer>().sprite = flagTexture;
        }
    }}