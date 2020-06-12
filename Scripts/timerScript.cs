using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;

public class timerScript : MonoBehaviour
{
    public Text gameTimerText;
    public float maxTimeForGame;
    public float timeForGame;
    // Start is called before the first frame update
    void Start()
    {
        timeForGame = maxTimeForGame;//Sets the max time for the game
    }

    // Update is called once per frame
    void Update()
    {
        //Decreases the value - time dependent
        maxTimeForGame -= Time.deltaTime;
        int seconds = (int)(maxTimeForGame % 60);//Calculates the seconds with the max value of 59
        int minutes = (int)(maxTimeForGame / 60) % 60;//Calculates the minutes
        string timerFormat = string.Format("{0:0}:{1:00}", minutes, seconds);//Formats the time
        gameTimerText.text = timerFormat;
        //Prevents the timer from going below 0 
        if(maxTimeForGame <= 0)
        {
            maxTimeForGame = 0;
        }
    }
}
