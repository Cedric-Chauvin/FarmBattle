using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatZone : MonoBehaviour
{
    public Player.TEAM team;

    [Header("Bat parameters")]
    public Pickable bat;
    public float batCooldownAfterUsed;

    private bool batOnIt = true;
    private Player player = null;

    //tmp
    private SpriteRenderer spriteRenderer;
    private Color32 cBat = new Color32(210, 88, 174, 255);
    private Color32 cNoBat = new Color32(132, 132, 132, 255);

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (batOnIt && collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();

            if (player && !player.isHolding && player.team == team)
            {
                Debug.Log("Player take bat");
                player.item = bat;
                player.isHolding = true;
                batOnIt = false;
                spriteRenderer.color = cNoBat;
            }
        }
    }

    private void Update()
    {
        if (player && player.item == null)
        {
            player = null;
            StartCoroutine(BatCooldown());
        }
    }

    IEnumerator BatCooldown()
    {
        yield return new WaitForSeconds(batCooldownAfterUsed);
        batOnIt = true;
        spriteRenderer.color = cBat;
    }
}
