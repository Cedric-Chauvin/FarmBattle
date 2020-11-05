using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public Text team1Score;
    public Text team2Score;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void AddPoint(Player.TEAM team, int point)
    {
        if (team == Player.TEAM.TEAM1)
            team1Score.text = (int.Parse(team1Score.text) + point).ToString();  
        else
            team2Score.text = (int.Parse(team2Score.text) + point).ToString();
    }

    static public UIManager GetInstance() => instance;
}
