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
    public Sprite growSprite;

    public void ChangeState()
    {
        spriteRenderer.sprite = growSprite;
    }

}
