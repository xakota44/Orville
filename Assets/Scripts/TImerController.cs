using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TImerController : MonoBehaviour
{
    public Sprite[] threeSegmentHeatSprites;
    public Sprite[] sixSegmentHeatSprites;
    public Sprite[] numberSprites;
    public GameObject Num1;
    public GameObject Num2;
    public GameObject Num3;
    public GameObject Num4;
    private SpriteRenderer sprite;



    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetBackground(string timerSprite, int index)
    {
        if(timerSprite == "3seg")
        {
            sprite.sprite = threeSegmentHeatSprites[index];
        }
        if (timerSprite == "6seg")
        {
            sprite.sprite = sixSegmentHeatSprites[index];
        }

    }

}
