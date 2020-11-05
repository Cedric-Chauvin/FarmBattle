using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidationArea : MonoBehaviour
{
    public Player.TEAM team;

    List<Player> players = new List<Player>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        Player player = collision.GetComponent<Player>();
        if (player != null && player.team == team)
            players.Add(collision.GetComponent<Player>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        Player player = collision.GetComponent<Player>();
        if (player != null && player.team == team)
            players.Remove(collision.GetComponent<Player>());
    }

    private void Update()
    {
        foreach (Player player in players)
        {
            if(player.item !=null && player.item.type == Pickable.TYPE.PUMPKIN)
            {
                player.item.UseObject(player.team);
                Destroy(player.item.gameObject);
                player.RemoveItem();
                GameManager.GetInstance.PlayVoice(team, "success");
            }
        }
    }

}
