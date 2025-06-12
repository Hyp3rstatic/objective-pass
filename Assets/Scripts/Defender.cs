using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{

    public float speed = 0.5f;
    public GameObject football;
    private bool stopDefending = true;
    private int playNumber;
    private Vector2 testPos;
    public float play = 1;
    private Vector2 runTarget;

    private bool correctLeft;

    int x;
    int y;

    // Start is called before the first frame update
    void Start()
    {
        GlobalVariables.players.Add(gameObject);
        football = GameObject.Find("Football");
        x = Mathf.RoundToInt((float)transform.position.x);
        y = Mathf.RoundToInt((float)transform.position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (play == 1)
        {
            runTarget = football.transform.position;
        }
        if(play == 2 && GlobalVariables.round % 2 == 1)
        {
            runTarget = GameObject.Find("EvenPass").transform.position;
        }
        if (play == 2 && GlobalVariables.round % 2 == 0)
        {
            runTarget = GameObject.Find("OddPass").transform.position;
        }
        FaceTowards(gameObject, runTarget);
        if (stopDefending && GlobalVariables.isInPlay)
        {
            playNumber = Random.Range(0, 2);
            stopDefending = false;
        }
        if (GlobalVariables.isInPlay)
        {
            transform.position = Vector2.MoveTowards(transform.position, runTarget, speed);
            //transform.position = Vector2.MoveTowards(transform.position, qB.runTarget, speed);
            x = Mathf.RoundToInt((float)transform.position.x/5f) * 5;
            y = Mathf.RoundToInt((float)transform.position.y/5f) * 5;
            if (x + 10 <= GlobalVariables.fieldMaxX)
            {
                correctLeft = false;
            }
            else if (x - 10 >= GlobalVariables.fieldMinX)
            {
                correctLeft = true;
            }
        } else if (!GlobalVariables.isInPlay)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(x, y), speed);
            stopDefending = true;
            for(int i = 0; i < GlobalVariables.offensePlayers.Count; i++)
            {
                if((Vector2)GlobalVariables.offensePlayers[i].transform.position == new Vector2(x, y))
                {
                    if (correctLeft)
                    {
                        Debug.Log(transform.position);
                        Debug.Log(GlobalVariables.fieldMaxX);
                        x -= 5;
                    }
                    else if (!correctLeft)
                    {
                        x += 5;
                    }
                }
            }
            
        }
    }

    private void FaceTowards(GameObject rotating, Vector2 target)
    {
        Vector2 origin = rotating.transform.position; //the origin of the circle
        float radius = Vector2.Distance(origin, target);

        if (radius == 0)
        {
            return;
        }

        float radAngle = Mathf.Asin((target.y - origin.y) / radius)/* * radius + origin.y*/;
        float degAngle = radAngle * Mathf.Rad2Deg;

        if (Mathf.Sign(target.x - origin.x) == -1)
        {
            rotating.transform.eulerAngles = new Vector3(180f, 180f, (degAngle * -1) - 90);
        }
        else
        {
            rotating.transform.eulerAngles = new Vector3(0f, 0f, degAngle - 90);
        }
    }
}
