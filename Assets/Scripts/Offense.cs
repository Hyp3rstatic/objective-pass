using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Timeline;

public class Offense : MonoBehaviour
{
    //private OffenseMarker selectMarker;

    public LineRenderer lineRenderer;
    private Selectable selectState;
    public SpriteRenderer spriteRenderer;
    //private ObjectRotator objectRotator;
    public Vector2 runTarget;
    public GameObject targetMarkerObj;

    public GameObject runMovingObject = null;
    public string moveMarkerName = "Marker";

    public Material lineMaterial;
    public Gradient lineColor;

    public Color markerColor;

    public float speed = 1f;
    public bool isRunning = false;
    private bool runComplete = true;
    public bool throwsOdd = false;

    public float strength = 1;

    // Start is called before the first frame update
    void Start()
    {
        if(runMovingObject == null)
        {
            runMovingObject = gameObject;
        }
        if(throwsOdd)
        {
            GlobalVariables.currentPasser = gameObject.GetComponent<Offense>();
        }
        else
        {
            GlobalVariables.currentReciever = gameObject.GetComponent<Offense>();
        }
        GlobalVariables.offensePlayers.Add(gameObject.GetComponent<Offense>());
        GlobalVariables.players.Add(gameObject);
        gameObject.AddComponent<CircleCollider2D>();
        targetMarkerObj = Instantiate(GameObject.Find(moveMarkerName), GameObject.Find("PlayerMarkers").transform);
        targetMarkerObj.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        targetMarkerObj.name = gameObject.name;
        selectState = gameObject.AddComponent<Selectable>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.sortingLayerName = "Forground";
        lineRenderer.material = lineMaterial;
        lineRenderer.colorGradient = lineColor;
        lineRenderer.startWidth = 0.25f;
        lineRenderer.endWidth = 0.25f;
        lineRenderer.sortingOrder = 0;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        runTarget = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.isInPlay && !runComplete)
            {
                isRunning = true;
                runComplete = false;
            }


            if (GameObject.Find("Marker") == null)
            {
                selectState.Deselect();
            }


            DrawLine(runTarget);

            if (selectState.isSelected && Input.GetKeyDown(GlobalVariables.selectButton))
            {
                //Vector2 oldTarget = runTarget;
                bool canPlace = true;
                for (int i = 0; i < GlobalVariables.markedList.Count; i++)
                {
                    if (GlobalVariables.markedList[i].transform.position == GameObject.Find("Marker").transform.position)
                    {
                        canPlace = false;
                    }
                }
                if (GlobalVariables.currentReciever.gameObject == gameObject && GameObject.Find("Marker").transform.position == GlobalVariables.currentPasser.transform.position)
                {
                    canPlace = false;
                }
                if (canPlace)
                {
                    runTarget = GameObject.Find("Marker").transform.position;
                }
            }

            targetMarkerObj.transform.position = runTarget;

            FaceTowards(gameObject, runTarget);
            FaceTowards(runMovingObject, runTarget);
            selectState.DeselectOn(GlobalVariables.deselectButton);
    }

    private void FixedUpdate()
    {
        
        if (isRunning)
        {
            runMovingObject.transform.position = Vector2.MoveTowards(runMovingObject.transform.position, runTarget, speed);
            if ((Vector2)runMovingObject.transform.position == runTarget)
            {
                isRunning = false;
                runComplete = true;
                runTarget = transform.position;
            }
        }
    }

    private void DrawLine(Vector2 targetPos)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, gameObject.transform.position);
        lineRenderer.SetPosition(1, targetPos);
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