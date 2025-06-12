using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSprites : MonoBehaviour
{
    public Sprite num0;
    public Sprite num1;
    public Sprite num2;
    public Sprite num3;
    public Sprite num4;
    public Sprite num5;
    public Sprite num6;
    public Sprite num7;
    public Sprite num8;
    public Sprite num9;
    public Sprite[] numSprites;

    private void Start()
    {
        numSprites[0] = num0;
        numSprites[1] = num1;
        numSprites[2] = num2;
        numSprites[3] = num3;
        numSprites[4] = num4;
        numSprites[5] = num5;
        numSprites[6] = num6;
        numSprites[7] = num7;
        numSprites[8] = num8;
        numSprites[9] = num9;
    }
}
