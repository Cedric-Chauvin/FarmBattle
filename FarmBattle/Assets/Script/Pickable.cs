using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [Range(0,100)]
    public int speedMalus = 20;
    public TYPE type = TYPE.PUMPKIN;

    [HideInInspector]
    public Rigidbody2D rigidbody = null;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public enum TYPE
    {
        PUMPKIN,
        BUCKET,
        SEED,
        BAT
    }

    public virtual void UseObject(Player.TEAM team)
    {

    }

}
