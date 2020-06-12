using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customerManager : MonoBehaviour
{
    private GameObject[] customerCount;
    public int askingTooManyQuestions;
    public bool TooManyCustomersAskingQuestions = false;
    private timerScript timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("GameManager").GetComponent<timerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        customerCount = GameObject.FindGameObjectsWithTag("Customer");//Finds all the customers in the scene
        //Ensures that only one AI is asking a question at any given time
        askingTooManyQuestions = 0;
        for (int i = 0; i < customerCount.Length; i++)
        {
            //Finds all that are asking a question
            if (customerCount[i].GetComponent<AIModes>().mode == 2)
            {
                askingTooManyQuestions++;
            }
            
        }
        if (askingTooManyQuestions > 1)
        {
            //Chooses one of them to be able to ask the question the others are forced to move on to the next item in their list
            customerCount = GameObject.FindGameObjectsWithTag("Customer");
            int pickOne = Random.Range(0, customerCount.Length);
            for (int i = 0; i < customerCount.Length; i++)
            {
                if (pickOne == i)
                {
                    continue;
                }
                else
                {
                    customerCount[i].GetComponent<AIModes>().i++;
                    customerCount[i].GetComponent<AIModes>().mode = 1;
                }
                    
            }
            
        }

        if(timer.maxTimeForGame == 0)
        {
            customerCount = GameObject.FindGameObjectsWithTag("Customer");
            for(int i = 0; i < customerCount.Length; i++)
            {
                customerCount[i].GetComponent<AIModes>().mode = 3;
            }
        }
       
    }
}
