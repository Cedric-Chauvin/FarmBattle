using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SeedSpawner : MonoBehaviour
{
    public float cooldown;
    public int maxSeedsInGame;

    [Header("Classic seed")]
    public Seed classicSeed;

    [Header("Gold seed")]
    public Seed goldSeed;
    [Range(0f, 100f)]
    public float goldSeedSpawnRate;

    private System.Random rand = new System.Random();
    private bool canSpawn = false;
    private Vector3 size;
    private int seedNumber = 0;

    private void Awake()
    {
        size = GetComponent<SpriteRenderer>().bounds.size;
    }
    private void Start()
    {
        StartCoroutine(Cooldown());
    }

    private void Update()
    {
        if (canSpawn && seedNumber < maxSeedsInGame)
        {
            int choice = rand.Next(0, 101);
            float posX = UnityEngine.Random.Range(transform.position.x - size.x / 2, transform.position.x + size.x / 2);
            float posY = UnityEngine.Random.Range(transform.position.y - size.y / 2, transform.position.y + size.y / 2);

            Seed newSeed;
            if (choice < goldSeedSpawnRate)
                newSeed = Instantiate(goldSeed, new Vector3(posX, posY, posY), Quaternion.identity);
            else
                newSeed = Instantiate(classicSeed, new Vector3(posX, posY, posY), Quaternion.identity);
            newSeed.destroy = RemoveSeed;
            seedNumber++;
            canSpawn = false;
            StartCoroutine(Cooldown());
        }
    }

    private void RemoveSeed()
    {
        seedNumber--;
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canSpawn = true;
    }
}
