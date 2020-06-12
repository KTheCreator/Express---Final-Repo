using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnCustomer : MonoBehaviour
{
    //private GameObject[] customerCount;
    timerScript time;
    public GameObject AIPrefab;
    public int count = 0; public int customerLimit;
    // Start is called before the first frame update
    void Start()
    {
        time = GetComponent<timerScript>();
        InvokeRepeating("waitToSpawn",2.0f,3.0f);//Repeats this process every 3 seconds after the first 2 second of the game
    }

 
    //Function handles the spawning of AI and updating of the AI counter
    void waitToSpawn()
    {
        if (count <= customerLimit)//Only creates of the number of Ai is lower than the limit
        {
            Instantiate(AIPrefab, GameObject.Find("AISpawnPoint").transform.position, Quaternion.identity);//Creates at the spawn point 
            count++;//Updates to show a new AI is created
        }


    }
}
