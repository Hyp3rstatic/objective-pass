using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make the football the QB space selector
//figure out how to angle football towards target

public class SpaceSelector : MonoBehaviour
{
    private GameObject spaceSelector;
    private Vector2 mousePosition;
    private LineRenderer lineRenderer;
    //private GameObject targetIndicator;
    private GameObject moveMarker;
    private GameObject throwMarker;
    private GameObject mouseMarker;

    public Gradient lineColor;
    public Material lineMaterial;

    // Start is called before the first frame update
    void Start()
    {
        mouseMarker = GameObject.Find("MouseMarker");
        spaceSelector = GameObject.Find("Marker");
        //spaceSelector.SetActive(false);
        //targetIndicator.SetActive(false);
        lineRenderer = spaceSelector.AddComponent<LineRenderer>();
        lineRenderer.sortingLayerName = "Forground";
        lineRenderer.material = lineMaterial;
        lineRenderer.colorGradient = lineColor;
        lineRenderer.startWidth = 0.25f;
        lineRenderer.endWidth = 0.25f;
        //targetIndicator = GameObject.Find("TargetIndicator");
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.isPlayerSelected)
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mouseMarker.transform.position = mousePosition;
            //transform.position = Vector2.Lerp(transform.position, mousePosition, 1f);
            spaceSelector.transform.position = mousePosition;
            int x = Mathf.RoundToInt(spaceSelector.transform.position.x);
            int y = Mathf.RoundToInt(spaceSelector.transform.position.y);
            x = Mathf.RoundToInt((float)x / 5f) * 5;
            y = Mathf.RoundToInt((float)y / 5f) * 5;
            spaceSelector.transform.position = new Vector2(x, y);

            //targetIndicator.SetActive(true);

            spaceSelector.SetActive(true);
            mouseMarker.SetActive(true);

            
            lineRenderer.gameObject.SetActive(true);
            Vector2 start = mouseMarker.transform.position;
            Vector2 end = GlobalVariables.selectedPlayer.transform.position;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
            //lineRenderer.sortingLayerName = "Forground";
            

            if (mousePosition.x <= GlobalVariables.fieldMinX - 2.5 || mousePosition.x >= GlobalVariables.fieldMaxX + 2.5 ||
                mousePosition.y <= GlobalVariables.fieldMinY - 2.5 || mousePosition.y >= GlobalVariables.fieldMaxY + 2.5)
            {
                GlobalVariables.isPlayerHovered = false;
                GlobalVariables.isPlayerSelected = false;
                GlobalVariables.selectedPlayer = null;

            }
        }
        else
        {
            mouseMarker.SetActive(false);
            spaceSelector.SetActive(false);
        }
    }
}
