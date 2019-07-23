using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MineGrid : MonoBehaviour
{
    public TileScript tilePrefab;
    public int columns = 8;
    public int rows = 8;
    public float distanceBetweenTiles = 1.0F;
    public TileScript[,] elements = new TileScript[rows, columns];

    public List <Vector3> gridPositions = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
    {
        CreateTiles();
    }


    void CreateTiles()
    {
        for  (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Instantiate(tilePrefab, new Vector3(x,y,0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
