using System.Collections.Generic;
using UnityEngine;

public class AIConvoScript : MonoBehaviour
{
    private List<string> shelvesA = new List<string>() { "breadShelf", "waterShelf", "internationalShelf", "readyMealShelf","cerealShelf","beautyShelf","crispShelf","alcoholShelf","milkShelf","cheeseShelf" };
    public List<string> shoppingList=new List<string>();
    public Dictionary<string, int> whatINeed = new Dictionary<string, int>(); public bool ready = false;
    public customerRandomDataBase spriteData;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Starting: Create List Process");
        int randomCap = Random.Range(1, shelvesA.Count);//Set the list limit of the ai

        for(int i = 0; i < randomCap; i++)
        {
            bool added = false;
            while (!added)
            {
                string shopItem = shelvesA[Random.Range(0, shelvesA.Count)];//Picks a shelf at random
                bool exist = false;
                for (int j = 0; j < shoppingList.Count; j++)//Checks if it exists in the ai personal shopping list
                {
                    if (shopItem == shoppingList[j])//If the choosen shelf is in the list already then tell the system its exist in the list
                    {
                        exist = true;
                        //return;
                    }
                        
                }
                if (!exist)//Only if it doesn't exist do we add it to the list....otherwise we just loop again and pick something else at random
                {
                    shoppingList.Add(shopItem);
                    added = true;
                }

            }
            
        }

        foreach(string priority in shoppingList)//Assigns the priority for all the items in the list
        {
            whatINeed.Add(priority,Random.Range(0,100));
        }
        //Chooses the AI's sprite
        int randomSprite = Random.Range(0, spriteData.spritesDB.Count);
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteData.spritesDB[randomSprite];
        ready = true;
        
    }




   
}
