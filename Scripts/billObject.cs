using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName = "bill Object")]
public class billObject : ScriptableObject
{
    //Characteristics of the bill section - outlines the amount for each bill
    static public float RentAmount = 200.00f;
    static public float heatAmount = 60.00f;
    static public float foodAmount = 55.00f;
    static public float waterAmount = 80.00f;
    public int rentDue = 4;
}
