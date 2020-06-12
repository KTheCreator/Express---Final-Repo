using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class messageScript : MonoBehaviour
{
    public messageObjects mObject;
    public Text senderText, messageText;
    public GameObject notificationIcon;
    // Start is called before the first frame update
    void Start()
    {
        //Uses data from the attached message object and assigns to their respective variables
        senderText.text = mObject.Sender;
        messageText.text = mObject.Message;
    }

    // Update is called once per frame
    void Update()
    {
        //Shows the notification if not opened previously by the player
        if (this.mObject.notRead == true)
            notificationIcon.SetActive(true);
        else
            notificationIcon.SetActive(false);
    }

    //Function handles the opening of the message and ensures that the data is displayed correctly when opened
    public void openMessageA()
    {
        GameObject.Find("menuButtonManager").GetComponent<additionalButtonBehaviour>().openMessageB();
        GameObject.Find("mainMessageArea/SenderText").GetComponent<Text>().text = this.mObject.Sender;
        GameObject.Find("mainMessageArea/MessageText").GetComponent<Text>().text = this.mObject.Message;
        this.mObject.notRead = false;//Marks the messsage as read

        //Changes the button to end the game rather than close the message
        if (this.mObject.custom && this.mObject.customType == "GameOver") {
            GameObject.Find("mainMessageArea/closeButton/Text").GetComponent<Text>().text = mObject.customButtonTitle;
            GameObject.Find("mainMessageArea/closeButton").GetComponent<Button>().onClick.AddListener(gameOverCustomMessage);
            GameObject.Find("mainMessageArea/closeButton").GetComponent<Button>().onClick.RemoveListener(GameObject.Find("menuButtonManager").GetComponent<additionalButtonBehaviour>().closeMessage);
        }

    }

    //Function that sends the player back to the start menu
    public void gameOverCustomMessage()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

}
