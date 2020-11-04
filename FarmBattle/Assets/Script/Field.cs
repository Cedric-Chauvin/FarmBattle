using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public Pumpkin pumpkin;

    private Seed seed = null;
    private List<Player> players = new List<Player>();

    private void Awake()
    {
        
    }

    public void PlantSeed(Pickable newSeed)
    {
        if (seed != null || newSeed.type!= Pickable.TYPE.SEED)
            return;

        seed = newSeed as Seed;
        seed.rigidbody.simulated = false;
        seed.transform.parent = this.transform;
        seed.transform.localPosition = Vector3.zero;
        ChangeState();
    }

    public void PutWater(Pickable newSeed)
    {
        if (seed == null || newSeed.type != Pickable.TYPE.BUCKET)
            return;
        Bucket bucket = newSeed as Bucket;
        if (bucket.fillingRate >= 100)
        {
            StartCoroutine(GrowthRoutine(seed.growthTime));
            bucket.fillingRate = 0;
            bucket.UpdateFillingRate();
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void SpawnPumpkin()
    {
        if (seed == null)
            return;
        transform.GetChild(0).gameObject.SetActive(false);
        Destroy(seed.gameObject);
        Pumpkin instance = Instantiate(pumpkin);
        instance.point = seed.point;
        instance.rigidbody.mass = seed.mass;
        instance.rigidbody.drag = seed.mass;
        instance.speedMalus = seed.pumpkinMalus;
        instance.transform.localScale = Vector3.one * seed.size / 100;
        instance.transform.position = transform.position;
        seed = null;
        ChangeState();
    }

    private IEnumerator GrowthRoutine(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnPumpkin();
    }

    private void ChangeState()
    {
        foreach (Player player in players)
        {
            if (seed == null)
            {
                player.OnRelease -= PutWater;
                player.OnRelease += PlantSeed;
            }
            else
            {
                player.OnRelease -= PlantSeed;
                player.OnRelease += PutWater;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if (seed == null)
                player.OnRelease += PlantSeed;
            else
                player.OnRelease += PutWater;
            players.Add(player);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if (seed == null)
                player.OnRelease -= PlantSeed;
            else
                player.OnRelease -= PutWater;
            players.Remove(player);
        }
    }
}
