using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCardScr : MonoBehaviour
{
    public MineGrid mineScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnStartCardClick()
    {
        //Debug.Log("startcard clicked");
        mineScript.StartGame();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
