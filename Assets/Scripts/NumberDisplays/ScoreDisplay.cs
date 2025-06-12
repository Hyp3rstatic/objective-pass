using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{


    public int number = 0;
    public SpriteRenderer spriteRenderer;
    private NumberSprites numberSprites;
    public bool countTen = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        numberSprites = GameObject.Find("NumberSprites").GetComponent<NumberSprites>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!countTen)
        {
            number = GlobalVariables.round % 10;
        }
        if (countTen)
        {
            number = (int)(GlobalVariables.round/10%10);
        }

        spriteRenderer.sprite = numberSprites.numSprites[number];
    }
}
