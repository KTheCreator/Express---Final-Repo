using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stockItemBehaviour : MonoBehaviour
{
    //private PlayerMovement player;
    BoxCollider2D stockCollider;
    public string stockItemName;
    public GameObject stockShelf;

    public GameObject highlightArea;
    public int stackingDuration = 100;
    

    // Start is called before the first frame update
    void Start()
    {
        stockCollider = gameObject.GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //Protocol if the player completes the stacking of this gameobject
        if (stackingDuration <= 0)
        {
            Destroy(gameObject);//Destroys the object
            //Resets the player's holding status to allow more sotck to be grabbed and increments their score
            GameObject.Find("Player").GetComponent<PlayerMovement>().isHoldingItem = false;
            GameObject.Find("GameManager").GetComponent<gameMangerScript>().score += 1;
        }

        //If the player is in range, the area around the stock is highlighted
        if (GameObject.Find("Player").GetComponent<PlayerMovement>().inRangeofStock == true)
            this.highlightArea.SetActive(true);
        else
            this.highlightArea.SetActive(false);
        
    }
    //Function handles the collision betweent the cage and the stock object and disables this whenenver they collide - prevents cage reboundig of the stock
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cage")
            Physics2D.IgnoreCollision(collision.collider, stockCollider);
    }
    //Function handles the assigment of variables with an attached stock object
    public void Settings(stockObject otherobject)
    {
        stockItemName = otherobject.stockItemName;
        stockShelf = GameObject.Find(otherobject.stockShelf);
    }
    
}
