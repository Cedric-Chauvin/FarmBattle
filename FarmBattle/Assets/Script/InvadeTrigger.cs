using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Player.TEAM team;
    public float cooldown;

    private bool canTalk;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && canTalk)
        {
            GameManager.GetInstance.PlayVoice(team, "invade");
            canTalk = false;
        }
    }

    private IEnumerator Routine()
    {
        yield return new WaitForSeconds(cooldown);
        canTalk = true;
    }
}
