using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using System;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject J1;
    public GameObject J2;
    public GameObject J3;
    public GameObject J4;
    public string sceneToLoad;

    private int[] Players;
    private Rewired.Player player1;
    private Rewired.Player player2;
    private Rewired.Player player3;
    private Rewired.Player player4;

    private void Awake()
    {
        Players = SelectionScene.GetPlayers();
        player1 = ReInput.players.GetPlayer(Players[0]);
        player2 = ReInput.players.GetPlayer(Players[1]);
        player3 = ReInput.players.GetPlayer(Players[2]);
        player4 = ReInput.players.GetPlayer(Players[3]);
    }

    private void Update()
    {
        CheckInput();
        AllPlayerReady();
    }

    private void CheckInput()
    {
        if (player1.GetButtonDown("Action"))
            J1.SetActive(!J1.activeSelf);
        if (player2.GetButtonDown("Action"))
            J2.SetActive(!J2.activeSelf);
        if (player3.GetButtonDown("Action"))
            J3.SetActive(!J3.activeSelf);
        if (player4.GetButtonDown("Action"))
            J4.SetActive(!J4.activeSelf);
    }

    private void AllPlayerReady()
    {
        if (J1.activeSelf && J2.activeSelf && J3.activeSelf && J4.activeSelf)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
