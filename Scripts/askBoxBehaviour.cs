using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class askBoxBehaviour : MonoBehaviour
{
    AIinteractingWithPLayer AIinteracting;
    
    public GameObject placementSprite;
    // Start is called before the first frame update
    void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //When the player clicks the screen a ray is sent to the position in relation to the world screen
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            //Ensures that is the speech bubble being clicked
            if (hit.collider != null)
            {
                if (hit.collider.name == gameObject.name)
                {
                    this.GetComponentInParent<AIinteractingWithPLayer>().answerAI();
                }
                    
            }
        }

    }



    

    
}
