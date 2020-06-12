using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowWayPointScript : MonoBehaviour
{
    public GameObject locArrow, cageIn;
    private PlayerMovement myPM;
    GameObject locPoint;
    // Start is called before the first frame update
    void Start()
    {
        myPM = GameObject.Find("Player").GetComponent<PlayerMovement>();//Gets the location of the player

    }

    // Update is called once per frame
    void Update()
    {
        //Ensures that either the player has stock or the inventory is open
        if (myPM.stockCurr != null || cageIn.activeSelf == true)
        {
            //Sets the position of the stock or shelves being inquired
            if (myPM.stockCurr != null)
            {
                locPoint = myPM.stockCurr.GetComponent<stockItemBehaviour>().stockShelf;
            }
            else if (cageIn.activeSelf == true)
            {
                locPoint = GameObject.Find(GameObject.Find("testCage").GetComponent<cageBehaviour>().cageDB[0].stockShelf);
            }
            locArrow.transform.position = locPoint.transform.position;
            locArrow.SetActive(true);
        }
        
        else
            locArrow.SetActive(false);
        //Changing the colour of the arrow if the player is in range
        if (myPM.inRangeOfShelf)
        {
            locArrow.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
            locArrow.GetComponent<SpriteRenderer>().color = Color.white;



    }
}