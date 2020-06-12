using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private timerScript gameTime;

    public float speed = 6.0f;
    public float originalSpeed;

    public Rigidbody2D playerBody;
    private Vector2 movement = Vector2.zero;
    public bool isHoldingCage = false;
    public bool isHoldingItem = false;
    public bool madeNewItem = false;
    public float armLength = .5f;
    public Transform holdingPoint;

    public bool inRangeOfCage = false;
    public bool inRangeOfShelf = false;
    public bool inRangeofStock = false;
    RaycastHit2D ray;

    public GameObject itemPrefab;
    private GameObject newObject;
    public GameObject stockCurr;

    public bool isAvaliableForQuestions = true;
    public GameObject shelfIcon;

    public float stamina = 100.0f;
    public float rateOfDecay = 2.0f;

    public GameObject durationText;

    float startTime = 0f;

    public AudioSource stackingSound;


    // Start is called before the first frame update
    void Start()
    {
        gameTime = GameObject.Find("GameManager").GetComponent<timerScript>();
        originalSpeed = speed; //Stores the player's initial speed
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Allows the player to move if the shift hasn't ended
        if (gameTime.maxTimeForGame != 0)
        {
            //Rotation for the player and raycast in only four directions.
            if (Input.GetKeyDown(KeyCode.A))
                transform.eulerAngles = new Vector3(0, 0, 90);
            if (Input.GetKeyDown(KeyCode.W))
                transform.eulerAngles = new Vector3(0, 0, 0);
            if (Input.GetKeyDown(KeyCode.S))
                transform.eulerAngles = new Vector3(0, 0, 180);
            if (Input.GetKeyDown(KeyCode.D))
                transform.eulerAngles = new Vector3(0, 0, 270);

            //Movement
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            
            playerBody.velocity = movement * speed;





            combinedObjectsMovement();//Calls the function that handles the interacton of objects
            
            //Ensures that while the player is not holding anything, they can never be in range of the shelf
            if (stockCurr == null)
            {
                inRangeOfShelf = false;
            }
            

        }
        else
        {
            playerBody.velocity = new Vector2(0, 0);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, transform.position + Vector3.up * transform.localScale.x*armLength);

    }


    // Function handles the interaction will all objects
 
    private void combinedObjectsMovement()
    {
        //Start a timer when the player hits E
        if (Input.GetKeyDown(KeyCode.E))
        {
            startTime = Time.time;

        }
        //Stops the timer when the E key is lifted
        if (Input.GetKeyUp(KeyCode.E))
        {

            float delta = Time.time - startTime;//Calculates difference 
            if (delta > .5f)//Protocol for grabbign stock from the cage
            {
                if (inRangeOfCage)//Checks if the player is range of the cage
                {
                    if (!isHoldingCage && !isHoldingItem && !madeNewItem)//Checks if the player is holding anything
                    {
                        //Creates a stock prefab and uses the data of the stock object stored in index 0 of the cage's inventory before removing
                        Vector3 spawn = new Vector3(holdingPoint.transform.position.x, holdingPoint.transform.position.y, holdingPoint.transform.position.z - 1);//Sets the position to the holding gameObject
                        newObject = Instantiate(itemPrefab, spawn, Quaternion.identity);
                        newObject.GetComponent<stockItemBehaviour>().Settings(GameObject.Find("testCage").GetComponent<cageBehaviour>().Pop());
                        GameObject.Find("testCage").GetComponent<cageBehaviour>().Push();//Fills the empty space in the cage
                        if (newObject != null)
                        {
                           
                            //Makrs that the player is holding an item
                            madeNewItem = true;
                            isHoldingItem = true;
                            Debug.Log("Holding");
                        }
                    }
                }
                else
                {
                    //ensures that the player is not holding anything
                    madeNewItem = false;
                    isHoldingItem = false;
                    newObject = null;
                    Debug.Log("Not Holding");
                }
            }
            //Protocol for holding the cage or item
            else
            {
                if (!isHoldingCage && !isHoldingItem)//Checks if anything is being held
                {
                    //Raycast used to get information about the object in front
                    Physics2D.queriesStartInColliders = false;
                    ray = Physics2D.Raycast(transform.position, transform.up * transform.localScale.y, armLength);
                    if (ray.collider != null)
                    {
                        if (ray.collider.CompareTag("Cage")&&inRangeOfCage)//Marks the cage as being the item held 
                        {
                            isHoldingCage = true;

                        }

                        if (ray.collider.CompareTag("Stock"))//Marks the stock beign the item held
                        {
                            isHoldingItem = true;
                        }
                    }

                }
                else
                {
                    isHoldingCage = false;
                    isHoldingItem = false;
                    madeNewItem = false;
                    //Debug.Log("Not Holding");
                }

            }
        }

        if (Input.GetKey(KeyCode.R))//Protocol for stack stock
        {
            if (isHoldingItem)//Checks if stock is being held
            {
                if ((stockCurr.GetComponent<stockItemBehaviour>() != null && inRangeOfShelf))//Checks if in range of the shelf
                {
                    //stockItemBehaviour stockcurrObject = ray.collider.gameObject.GetComponent<stockItemBehaviour>();
                    stackingSound.Play();
                    stockCurr.GetComponent<stockItemBehaviour>().stackingDuration--;//Decrease the stocks duration

                }

            }
            else
            {
                stackingSound.Stop();
            }
        }
        else
        {
            stackingSound.Stop();
        }

        //Protocol for holding the cage
        if (isHoldingCage)
        {
            if (Mathf.Round(transform.rotation.eulerAngles.z) != 90 || Mathf.Round(transform.rotation.eulerAngles.z) != 270)//Ensures that the player cannot grab the cgae when at these angles
            {
                ray.collider.gameObject.transform.position = holdingPoint.position;//Sets the cage's position to the holding point position
                Physics2D.IgnoreCollision(GameObject.Find("testCage").GetComponent<Collider2D>(), GetComponent<Collider2D>(),true);//Temporaraily turns off collision between cage and player
            }
            

        }
        else
        {
            Physics2D.IgnoreCollision(GameObject.Find("testCage").GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);//Resets collision
        }

        //Protocol for holding item
        if (isHoldingItem)
        {

            if (ray.collider != null)
            {
                //Checks if the player is picking up an object from the floor or the cage and sets teh position of the item accordingly
                if (madeNewItem)
                {
                    stockCurr = newObject;
                }
                else
                {
                    stockCurr = ray.collider.gameObject;
                }

                stockCurr.transform.position = holdingPoint.position;
            }
            //ray.collider.gameObject.transform.position = holdingPoint.position;
            Debug.Log("Holding Stock");

        }
        else
        {
            stockCurr = null;
            
        }


    }

    //Function used to detect if in range of objects
    private void OnTriggerStay2D(Collider2D other)
    {
        //Handles the detection of shelves
        if (stockCurr != null)
        {
            if (other.gameObject.CompareTag("Shelf") && stockCurr != null)
            {
                if (other.GetComponent<shelfBehaviour>().requiredStock == stockCurr.GetComponent<stockItemBehaviour>().stockItemName && Vector2.Distance(other.gameObject.transform.position, transform.position) < 2)
                    inRangeOfShelf = true;
                Debug.Log("In Range");
            }
        }

        //Handles the detection of the cage
        if (other.gameObject.CompareTag("Cage"))
            inRangeOfCage = true;

        //Handles the detection of stock objects
        if (other.gameObject.CompareTag("Stock"))
            inRangeofStock = true;

    }

    //Function used to detect left the range of the objects and resets theif respective variable
    private void OnTriggerExit2D(Collider2D other)
    {
        if (stockCurr != null)
        {
            if (other.gameObject.CompareTag("Shelf") && other.GetComponent<shelfBehaviour>().requiredStock == stockCurr.GetComponent<stockItemBehaviour>().stockItemName)
            {
                Debug.Log("Out of Range");
                inRangeOfShelf = false;

            }
        }
       
        if (other.gameObject.CompareTag("Cage"))
            inRangeOfCage = false;

        if (other.gameObject.CompareTag("Stock"))
            inRangeofStock = false;


    }

}
