using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialScript : MonoBehaviour
{
    public List<GameObject> tutSlides;//List containing the slide gameobjects
    public GameObject prev, next, close, tutorialArea;
    [SerializeField]
    int currentSlide = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            Time.timeScale = 0;
        }
        //Making sure only one slide is open at a time.
        for(int i = 0; i < tutSlides.Count; i++)
        {
            if (i == currentSlide)
                tutSlides[i].SetActive(true);
            else
                tutSlides[i].SetActive(false);
        }

        //Making sure buttons appear when nessecary

        if(currentSlide == 0)
        {
            prev.SetActive(false);next.SetActive(true);close.SetActive(false);
        }
        else if(currentSlide == 12)
        {
            prev.SetActive(true); next.SetActive(false); close.SetActive(true);
        }
        else if(currentSlide>0 && currentSlide < 12)
        {
            prev.SetActive(true); next.SetActive(true); close.SetActive(false);
        }

    }

    //Functions for the buttons to increment the slide
    public void nextSlide()
    {
        currentSlide++;
    }
    //Functions for the buttons to decrement the slide
    public void prevSlide()
    {
        currentSlide--;
    }
    //Functions for the button to close the tutorial
    public void closeSlide()
    {
        tutorialArea.SetActive(false);
        Time.timeScale = 1;
    }
}
