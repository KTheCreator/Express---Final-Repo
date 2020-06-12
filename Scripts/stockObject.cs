using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="stock Object")]
public class stockObject : ScriptableObject
{
    //Stores the characteristics for each stock object created
    public string stockItemName;//Name
    public string stockShelf;//Designated shelf
    public Sprite stockIcon;//Icon
    
}
