using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Pickable
{
    [Header("Seed parameter")]
    public int point = 1;
    [Range(0,100)]
    public int size = 50;
    public float mass = 20;
    public float growthTime = 5;
    [Range(0, 100)]
    public int pumpkinMalus = 50;
    public Sprite pumpkinSprite;
    public Sprite plantSprite;
    public Sprite growSprite;
    public Action destroy;

    private bool first = true;

    public void ChangeState()
    {
        if (first)
        {
            spriteRenderer.sprite = plantSprite;
            first = false;
        }
        else
            spriteRenderer.sprite = growSprite;
    }

    public void Update()
    {
        if (!isHolding)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

}
