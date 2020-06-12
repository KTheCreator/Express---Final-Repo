using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class messageListBehaviour : MonoBehaviour
{
    
    public GameObject mPrefab;
    public messageDatabase mDB;

    // Start is called before the first frame update
    void Awake()
    {
        //Reverse the order that elements are placed into the list
        for(int i = mDB.mDatabase.Count-1; i >= 0; i--)
        {
            //Creates a prefab using the data from the attached message Object
            GameObject tmpObject = Instantiate(mPrefab) as GameObject;
            tmpObject.transform.SetParent(GameObject.Find("GridwithElements").transform, false);//Makes the prefab a child of the gameobject in charge of formatting the list
            tmpObject.GetComponent<messageScript>().mObject = mDB.mDatabase[i];
        }
    }
}
