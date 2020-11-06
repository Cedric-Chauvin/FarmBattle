using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public int maxScore = 27;
    public Text team1Score;
    public Text team2Score;
    public RectTransform demoPanel;
    public Text demoText;
    public RectTransform repuPanel;
    public Text repuText;

    private int score1 = 27;
    private int score2 = 0;
    private bool endGme = false;
    private Rewired.Player player;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Update()
    {
        if (!endGme)
        {
            if (score1 >= 27)
            {
                repuPanel.gameObject.SetActive(true);
                repuText.text = score1.ToString() + "/" + score2.ToString();
                EndGame();
            }
            else if (score2 >= 27)
            {
                demoPanel.gameObject.SetActive(false);
                demoText.text = score2.ToString() + "/" + score1.ToString();
                EndGame();
            }
        }
        if(endGme)
        {
            if (player.GetButtonDown("Action"))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            if (player.GetButtonDown("Hit"))
                SceneManager.LoadScene("MainMenu");
            if (player.GetButtonDown("Y"))
                SceneManager.LoadScene("Selection screen");

        }
    }

    private void EndGame()
    {
        Time.timeScale = 0;
        team1Score.transform.parent.gameObject.SetActive(false);
        team2Score.transform.parent.gameObject.SetActive(false);
        player = Rewired.ReInput.players.GetPlayer(0);
        endGme = true;
    }

    public void AddPoint(Player.TEAM team, int point)
    {
        if (team == Player.TEAM.TEAM1)
        {
            score1 += point;
            team1Score.text = (score1).ToString();
        }
        else
        {
            score2 += point;
            team2Score.text = (score2).ToString();
        }
    }

    static public UIManager GetInstance() => instance;
}
