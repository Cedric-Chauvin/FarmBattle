using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : Pickable
{
    [Header("Pumpkin parameter")]
    public int point = 1;

    [HideInInspector]
    public Player.TEAM team;
    private bool first = true;
    public override void UseObject(Player.TEAM team)
    {
        UIManager.GetInstance().AddPoint(team, point);
    }

    private void Update()
    {
        if (!isHolding)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    public void Steal(Player.TEAM pTeam)
    {
        if(pTeam!=team && first)
            GameManager.GetInstance.PlayVoice(team, "voler");

        first = false;
    }
}
