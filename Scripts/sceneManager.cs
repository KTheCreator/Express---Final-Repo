using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public messageDatabase mDatabase;
    //Function used to open scenes 
    public void FIreMe(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex,LoadSceneMode.Single);
    }
     public void decreaseDay()
    {
        //Decreasing the amount days till you have to pay rent
        int tmpInt = PlayerPrefs.GetInt("rentDays");
        tmpInt = tmpInt - 1;
        PlayerPrefs.SetInt("rentDays", tmpInt);
        if (PlayerPrefs.GetInt("rentDays") <= 0)
            mDatabase.mDatabase.Add(Resources.Load<messageObjects>("LandLord_GameOver"));

        //Managing the water days
        tmpInt = PlayerPrefs.GetInt("waterDays");
        tmpInt = tmpInt - 1;
        PlayerPrefs.SetInt("waterDays", tmpInt);
        if (PlayerPrefs.GetInt("waterDays") == 2)
            mDatabase.mDatabase.Add(Resources.Load<messageObjects>("Family_Notification_Water"));
        else if (PlayerPrefs.GetInt("waterDays") <= 0)
            mDatabase.mDatabase.Add(Resources.Load<messageObjects>("Family_GameOver_Water"));

        //Managing the heat days 
        tmpInt = PlayerPrefs.GetInt("heatDays");
        tmpInt = tmpInt - 1;
        PlayerPrefs.SetInt("heatDays", tmpInt);
        if (PlayerPrefs.GetInt("heatDays") == 2)
            mDatabase.mDatabase.Add(Resources.Load<messageObjects>("Family_Notification_heat"));
        else if (PlayerPrefs.GetInt("heatDays") <= 0)
            mDatabase.mDatabase.Add(Resources.Load<messageObjects>("Family_GameOver_Heat"));

        //Managing the food days
        tmpInt = PlayerPrefs.GetInt("foodDays");
        tmpInt = tmpInt - 1;
        PlayerPrefs.SetInt("foodDays", tmpInt);
        if (PlayerPrefs.GetInt("foodDays") == 2)
            mDatabase.mDatabase.Add(Resources.Load<messageObjects>("Family_Notification_Food"));
        else if (PlayerPrefs.GetInt("foodDays") <= 0)
            mDatabase.mDatabase.Add(Resources.Load<messageObjects>("Family_GameOver_Food"));

        //Deciding whether to send a message to the player or not - 60% likely
        int sending = Random.Range(0, 100);
        if(sending >= 60)
        {
            //Sending a message from the family or manager randomly
            int whoFrom = Random.Range(0, 2);
            if (whoFrom == 0)//From Manager
            {
                mDatabase.mDatabase.Add(addMessageFromManager_Random());
            }
            else//From Family
                mDatabase.mDatabase.Add(addMessageFromFamily());

        }
    }
    //Funciton handles the creation and setting of important values used fro tracking and such in the game
    public void StartGame()
    {
        PlayerPrefs.SetFloat("bankBalance",0.00f);
        PlayerPrefs.SetInt("rentDays", 10);
        PlayerPrefs.SetInt("waterDays", 7);
        PlayerPrefs.SetInt("heatDays", 7);
        PlayerPrefs.SetInt("foodDays", 7);
        PlayerPrefs.SetInt("firstTime", 1);
        PlayerPrefs.SetInt("performanceManager",45);
        //Resets the content of the messages to have the starting two feature intially 
        mDatabase.mDatabase.Clear();
        mDatabase.mDatabase.Add(Resources.Load<messageObjects>("Landlord_Introduction]"));
        mDatabase.mDatabase.Add(Resources.Load<messageObjects>("Manger_Introduction"));
        
        FIreMe(1);//Opens phone scene

    }

    //Choosing which message to send randomly to the player in terms of manager
    private messageObjects addMessageFromManager_Random()
    {
        int chooseMessage = Random.Range(0, 3);
        if (chooseMessage == 0)
            return Resources.Load<messageObjects>("Manager_Random1");
        else if (chooseMessage == 1)
            return Resources.Load<messageObjects>("Manager_Random2");
        else
            return Resources.Load<messageObjects>("ManagerEasterEgg");

    }

    //Choosing which message to send randomly to the player in terms of family
    private messageObjects addMessageFromFamily()
    {
        int chooseMessage = Random.Range(0, 3);
        if (chooseMessage == 0)
            return Resources.Load<messageObjects>("Family_Message1");
        else if (chooseMessage == 1)
            return Resources.Load<messageObjects>("Family_Message2");
        else
            return Resources.Load<messageObjects>("Family_Message3");

    }

}
