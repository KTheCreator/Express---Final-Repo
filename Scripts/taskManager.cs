using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class taskManager : MonoBehaviour
{
    public int challengeLimit,challengeCount;
    public string challengeType;
    public Text taskInfo;
    // Start is called before the first frame update
    void Awake()
    {
        //Chooses the challenge type
        int randomChallenge = Random.Range(0, 100);
        challengeCount = 0;
        //Concentrating on the shelves 
        if (randomChallenge >= 40)
        {
            challengeType = "shelves";
            challengeLimit = Random.Range(2, 11);
        }
        //Concentrating on the customers
        else
        {
            challengeType = "customers";
            challengeLimit = Random.Range(1, 5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Displays and updates the progression of the objective depending on the type of objective set.
        if(challengeType == "shelves")
        {
            //Formatting the text
            string taskFormat = string.Format("Fill {0:00} Shelves: \n {1:00}/{2:00}", challengeLimit, challengeCount, challengeLimit);
            taskInfo.text = taskFormat;
        }
        else
        {
            //Formatting the text
            string taskFormat = string.Format("Help {0:00} Customers: \n {1:00}/{2:00}", challengeLimit, challengeCount, challengeLimit);
            taskInfo.text = taskFormat;
        }
    }
}
