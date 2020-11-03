using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Pickable
{
    [Header("Bucket parameter")]
    [Range(1, 10)]
    public int PumpNumberToFillTheBucket;

    [HideInInspector]
    public int fillingRate = 0;

    public override void UseObject(Player.TEAM team)
    {
    }

    public void FillBucket()
    {
        Debug.Log("Bucket: " + fillingRate + "/100");
        if (fillingRate < 100)
            fillingRate += 100 / PumpNumberToFillTheBucket;
    }
}
