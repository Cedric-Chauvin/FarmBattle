using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatZone : MonoBehaviour
{
    public Player.TEAM team;
    public GameObject Cross;

    [Header("Bat parameters")]
    public Pickable bat;
    public float batCooldownAfterUsed;

    private bool batOnIt = true;
    private Player player = null;
    private Animator animator;

    private bool batTaken = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
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
                player.canHit = true;
                player.canDropBat = false;
                player.StartCoroutine(player.DropBatCooldown());
                batTaken = true;
                batOnIt = false;
                animator.SetBool("Bat", batOnIt);
            }
            else
            {
                player = null;
                Cross.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Cross.SetActive(false);
        }
    }

    private void Update()
    {
        if (player && player.item == null && batTaken)
        {
            player = null;
            batTaken = false;
            StartCoroutine(BatCooldown());
        }
    }

    IEnumerator BatCooldown()
    {
        yield return new WaitForSeconds(batCooldownAfterUsed);
        batOnIt = true;
        animator.SetBool("Bat", batOnIt);
    }
}
