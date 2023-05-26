using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameControls : MonoBehaviour
{
    
    void Start()
    {
        
    }

   
     void Update()
    {
        if (Input.anyKey)
        {
            //StartGame();
            StarThing();
        
        }
    }

    void StarThing()
    {
        SceneManager.LoadScene("Game");

    }
}
