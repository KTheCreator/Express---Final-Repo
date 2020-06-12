using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;
public class gameMangerScript : MonoBehaviour
{
    timerScript clock;
    PlayerMovement player;
    taskManager task;
    public GameObject endReport;
    public int score = 0;
    public Text scoreText, wagesText, taskText;
    public float wages, hourlyRate;
    public GameObject mainMenuB;
    public messageDatabase mDB;
    public billObject rentDays;

    public Slider performanceSlider;

    public GameObject tutorialShow;

    bool bonus = false;
    bool paying = false;
    bool routineNeeded = true;
    [SerializeField]
    float moneyCheck;
    //private AIModes hive; 
    // Start is called before the first frame update 
    void Start()
    {

        routineNeeded = true;
        //Checks if the this is the first time the player is playing the game to show the tutorial
        if (PlayerPrefs.GetInt("firstTime") == 1)
        {
            tutorialShow.SetActive(true);
        }
        else
            tutorialShow.SetActive(false);
        InvokeRepeating("updateGraph", 2.0f, 1f);//Repeats the function to refresh the graph at a rate of 1 second
        if (!PlayerPrefs.HasKey("rentDays"))
        {
            PlayerPrefs.SetInt("rentDays", 4);
        }


        clock = GetComponent<timerScript>();
        task = GetComponent<taskManager>();
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        if (!PlayerPrefs.HasKey("bankBalance"))
            PlayerPrefs.SetFloat("bankBalance", 0.0f);

        performanceSlider.maxValue = 100;
        //Retrieves the player's performance
        performanceSlider.value = PlayerPrefs.GetInt("performanceManager");

    }

    // Update is called once per frame 
    void Update()
    {
        
       //Only activates when the shift ends
        if (clock.maxTimeForGame == 0)
        {
            if (routineNeeded)//Starts the rountine if not done
            {
                //Protocol if player completes the objective
                if (task.challengeCount >= task.challengeLimit)
                {
                    taskText.text = "Task Completed: <color=green>PASS</color>";//Changes text to green

                    int pCap = PlayerPrefs.GetInt("performanceManager") + 5;//Adds 5 to pllayer's overall performance tracker
                    
                    performanceSlider.value = pCap;//Changes teh slider to reflect the new performance value
                    //Sending a message of praise from manager
                    mDB.mDatabase.Add(addMessageFromManager_Encouragement());//sends praise message to player 

                    bonus = true;//Ensures that a bonus is given
                }
                //Protocol if player fails objective
                else
                {
                    taskText.text = "Task Completed: <color=red>FAIL</color>";//Changes txt to red
                    int pCap = PlayerPrefs.GetInt("performanceManager") - 10;//Deducts 10 from player performance
                    
                    performanceSlider.value = pCap;
                    mDB.mDatabase.Add(addMessageFromManager_Fail());//sends fail message

                }
                paying = true;
                //Paying protocol
                if (paying)
                {
                    wages = (clock.timeForGame / 60) * hourlyRate;//Pays the player a certain amount depending on minutes of the shift
                    wages = wages + (score * 5);//Adds 5 fro every shelf stacked
                    if (bonus)
                    {
                        wages += 10;//Adds 10 to wages
                        bonus = false;
                    }
                    paying = false;
                    moneyCheck = wages;
                }
                //Routine to save the player's performance and wages accordingly
                PlayerPrefs.SetFloat("performanceManager", performanceSlider.value);

                float playerB = PlayerPrefs.GetFloat("bankBalance") + wages;
                PlayerPrefs.SetFloat("bankBalance", playerB);

                routineNeeded = false;
            }
            //Shows the report with the no. of shelves filled, wages and performance
            endReport.SetActive(true);
            scoreText.text = "Shelves Stacked: " + score.ToString();
            string wagesFormat = string.Format("{0:000.00}", wages);
            wagesText.text = "Wages: " + wagesFormat;
            


            StartCoroutine(waitToShow());
            //Stops the spawning of AI
            GameObject spawnPoint = GameObject.Find("AISpawnPoint");
            spawnPoint.SetActive(false);



        }
        //Ensures that the report doesn't show and AIs can spawn in
        else
        {
            endReport.SetActive(false);
            GameObject spawnPoint = GameObject.Find("AISpawnPoint");
            spawnPoint.SetActive(true);
            //mainMenuB.SetActive(false); 
        }

    }
    //Waits 5 seconds before the player can advance to the phone scene
    IEnumerator waitToShow()
    {
        yield return new WaitForSeconds(5.0f);
        mainMenuB.SetActive(true);
        
    }
   //Function handles the update of the graph
    void updateGraph()
    {
        AstarPath.active.Scan();
    }
    //Chooses a random message to send to the player
    public messageObjects addMessageFromManager_Encouragement()
    {
        int chooseMessage = Random.Range(0, 3);
        if (chooseMessage == 0)
            return Resources.Load<messageObjects>("Manager_Encouragement1");
        else if (chooseMessage == 1)
            return Resources.Load<messageObjects>("Manager_Encouragement2");
        else
            return Resources.Load<messageObjects>("Manager_Encouragement3");

    }
    //Chooses a random message to send to the player
    public messageObjects addMessageFromManager_Fail()
    {
        int chooseMessage = Random.Range(0, 3);
        if (chooseMessage == 0)
            return Resources.Load<messageObjects>("Manager_Fail1");
        else if (chooseMessage == 1)
            return Resources.Load<messageObjects>("Manager_Fail2");
        else
            return Resources.Load<messageObjects>("Manager_Fail3");

    }
}
