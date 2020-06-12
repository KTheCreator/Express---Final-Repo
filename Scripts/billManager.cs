using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class billManager : MonoBehaviour
{
    public Text rentText,heatText,waterText,foodText;
    public Text rentDeadlineText;
    public AudioSource paySound,errorSound;

    // Start is called before the first frame update
    void Awake()
    {
        //Formats each bill with their respective value
        string rentReplace = string.Format("Rent Amount: \n £{0:000.00}", billObject.RentAmount);
        rentText.text = rentReplace;
        string heatReplace = string.Format("Heat Amount: \n £{0:000.00}", billObject.heatAmount);
        string waterReplace = string.Format("Water Amount: \n £{0:000.00}", billObject.waterAmount);
        string foodReplace = string.Format("Food Amount: \n £{0:000.00}",billObject.foodAmount);
        heatText.text = heatReplace;
        waterText.text = waterReplace;
        foodText.text = foodReplace;
    }

    // Update is called once per frame
    void Update()
    {
        //Formats the deadline message to represent the different stages
        if(PlayerPrefs.GetInt("rentDays") == 1)
        {
            string rentDueReplace = "Rent Deadline: \n You have to pay your rent tomorrow (1)";
            rentDeadlineText.text = rentDueReplace;
        }
        else if(PlayerPrefs.GetInt("rentDays") == 0)
        {
            string rentDueReplace = "Rent Deadline: \n You have to pay your rent TODAY (0)";
            rentDeadlineText.text = rentDueReplace;
        }
        else
        {

            if (PlayerPrefs.GetInt("rentDays") < 0)
                PlayerPrefs.SetInt("rentDays", 0);
            //General format for the deadline when above 1
            string rentDueReplace = string.Format("Rent Deadline: \n {0:00} days left", PlayerPrefs.GetInt("rentDays"));
            rentDeadlineText.text = rentDueReplace;
        }

        //Changes the colour of the text depending on if the player has enough money in their account
        if (PlayerPrefs.GetFloat("bankBalance") >= billObject.RentAmount)
            rentText.color = Color.black;
        else
            rentText.color = Color.red;

        if (PlayerPrefs.GetFloat("bankBalance") >= billObject.heatAmount)
            heatText.color = Color.black;
        else
            heatText.color = Color.red;

        if (PlayerPrefs.GetFloat("bankBalance") >= billObject.waterAmount)
            waterText.color = Color.black;
        else
            waterText.color = Color.red;

        if (PlayerPrefs.GetFloat("bankBalance") >= billObject.foodAmount)
            foodText.color = Color.black;
        else
            foodText.color = Color.red;

    }
    
    //The subsequent functions follow the same procedure - deducts the amount of the bill from the bank value and plays a confirmation sound

    //Function handles the paying of the rent
    public void payRent()
    {
        if (PlayerPrefs.GetFloat("bankBalance") >= billObject.RentAmount)
        {
            
            float tmpF = PlayerPrefs.GetFloat("bankBalance");
            tmpF = tmpF - billObject.RentAmount;
            PlayerPrefs.SetFloat("bankBalance", tmpF);
            PlayerPrefs.SetInt("rentDays", 4);
            paySound.Play();
        }
        else
            errorSound.Play();
    }
    //Function handles the paying of the heat bill
    public void payHeat()
    {
        if (PlayerPrefs.GetFloat("bankBalance") >= billObject.heatAmount)
        {
            float tmpF = PlayerPrefs.GetFloat("bankBalance");
            tmpF = tmpF - billObject.heatAmount;
            PlayerPrefs.SetFloat("bankBalance", tmpF);
            PlayerPrefs.SetInt("heatDays", 7);
            paySound.Play();
        }
        else
            errorSound.Play();
    }
    //Function handles the paying of the food bill
    public void payFood()
    {
        if (PlayerPrefs.GetFloat("bankBalance") >= billObject.foodAmount)
        {

            float tmpF = PlayerPrefs.GetFloat("bankBalance");
            tmpF = tmpF - billObject.foodAmount;
            PlayerPrefs.SetFloat("bankBalance", tmpF);
            PlayerPrefs.SetInt("foodDays", 7);
            paySound.Play();
        }
        else
            errorSound.Play();
    }
    //Function handles the paying of the water bill
    public void payWater()
    {
        if (PlayerPrefs.GetFloat("bankBalance") >= billObject.waterAmount)
        {
            float tmpF = PlayerPrefs.GetFloat("bankBalance");
            tmpF = tmpF - billObject.waterAmount;
            PlayerPrefs.SetFloat("bankBalance", tmpF);
            PlayerPrefs.SetInt("waterDays", 7);
            paySound.Play();
        }
        else
            errorSound.Play();
    }
}
