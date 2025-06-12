using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public KeyCode selectKey = KeyCode.None;
    public bool isSelected = false;
    public bool isHoveredOver = false;
    private Vector2 defaultScale;
    private SpriteRenderer spriteRenderer;
    private Color defaultColor;

    private void Start()
    {
        defaultScale = transform.localScale;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    private void Update()
    {
        UpdateSelection();
    }

    public void UpdateScale(Vector2 scaleMult)
    {
        transform.localScale = defaultScale;
        if (isHoveredOver)
        {
            transform.localScale *= scaleMult;
        }
    }

    public void UpdateColor(Color selectColor)
    {
        spriteRenderer.color = defaultColor;
        if(isSelected)
        {
            spriteRenderer.color = selectColor;
        }
    }

    public void DeselectOn(KeyCode deselectKey)
    {
        if(Input.GetKeyDown(deselectKey))
        {
            isSelected = false;
            GlobalVariables.isPlayerSelected = false;
            GlobalVariables.selectedPlayer = null;
        }
    }

    public void Deselect()
    {
        isSelected = false;
        GlobalVariables.isPlayerSelected = false;
        GlobalVariables.selectedPlayer = null;
    }

    private void UpdateSelection()
    {
        if (Input.GetKeyDown(GlobalVariables.selectButton) && isHoveredOver && !isSelected && Time.timeScale != 0)
        {
            isSelected = true;
            GlobalVariables.isPlayerSelected = true;
            GlobalVariables.selectedPlayer = gameObject;
        }
    }

    private void OnMouseOver()
    {
        isHoveredOver = true;
        GlobalVariables.isPlayerHovered = true;
    }

    private void OnMouseExit()
    {
        isHoveredOver = false;
        GlobalVariables.isPlayerHovered = false;
    }
    /*
    private void checkSelection()
    {
        if (Input.GetKeyDown(selectKey)
        {

        }
    }
    */
}
