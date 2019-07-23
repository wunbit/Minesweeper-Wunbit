using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public bool isMined = false;
    public Sprite[] emptyTextures;
    public Sprite mineTexture;

    
    // Start is called before the first frame update
    void Start()
    {
        isMined = Random.value < 0.15;
    }

    public void loadTexture(int adjacentCount) 
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
        if (isMined)
        {

        }
        else
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
