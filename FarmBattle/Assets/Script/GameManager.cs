using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;

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

    private void Start()
    {
        PlayVoice(Player.TEAM.TEAM1, "start");
        Invoke("BidenReply", 4f);
        foreach (Player item in players)
        {
            item.Stunned(9.50f);
        }
    }

    private void BidenReply()
    {
        PlayVoice(Player.TEAM.TEAM2, "start");
    }


    public void PlayVoice(Player.TEAM team,string name)
    {
        if (!CanPlay())
            return;
        int index;
        if (team == Player.TEAM.TEAM1)
            index = Random.Range(2, 4);
        else
            index = Random.Range(0, 2);
        PlaySound(players[index], name);
    }
    public void PlayVoice(int id,string name)
    {
        if (!CanPlay())
            return;
        Player p = players.First(x => x.playerId == id);
        PlaySound(p, name);
    }
    public void PlayVoice(int attackID,int targetID,string nameAttack,string nameTarget)
    {
        if (!CanPlay())
            return;
        if (Random.Range(0, 100) > 50)
            PlaySound(players.First(x => x.playerId == attackID), nameAttack);
        else
            PlaySound(players.First(x => x.playerId == targetID), nameTarget);

    }

    private bool CanPlay()
    {
        if (lastVoice != "" && SoundManager.Instance.isPlaying(lastVoice))
        {

            if (lastVoice == "success")
                return false;
            SoundManager.Instance.StopSound(lastVoice);
            lastPlayer.DesactivateBubble();
        }
        return true;
    }
    private void PlaySound(Player p,string name)
    {
        if (name == "success")
            lastVoice = name;
        else
            lastVoice = p.team == Player.TEAM.TEAM1 ? "trump-" + name : "biden-" + name;
        float time = SoundManager.Instance.PlaySound(lastVoice);
        if (name != "success")
            p.ActivateBubble(time);
        lastPlayer = p;
    }
}
