using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayButton : MonoBehaviour
{

    private Selectable selectState;
    private SpriteRenderer spriteRenderer;
    private Color defaultSpriteColor;

    public Gradient throwColor;
    public Gradient recieveColor;

    public float throwSpeed;
    public float recieveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        selectState = gameObject.AddComponent<Selectable>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultSpriteColor = spriteRenderer.color;

        throwColor = GameObject.Find("OddPass").GetComponent<Offense>().lineColor;
        recieveColor = GameObject.Find("EvenPass").GetComponent<Offense>().lineColor;

        throwSpeed = GameObject.Find("OddPass").GetComponent<Offense>().speed;
        recieveSpeed = GameObject.Find("EvenPass").GetComponent<Offense>().speed;
    }

    // Update is called once per frame
    void Update()
    {

        if (selectState.isSelected && !GlobalVariables.isInPlay)
        {
            GlobalVariables.isInPlay = true;
            selectState.isSelected = false;
            for (int i = 0; i < GlobalVariables.offensePlayers.Count; i++)
            {
                GlobalVariables.offensePlayers[i].isRunning = true;
            }
    }

        //selectState.UpdateScale(new Vector2(1.25f, 1.25f));

        if (GlobalVariables.isInPlay)
        {
            spriteRenderer.color = new Color(240, 0, 0);
        } else
        {
            spriteRenderer.color = defaultSpriteColor;
        }

        if (GlobalVariables.isInPlay)
        {
            bool continuePlay = false;
            for (int i = 0; i < GlobalVariables.offensePlayers.Count; i++)
            {
                if (GlobalVariables.offensePlayers[i].isRunning == true)
                {
                    continuePlay = true;
                    break;
                }
            }
            if (!continuePlay && GlobalVariables.passComplete)
            {
                GlobalVariables.isInPlay = false;
                GlobalVariables.passComplete = false;

                GlobalVariables.currency += Mathf.RoundToInt((float)Vector2.Distance(GlobalVariables.currentReciever.transform.position, GlobalVariables.currentPasser.transform.position) / 5f) * 5;

                GlobalVariables.round++;
                //GlobalVariables.currency += 1* GlobalVariables.round; //make the base value the distance traveled
                GameObject.Find("Defender").GetComponent<Defender>().speed += 0.1f;
                throwSpeed += 0.1f;
                recieveSpeed += 0.1f;
                GameObject.Find("Football").GetComponent<Objective>().speed += 0.1f;

                GlobalVariables.markedList.ForEach(Destroy);
                GlobalVariables.markedList = new List<GameObject>();

                GameObject.Find("Football").transform.rotation = GlobalVariables.currentReciever.transform.rotation;

                for (int i = 0; i < GlobalVariables.round*3; i++)
                {
                    GlobalVariables.markedList.Add(Instantiate<GameObject>(GameObject.Find("DisableMark")));
                    bool invalidSpot = true;
                    while (invalidSpot)
                    {
                        int disableY = Mathf.RoundToInt((float)Random.Range(GlobalVariables.fieldMinY, GlobalVariables.fieldMaxY) / 5f) * 5;
                        int disableX = Mathf.RoundToInt((float)Random.Range(GlobalVariables.fieldMinX, GlobalVariables.fieldMaxX) / 5f) * 5;
                        GlobalVariables.markedList[i].transform.position = new Vector2(disableX, disableY);
                        GlobalVariables.markedList[i].transform.parent = GameObject.Find("TemporaryMarkers").transform;
                        for (int j = 0; j < GlobalVariables.markedList.Count; j++)
                        {
                            invalidSpot = false;
                            if (GlobalVariables.markedList[j].transform.position == GlobalVariables.markedList[i].transform.position && GlobalVariables.markedList[j] != GlobalVariables.markedList[i])
                            {
                                invalidSpot = true;
                                break;
                            }
                        }
                        if (!invalidSpot)
                        {
                            for (int j = 0; j < GlobalVariables.players.Count; j++)
                            {
                                if (GlobalVariables.players[j].transform.position == GlobalVariables.markedList[i].transform.position)
                                {
                                    invalidSpot = true;
                                    break;
                                }
                            }
                        }
                    }
                }


                int affectedStat = Mathf.RoundToInt(Random.Range(1, 5));
                bool hasDebuffed = false;
                while (!hasDebuffed)
                {
                    switch (affectedStat)
                    {
                        case 1:
                            if (recieveSpeed > 0.1f)
                            {
                                recieveSpeed -= 0.1f;
                                hasDebuffed = true;
                            }
                            break;
                        case 2:
                            if (throwSpeed > 0.1f)
                            {
                                throwSpeed -= 0.1f;
                                hasDebuffed = true;
                            }
                            break;
                        case 3:
                            if (GameObject.Find("Football").GetComponent<Objective>().speed > 0.1f)
                            {
                                GameObject.Find("Football").GetComponent<Objective>().speed -= 0.1f;
                                hasDebuffed = true;
                            }
                            break;
                        case 4:
                            GameObject.Find("Defender").GetComponent<Defender>().speed += 0.1f;
                            hasDebuffed = true;
                            break;
                        default:
                            break;
                    }
                }

                for (int j = 0; j < GlobalVariables.offensePlayers.Count; j++)
                {
                    GlobalVariables.offensePlayers[j].runTarget = GlobalVariables.offensePlayers[j].transform.position;
                    if (GlobalVariables.offensePlayers[j].throwsOdd)
                    {
                        if((GlobalVariables.round) % 2 == 1)
                        {
                            GlobalVariables.offensePlayers[j].runMovingObject = GameObject.Find("Football");
                            GlobalVariables.offensePlayers[j].lineRenderer.colorGradient = throwColor;
                            GlobalVariables.offensePlayers[j].speed = throwSpeed;
                            GlobalVariables.offensePlayers[j].runMovingObject.transform.position = GlobalVariables.offensePlayers[j].transform.position;
                            GlobalVariables.currentPasser = GlobalVariables.offensePlayers[j];
                        }
                        else
                        {
                            GlobalVariables.offensePlayers[j].runMovingObject = GlobalVariables.offensePlayers[j].gameObject;
                            GlobalVariables.offensePlayers[j].lineRenderer.colorGradient = recieveColor;
                            GlobalVariables.offensePlayers[j].speed = recieveSpeed;
                            GlobalVariables.currentReciever = GlobalVariables.offensePlayers[j];
                        }
                    }
                    else if (!GlobalVariables.offensePlayers[j].throwsOdd)
                    {
                        if ((GlobalVariables.round) % 2 == 0)
                        {
                            GlobalVariables.offensePlayers[j].runMovingObject = GameObject.Find("Football");
                            GlobalVariables.offensePlayers[j].lineRenderer.colorGradient = throwColor;
                            GlobalVariables.offensePlayers[j].speed = throwSpeed;
                            GlobalVariables.offensePlayers[j].runMovingObject.transform.position = GlobalVariables.offensePlayers[j].transform.position;
                            GlobalVariables.currentPasser = GlobalVariables.offensePlayers[j];
                        }
                        else
                        {
                            GlobalVariables.offensePlayers[j].runMovingObject = GlobalVariables.offensePlayers[j].gameObject;
                            GlobalVariables.offensePlayers[j].lineRenderer.colorGradient = recieveColor;
                            GlobalVariables.offensePlayers[j].speed = recieveSpeed;
                            GlobalVariables.currentReciever = GlobalVariables.offensePlayers[j];
                        }
                    }
                }
            }
        }

    }
}
