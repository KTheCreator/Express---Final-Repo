using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerStats : MonoBehaviour
{
    PlayerMovement playerM;
    Rigidbody2D playerRB;
    timerScript timer;

    public Text staminaUI;

    public Slider staminaSlider;
    public float maxStamina;
    private int staminaFallRate;
    public int staminaFallMult;
    private int staminaRegainRate;
    public int staminaRegainMult;

    private int staminaMaxFallRate;
    public int staminaMaxFallMult;

   
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerM = GetComponent<PlayerMovement>();
        timer = GameObject.Find("GameManager").GetComponent<timerScript>();
        //Intialises the maximum value and the current value of the slider
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
        //Sets the multiplier for different rates
        staminaFallRate = 1;//Fall rate
        staminaRegainRate = 1;//Recovery rate
        staminaMaxFallRate = 1; //Max fall rate

    }

    // Update is called once per frame
    void Update()
    {
        //Stamina For Player - decreases the stamina slider if the player is moving
        if(playerRB.velocity.magnitude != 0)
        {
           //Doubles the fall rate if the player is moving while holding an object
            if (playerM.isHoldingItem == true)
            {
                staminaSlider.value -= 2*(Time.deltaTime / staminaFallRate * staminaFallMult);
            }
            //Normal rate if player is moving
            else
            {
                Debug.Log("Player is moving");
                staminaSlider.value -= Time.deltaTime / staminaFallRate * staminaFallMult;
            }
            
        }
        else//Recovers the stamina if not moving
        {
            staminaSlider.value += Time.deltaTime / staminaRegainRate * staminaRegainMult;
        }
        if(staminaSlider.value >= maxStamina)//Caps the value at the maximum
        {
            staminaSlider.value = maxStamina;
        }
        else if(staminaSlider.value > (maxStamina / 4))//Above 25% the player moves are regular speed
        {
            playerM.speed = playerM.originalSpeed;
        }
        else if(staminaSlider.value <= (maxStamina / 4))//Below 25% the player moves at 2.5 speed(slower)
        {
            playerM.speed = 2.5f;
        }
        else if(staminaSlider.value <= 0) //At 0 the player cannot move and will drop the item that is currently in their hand
        {
            staminaSlider.value = 0;
            playerM.isHoldingItem = false;
        }
        if(timer.maxTimeForGame != 0)// will decrease the maxium amount of stamina that the player has over time.
        {
            if(playerRB.velocity.magnitude != 0)//if the player is moving it decreases quicker
            {
                maxStamina -= Time.deltaTime / (5 * staminaMaxFallRate) * staminaMaxFallMult;
            }//slower fall rate 
            maxStamina -= Time.deltaTime / (10*staminaMaxFallRate) * staminaMaxFallMult;
        }
        //Formats the text display of the stamina and updates it as the values change
        string staminaFormat = string.Format("{0:000}/{1:000}", (int)staminaSlider.value, (int)maxStamina);
        staminaUI.text = "Stamina: " + staminaFormat;
    }
}
