using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    Text scoreText;
    GameSession gameSession;

    void Start()
    {
        scoreText = GetComponent<Text>();  //Serializefield yerine bunları kullandık.
        gameSession = FindObjectOfType<GameSession>(); //Serializefield yerine bunları kullandık.

    }

    // Update is called once per frame
    void Update()
    {

        scoreText.text = gameSession.GetScore().ToString();

        /*
         ScoreDisplay, ScoreText'e attachlendiği için, direkt burdan skoru çekip göstermemizi sağlıyor.


         * */
    }
}
