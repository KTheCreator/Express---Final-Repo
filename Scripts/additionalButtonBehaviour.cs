using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class additionalButtonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Button activeButton;
    public messageDatabase mDatabase;
    public bool newMessages;
    public GameObject mainScreen, bankScreen, messageScreen, mainMessageArea, messageListing, billArea, messageIcon;
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Shows an indicator if there is any messages that the player has not opened yet
        if (newMessages)
            messageIcon.SetActive(true);
        else
            messageIcon.SetActive(false);
        int count = 0;
        for (int i = 0; i < mDatabase.mDatabase.Count; i++)//Cycles through all the messages and checks if there is any unopened
        {
            if (mDatabase.mDatabase[i].notRead == true)
            {
                count++;
            }
        }
        if (count != 0)
        {
            newMessages = true;
        }
        else
            newMessages = false;
        
    }

    public void switchToBank()//Used to transition to the bank screen on the phone from the main screen 
    {
        mainScreen.SetActive(false);
        bankScreen.SetActive(true);
    }

    public void switchToMenu()//Used to transition to the main screen on the phone from the bank screen
    {
        mainScreen.SetActive(true);
        bankScreen.SetActive(false);
    }

    public void openMessageB()//Used to transition from the list of messages to the message area
    {
        messageListing.SetActive(false);
        mainMessageArea.SetActive(true);
         
    }
    
    public void closeMessage()//Used to transition from the message area to the list of messages
    {
        messageListing.SetActive(true);
        mainMessageArea.SetActive(false);
    }
    
    public void switchToMenuFromMessages()////Used to transition to the main screen on the phone from messages screen
    {
        mainScreen.SetActive(true);
        messageScreen.SetActive(false);
    }

    public void switchToMessages()//Used to transition to the message screen on the phone from the main screen 
    {
        mainScreen.SetActive(false);
        messageScreen.SetActive(true);
        closeMessage();
    }

    public void openBillArea()//Opens the section that displays the bill
    {
        billArea.SetActive(true);
    }

    public void closeBillArea()//Closes the section with the bill
    {
        billArea.SetActive(false);
    }

}
