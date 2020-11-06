using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class SelectionScene : MonoBehaviour
{
    public PlayerSelection Player1;
    public PlayerSelection Player2;
    public PlayerSelection Player3;
    public PlayerSelection Player4;
    public string sceneToLoad;

    public static int player1;
    public static int player2;
    public static int player3;
    public static int player4;

    private void Update()
    {
        AllPlayerReady();
    }

    private void AllPlayerReady()
    {
        if (Player1.status == PlayerSelection.STATUS.VALIDATED
            && Player2.status == PlayerSelection.STATUS.VALIDATED
            && Player3.status == PlayerSelection.STATUS.VALIDATED
            && Player4.status == PlayerSelection.STATUS.VALIDATED)
        {
            player1 = Player1.playerId;
            player2 = Player2.playerId;
            player3 = Player3.playerId;
            player4 = Player4.playerId;

            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public static int[] GetPlayers()
    {
        int[] Players = new int[4];
        Players[0] = player1;
        Players[1] = player2;
        Players[2] = player3;
        Players[3] = player4;

        return Players;
    }
}
