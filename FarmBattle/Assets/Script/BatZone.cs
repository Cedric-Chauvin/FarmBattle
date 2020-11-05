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
    private Animator animator;

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
                batOnIt = false;
                animator.SetBool("Bat", batOnIt);
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
        animator.SetBool("Bat", batOnIt);
    }
}
