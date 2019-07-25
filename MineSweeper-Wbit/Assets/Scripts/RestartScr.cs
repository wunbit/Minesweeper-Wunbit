using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScr : MonoBehaviour
{
    public MineGrid mineScrptB;
    public GameObject wincardref;
    public GameObject losecardref;
    // Start is called before the first frame update
    void Start()
    {
        
    }

        public void OnRestartCardClick()
    {
        //mineScrptB.HideCards();
        wincardref.SetActive(false);
        losecardref.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
