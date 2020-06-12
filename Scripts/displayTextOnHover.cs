using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class displayTextOnHover : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject highlightText;
    //public bool show = false;
    // Start is called before the first frame update
    void Start()
    {
        highlightText.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //When the player goes over the icons on the phone scene their description appears at the bottom.
    public void OnPointerEnter( PointerEventData pointerEventData)
    {
        Debug.Log("Mouse is " + pointerEventData.selectedObject);
        highlightText.SetActive(true);
    }
    public void OnPointerExit(PointerEventData pointerEvetData)
    {
        Debug.Log("Mouse left");
        highlightText.SetActive(false);
    }
}
