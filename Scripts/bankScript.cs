using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class bankScript : MonoBehaviour
{
    public Text bankAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Formats the player's bank value into £00.00
        string amountText = string.Format("Current Amount: \n £{0:000.00}", PlayerPrefs.GetFloat("bankBalance"));
        bankAmount.text = amountText;
    }
}
