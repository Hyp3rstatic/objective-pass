using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDisplay : MonoBehaviour
{


    public int number = 0;
    public SpriteRenderer spriteRenderer;
    private NumberSprites numberSprites;
    public bool countTen = false;
    public bool defense = false;
    public bool passing = false;
    public bool running = false;
    public bool objective = false;

    private PlayButton playButton;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        numberSprites = GameObject.Find("NumberSprites").GetComponent<NumberSprites>();
        playButton = GameObject.Find("PlayButton").GetComponent<PlayButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (passing)
        {
            if (!countTen)
            {
                number = (int)(playButton.throwSpeed * 10f) % 10;
            }
            if (countTen)
            {
                number = (int)playButton.throwSpeed % 10;
            }
        }
        else if (running)
        {
            if (!countTen)
            {
                number = (int)(playButton.recieveSpeed * 10f) % 10;
            }
            if (countTen)
            {
                number = (int)playButton.recieveSpeed % 10;
            }
        }
        else if (defense)
        {
            if (!countTen)
            {
                number = (int)(GameObject.Find("Defender").GetComponent<Defender>().speed * 10f) % 10;
            }
            if (countTen)
            {
                number = (int)GameObject.Find("Defender").GetComponent<Defender>().speed % 10;
            }
        }
        else if (objective)
        {
            if (!countTen)
            {
                number = (int)(GameObject.Find("Football").GetComponent<Objective>().speed * 10f) % 10;
            }
            if (countTen)
            {
                number = (int)GameObject.Find("Football").GetComponent<Objective>().speed % 10;
            }
        }


        spriteRenderer.sprite = numberSprites.numSprites[number];
    }
}
