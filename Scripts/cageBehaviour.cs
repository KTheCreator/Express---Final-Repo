using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cageBehaviour : MonoBehaviour
{

    public List<stockObject> cageDB;
    private PlayerMovement player;
    public GameObject inventoryObject;
    public TextMesh invText;
    public SpriteRenderer invIcon;
    public GameObject highlightArea;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        //Fills the cage's inventory by randomly choosing from the main database
        for (int i = 0; i < cageDB.Count; i++)
        {
            int RandomAccess = Random.Range(0, GameObject.Find("stockHandler").GetComponent<stockDatabase>().stockDB.Count);
            cageDB[i] = GameObject.Find("stockHandler").GetComponent<stockDatabase>().stockDB[RandomAccess];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Highlights the immediate area around the cage if the player is near
        if (player.inRangeOfCage)
        {
            highlightArea.SetActive(true);
        }
        else
        {
            highlightArea.SetActive(false);
        }

        //Only allows the player to toggle the inventory if nearby
        if (Input.GetKeyDown(KeyCode.I) && player.inRangeOfCage)
        {
            //Shows the stock stored in the first index
            if (inventoryObject.activeSelf == false)
            {
                inventoryObject.SetActive(true);
                invText.text = cageDB[0].stockItemName;//Stock's name
                invIcon.sprite = cageDB[0].stockIcon;//Stock's Icon

            }
            else
            {
                inventoryObject.SetActive(false);
                //GameObject.Find("Player").GetComponent<arrowWayPointScript>().locArrow.SetActive(false);
            }
        }
        //Allows the player to wlak away to close the inventory
        else if (inventoryObject.activeSelf == true && !player.inRangeOfCage)
        {
            inventoryObject.SetActive(false);
            //GameObject.Find("Player").GetComponent<arrowWayPointScript>().locArrow.SetActive(false);
        }
    }
    //Function handles the removal of the first element while retaining the data from the 
    //stock object
    public stockObject Pop()
    {
        stockObject tmpObj = cageDB[0];
        cageDB.Remove(cageDB[0]);
        return tmpObj;
    }

    //Function handles the insertion of stock items into the list
    public void Push()
    {
        int RandomAccess = Random.Range(0, GameObject.Find("stockHandler").GetComponent<stockDatabase>().stockDB.Count);
        cageDB.Add(GameObject.Find("stockHandler").GetComponent<stockDatabase>().stockDB[RandomAccess]);
    }

}