using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public float speed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GlobalVariables.gameOver)
        {
            Time.timeScale = 0f;
            GlobalVariables.isInPlay = false;
        }
        if(GlobalVariables.isInPlay && !GlobalVariables.currentPasser.isRunning && !GlobalVariables.passComplete)
        {
            //gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
            transform.position = Vector2.MoveTowards(transform.position, GlobalVariables.currentReciever.runTarget, speed);
            if (Vector2.Distance(transform.position, GlobalVariables.currentReciever.transform.position) <= 1)
            {
                //gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
                GlobalVariables.passComplete = true;
                
            }
            else if (Vector2.Distance(transform.position, GameObject.Find("Defender").transform.position) <= 1.5f)
            {
                GlobalVariables.gameOver = true;
                for(int i = 0; i <= 255; i++)
                {
                    GameObject.Find("Defeat").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, i);
                }
            }
        }
        else if(GlobalVariables.isInPlay && !GlobalVariables.passComplete)
        {
            if (Vector2.Distance(transform.position, GlobalVariables.currentReciever.transform.position) <= 1)
            {
                GlobalVariables.currentPasser.isRunning = false;
                GlobalVariables.passComplete = true;
            }
            else if (Vector2.Distance(transform.position, GameObject.Find("Defender").transform.position) <= 1.5f)
            {
                GlobalVariables.gameOver = true;
                for (int i = 0; i <= 255; i++)
                {
                    GameObject.Find("Defeat").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, i);
                }
                
            }
        }
        else if(GlobalVariables.isInPlay && GlobalVariables.gameOver)
        {
            transform.position = GlobalVariables.currentReciever.transform.position;
        }
        else if(GlobalVariables.isInPlay)
        {
            transform.position = GameObject.Find("Defender").transform.position;
        }
        if(GlobalVariables.round == 10)
        {
            for (int i = 0; i <= 255; i++)
            {
                GameObject.Find("Victory").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, i);
                GlobalVariables.gameOver = true;
            }
        }
    }
}
