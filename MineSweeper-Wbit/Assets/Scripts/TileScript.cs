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
        isMined = UnityEngine.Random.value < 0.15;
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
        return GetComponent<SpriteRenderer>().sprite.texture.name == "default";
    }

    void OnMouseUpAsButton()
    {
        //Debug.Log();
        LoadTexture(0);
    }

}
