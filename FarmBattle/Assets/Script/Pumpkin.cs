using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : Pickable
{
    [Header("Pumpkin parameter")]
    public int point = 1;

    public override void UseObject(Player.TEAM team)
    {
        UIManager.GetInstance().AddPoint(team, point);
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }
}
