using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SetUpSingleton();


    }

    private void SetUpSingleton()
    {

        if (FindObjectsOfType(GetType()).Length > 1)  // If there is more than 1 Music Player

        { 
            Destroy(gameObject); // destroy the new one
        
            
        }

        else  // if there is not already one
        {

            DontDestroyOnLoad(gameObject);  // Do not destroy this particulat object.
        }




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
