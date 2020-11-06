using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvadeTrigger : MonoBehaviour
{
    public Player.TEAM team;
    public float cooldown;

    private bool canTalk = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && canTalk)
        {
            if (other.GetComponent<Player>().team != team)
            {
                GameManager.GetInstance.PlayVoice(team, "invade");
                canTalk = false;
                StartCoroutine(Routine());
            }
        }
    }

    private IEnumerator Routine()
    {
        yield return new WaitForSeconds(cooldown);
        canTalk = true;
    }
}
