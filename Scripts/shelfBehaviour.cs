using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shelfBehaviour : MonoBehaviour
{
    private PlayerMovement player;
    public string requiredStock;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();//Finds the player
        
        
    }

    //Function handles if the player is in range of the cage -OLD METHOD, REFER TO NEW METHOD IN PLAYERMOVEMENT SCRIPT
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.stockCurr.GetComponent<stockItemBehaviour>().stockItemName == requiredStock)
            player.inRangeOfShelf = true;
        else
            player.inRangeOfShelf = false;
    }
    //Function handles if the player is NOT in range of the cage -OLD METHOD, REFER TO NEW METHOD IN PLAYERMOVEMENT SCRIPT
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.stockCurr.GetComponent<stockItemBehaviour>().stockItemName == requiredStock)
            player.inRangeOfShelf = false;
        else
            player.inRangeOfShelf = false;
    }

}
