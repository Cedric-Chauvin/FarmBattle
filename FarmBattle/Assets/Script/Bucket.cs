using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Pickable
{
    [Header("Bucket parameter")]
    [Range(1, 10)]
    public int PumpNumberToFillTheBucket;
    public Sprite bucketEmpty;
    public Sprite bucketFull;

    [HideInInspector]
    public int fillingRate = 0;

    private bool spriteChanged = true;

    public override void UseObject(Player.TEAM team)
    {
    }

    public void FillBucket()
    {
        Debug.Log("Bucket: " + fillingRate + "/100");
        if (fillingRate < 100)
            fillingRate += 100 / PumpNumberToFillTheBucket;
    }

    private void Update()
    {
        if (fillingRate < 100 && !spriteChanged)
        {
            Debug.Log("The bucket is empty or not full");
            spriteRenderer.sprite = bucketEmpty;
            spriteChanged = true;
        }
        if (fillingRate >= 100 && spriteChanged)
        {
            Debug.Log("The bucket is full");
            spriteRenderer.sprite = bucketFull;
            spriteChanged = false;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }
}
