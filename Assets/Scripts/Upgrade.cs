using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public bool target = false;
    public bool pass = false;
    public bool run = false;

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(GlobalVariables.selectButton) && !GlobalVariables.isInPlay && Time.timeScale != 0)
        {
            if (GlobalVariables.currency >= 25)
            {
                GameObject.Find("Defender").GetComponent<Defender>().speed += 0.1f;
                GlobalVariables.currency -= 25;
                if (target)
                {
                    GameObject.Find("Football").GetComponent<Objective>().speed += 0.2f;
                }
                else if (run)
                {
                    GameObject.Find("PlayButton").GetComponent<PlayButton>().recieveSpeed += 0.2f;
                }
                else if (pass)
                {
                    GameObject.Find("PlayButton").GetComponent<PlayButton>().throwSpeed += 0.2f;
                }
            }
        }
    }
}
