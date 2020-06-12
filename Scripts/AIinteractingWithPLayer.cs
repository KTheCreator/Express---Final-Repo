using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pathfinding;

public class AIinteractingWithPLayer : MonoBehaviour
{
    // Start is called before the first frame update 
    AIModes aIModes;
    AIPath aP;
    public bool asking, answered;
    private PlayerMovement player;
    public GameObject askBox; 
    public float timeOut = 3.0f;
    public Sprite defaultSprite;
    public GameObject pSprite;

    public Sprite satisfiedEmoji;

    public AudioSource checkSound;
    void Awake()
    {
        aIModes = GetComponent<AIModes>();
        aP = GetComponent<AIPath>();
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();

    }

    // Update is called once per frame 
    void Update()
    {
        //Executes the asking protocol when the AI's asking the player
        if (asking)
        {
            pSprite.GetComponent<SpriteRenderer>().sprite = defaultSprite;

            player.isAvaliableForQuestions = false;//Makes sure tha other AI's do not approach the player when already dealing with one customer
            askBox.SetActive(true);

        }
        else
        {
            askBox.SetActive(false);
        }

    }



    //Function handles the answering protocol for the AI
    public void answerAI()
    {
        Debug.Log("Answered AI");
        pSprite.GetComponent<SpriteRenderer>().sprite = satisfiedEmoji;//Changes the sprite in the box
        //If the current object relates to helping customers, keeps track of how many are help
        if (GameObject.Find("GameManager").GetComponent<taskManager>().challengeType == "customers")
        {
            GameObject.Find("GameManager").GetComponent<taskManager>().challengeCount++;
        }
        //Proceeds to the next item and reset protocol
        player.isAvaliableForQuestions = true;
        this.aIModes.i++; asking = false;
        this.aIModes.mode = 1;
        checkSound.Play();

        this.aP.isStopped = false;
    }
    //Function used to handle if the player ignores the AI
    public void ignoredAI()
    {
        Debug.Log("Failed to reply");
        player.isAvaliableForQuestions = true;
        this.aIModes.i++; asking = false;
        this.aIModes.mode = 1;

        this.aP.isStopped = false;

    }

}