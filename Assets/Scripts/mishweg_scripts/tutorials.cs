using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorials : MonoBehaviour
{
    public GameObject tutorial1, tutorial2, tutorial3, tutorial4,tutorial5, tutorial6, tutorial7, tutorial8;
    public string tutorialName;
    // Update is called once per frame
   

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"&& tutorialName == "wasd")
            {
               tutorial1.SetActive(true );
            
            

        }
        if(other.gameObject.tag == "Player" && tutorialName == "jump")
        {
            tutorial2.SetActive(true);
            
            
        }
        if (other.gameObject.tag == "Player" && tutorialName == "dash")
        {
            tutorial3.SetActive(true);
            
            
        }
        if (other.gameObject.tag == "Player" && tutorialName == "shoot")
        {
            tutorial4.SetActive(true);


        }
        if (other.gameObject.tag == "Player" && tutorialName == "enemy")
        {
            tutorial5.SetActive(true);


        }
        if (other.gameObject.tag == "Player" && tutorialName == "heal")
        {
            tutorial6.SetActive(true);


        }
        if (other.gameObject.tag == "Player" && tutorialName == "buy")
        {
            tutorial7.SetActive(true);


        }
        if (other.gameObject.tag == "Player" && tutorialName == "nextlevel")
        {
            tutorial8.SetActive(true);
           


        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && tutorialName == "wasd")
        {
            tutorial1.SetActive(true);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && tutorialName == "wasd")
        {
            tutorial1.SetActive(false);



        }
        if (other.gameObject.tag == "Player" && tutorialName == "jump")
        {
            tutorial2.SetActive(false);


        }
        if (other.gameObject.tag == "Player" && tutorialName == "dash")
        {
            tutorial3.SetActive(false);


        }
        if (other.gameObject.tag == "Player" && tutorialName == "shoot")
        {
            tutorial4.SetActive(false);


        }
        if (other.gameObject.tag == "Player" && tutorialName == "enemy")
        {
            tutorial5.SetActive(false);


        }
        if (other.gameObject.tag == "Player" && tutorialName == "heal")
        {
            tutorial6.SetActive(false);


        }
        if (other.gameObject.tag == "Player" && tutorialName == "buy")
        {
            tutorial7.SetActive(false);


        }
        if (other.gameObject.tag == "Player" && tutorialName == "nextlevel")
        {
            tutorial8.SetActive(false);


        }

    }
}
