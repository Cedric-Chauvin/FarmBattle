using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Field : MonoBehaviour
{
    public Pumpkin pumpkin;
    public Vector3Int CellPosition;
    public TilesGroups tilesGroups;

    private Seed seed = null;
    private List<Player> players = new List<Player>();
    private Tilemap tilemap;

    private void Awake()
    {
        tilemap = GetComponentInParent<Tilemap>();
        transform.position = GetComponentInParent<GridLayout>().CellToWorld(CellPosition)+Vector3.one*0.55f;  
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
            tilemap.SetTile(CellPosition, tilesGroups.wateredTile);
            seed.ChangeState();
        }
    }

    private void SpawnPumpkin()
    {
        if (seed == null)
            return;
        Destroy(seed.gameObject);
        Pumpkin instance = Instantiate(pumpkin);
        instance.point = seed.point;
        instance.rigidbody.mass = seed.mass;
        instance.rigidbody.drag = seed.mass;
        instance.speedMalus = seed.pumpkinMalus;
        instance.transform.localScale = Vector3.one * (seed.size / 100.0f)*2;
        instance.transform.position = transform.position;
        instance.GetComponent<SpriteRenderer>().sprite = seed.pumpkinSprite;
        seed = null;
        tilemap.SetTile(CellPosition, tilesGroups.normalTile);
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
