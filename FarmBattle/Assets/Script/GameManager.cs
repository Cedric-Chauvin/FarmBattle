using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Player[] players;

    private static int[] ID = new int[4];

    private static GameManager instance;
    private Player lastPlayer;
    private string lastVoice="";

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        instance = null;
    }
    public static GameManager GetInstance => instance; 

    public void PlayVoice(Player.TEAM team,string name)
    {
        if(lastVoice != "" && SoundManager.Instance.isPlaying(lastVoice))
        {
            SoundManager.Instance.StopSound(lastVoice);
            lastPlayer.DesactivateBubble();
        }
        int index;
        if (team == Player.TEAM.TEAM1)
            index = Random.Range(1, 3);
        else
        {
            index = Random.Range(0, 2);
            if (index == 1)
                index += 2;
        }
        lastVoice = team == Player.TEAM.TEAM1 ? "trump-" + name : "biden-" + name;
        float time = SoundManager.Instance.PlaySound(lastVoice);
        players[index].ActivateBubble(time);
        lastPlayer = players[index];
    }
    public void PlayVoice(int id,string name)
    {
        if (lastVoice != "" && SoundManager.Instance.isPlaying(lastVoice))
        {
            SoundManager.Instance.StopSound(lastVoice);
            lastPlayer.DesactivateBubble();
        }
        Player p = players.First(x => x.playerId == id);
        lastVoice = p.team == Player.TEAM.TEAM1 ? "trump-" + name : "biden-" + name;
        float time = SoundManager.Instance.PlaySound(lastVoice);
        p.ActivateBubble(time);
        lastPlayer = p;
    }
}
