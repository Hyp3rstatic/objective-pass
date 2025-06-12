using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyDisplay : MonoBehaviour
{


    public int number = 0;
    public SpriteRenderer spriteRenderer;
    private NumberSprites numberSprites;
    public bool countTen = false;
    public bool countHundred = false;
    public bool countThousand = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        numberSprites = GameObject.Find("NumberSprites").GetComponent<NumberSprites>();
    }

    // Update is called once per frame
    void Update()
    {
        if (countTen)
        {
            number = (int)(GlobalVariables.currency / 10 % 10);
        }
        else if (countHundred)
        {
            number = (int)((GlobalVariables.currency / 100) % 10);
        }
        else if (countThousand)
        {
            number = (int)((GlobalVariables.currency / 1000) % 10);
        }
        else
        {
            number = GlobalVariables.currency % 10;
        }
        spriteRenderer.sprite = numberSprites.numSprites[number];
    }
}
