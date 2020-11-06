using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PumpPlayerZone : MonoBehaviour
{
    public PumpBucketZone pumpBucketZone;
    public float pumpCooldown;
    public GameObject ghost;

    private List<Player> players = new List<Player>();
    private bool canPump = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            players.Add(player);
            player.isOnPump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            players.Remove(player);
            player.isOnPump = false;
        }
    }

    private void Update()
    {
        foreach (Player player in players)
        {
            if (player.tryPump)
            {
                if (canPump && pumpBucketZone.bucket && pumpBucketZone.transform.GetChild(0).childCount > 1)
                {
                    pumpBucketZone.isPumping = true;
                    canPump = false;
                    pumpBucketZone.bucket.FillBucket();
                    StartCoroutine(PumpCooldown());
                }
                player.tryPump = false;
            }
        }

        if (pumpBucketZone.ghost.activeSelf && ghost.activeSelf)
            ghost.SetActive(false);
        if (!pumpBucketZone.ghost.activeSelf && !ghost.activeSelf)
            ghost.SetActive(true);
        if (pumpBucketZone.transform.GetChild(0).childCount > 1 && pumpBucketZone.bucket.fillingRate >= 100 && ghost.activeSelf)
            ghost.SetActive(false);

    }

    IEnumerator PumpCooldown()
    {
        yield return new WaitForSeconds(pumpCooldown);
        canPump = true;
    }
}
