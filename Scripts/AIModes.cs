using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIModes : MonoBehaviour
{
    AIDestinationSetter AITarget;
    AIConvoScript brain;
    AIinteractingWithPLayer socialSkills;
    AIPath AiBase;
    private PlayerMovement player;
    private spawnCustomer customerCount;
    bool currentlyAskingThePlayer = false;
    public string currentlyWorkingOn; public int i = 0;
    public float range = 2f; public int mode = 1;
    [SerializeField]
    bool inRangeofTarget;
    bool askedQuestion = false;
    bool nextStep = false;
    // Start is called before the first frame update 
    void Awake()
    {
        AITarget = GetComponent<AIDestinationSetter>();
        AiBase = GetComponent<AIPath>();
        brain = GetComponent<AIConvoScript>();
        socialSkills = GetComponent<AIinteractingWithPLayer>();
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        customerCount = GameObject.Find("AISpawnPoint").GetComponent<spawnCustomer>();

    }

    // Update is called once per frame 
    void Update()
    {
        //Handles the switching of modes
        if (brain.ready)
        {
            switch (mode)
            {
                case 1:
                    shoppingListMode();
                    break;
                case 2:
                    askPlayer();
                    break;
                case 3:
                    leaveShop();
                    break;

            }
        }

    }

    //Funciton analsyses each item and acts accordingly
    private void shoppingListMode()
    {
        //Checks if all elements are visited
        if (i < brain.shoppingList.Count)
        {
            currentlyWorkingOn = brain.shoppingList[i];//Assigns the current item being worked on to store
            //Limits the AI to ask the player once 
            if ((brain.whatINeed[currentlyWorkingOn] > 75) && !askedQuestion) { mode = 2; return; }//Checks value of item and if the AI has asked a quesiton before, if not the AI can ask the play the question

            //Moves the AI to the destination and increments when reached
            AITarget.target = GameObject.Find(currentlyWorkingOn).transform;
            if (Vector2.Distance(gameObject.transform.position,AITarget.target.position) < 2)
            {
                nextStep = true;
                
            }
            if (nextStep)
            {
                i++;
                nextStep = false;
            }
        }
        //In the case that the AI exceeds the list, they sent sent straight to the exit point
        else if (i >= brain.shoppingList.Count)
        {
            Debug.Log("Exceeds Levels");
            mode = 3;
            return;
        }
    }
    //Function handles the whole asking protocol
    private void askPlayer()
    {
        askedQuestion = true;
        if (player.isAvaliableForQuestions || this.currentlyAskingThePlayer)//Checks if the player is available for questioning
        {
            AITarget.target = GameObject.Find("Player").transform;
            if (inRangeofTarget)
            {
                AiBase.isStopped = true;
                this.socialSkills.asking = true;
                this.currentlyAskingThePlayer = true;

            }
        }

    }
    //Handles the leaving protocol of the AI
    private void leaveShop()
    {
        AITarget.target = GameObject.Find("AIExitPoint").transform;
        //De-spawns the AI and decrease the total amount so the system can create more with the new space
        if (inRangeofTarget)
        {
            customerCount.count--;
            gameObject.SetActive(false);
        }

    }

    //Handles the detection of AI with triggers
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == AITarget.target.name)
        {
            inRangeofTarget = true;
            Debug.Log("At: " + other.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == AITarget.target.name)
        {
            //Allows the player to walk away from the AI if they choose to ignore them.
            if (mode == 2)
            {
                inRangeofTarget = false;
                Debug.Log("Left AI");
                this.socialSkills.ignoredAI();

            }
        }
        inRangeofTarget = false;

    }
}