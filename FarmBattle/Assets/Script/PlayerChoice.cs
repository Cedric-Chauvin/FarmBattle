using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerChoice : MonoBehaviour
{
    public int playerId;
    public PlayerSelection[] Player;

    private Rewired.Player player;
    private PlayerSelection.STATUS status;
    private int playerNumber;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
        status = PlayerSelection.STATUS.UNSELECTED;
        playerNumber = -1;
    }

    private void Update()
    {
        if (status == PlayerSelection.STATUS.UNSELECTED)
        {
            if (player.GetButtonDown("X") && Player[0].status == PlayerSelection.STATUS.UNSELECTED) // X
            {
                Player[0].status = PlayerSelection.STATUS.SELECTED;
                Player[0].playerId = player.id;
                Player[0].gameObject.transform.GetChild(0).gameObject.SetActive(false);
                Player[0].gameObject.transform.GetChild(1).gameObject.SetActive(true);

                status = PlayerSelection.STATUS.SELECTED;
                playerNumber = 0;

            }
            if (player.GetButtonDown("Action") && Player[1].status == PlayerSelection.STATUS.UNSELECTED) // A
            {
                Player[1].status = PlayerSelection.STATUS.SELECTED;
                Player[1].playerId = player.id;
                Player[1].gameObject.transform.GetChild(0).gameObject.SetActive(false);
                Player[1].gameObject.transform.GetChild(1).gameObject.SetActive(true);

                status = PlayerSelection.STATUS.SELECTED;
                playerNumber = 1;
            }
            if (player.GetButtonDown("Hit") && Player[2].status == PlayerSelection.STATUS.UNSELECTED) // B
            {
                Player[2].status = PlayerSelection.STATUS.SELECTED;
                Player[2].playerId = player.id;
                Player[2].gameObject.transform.GetChild(0).gameObject.SetActive(false);
                Player[2].gameObject.transform.GetChild(1).gameObject.SetActive(true);

                status = PlayerSelection.STATUS.SELECTED;
                playerNumber = 2;
            }
            if (player.GetButtonDown("Y") && Player[3].status == PlayerSelection.STATUS.UNSELECTED) // Y
            {
                Player[3].status = PlayerSelection.STATUS.SELECTED;
                Player[3].playerId = player.id;
                Player[3].gameObject.transform.GetChild(0).gameObject.SetActive(false);
                Player[3].gameObject.transform.GetChild(1).gameObject.SetActive(true);

                status = PlayerSelection.STATUS.SELECTED;
                playerNumber = 3;
            }
        }
        else if (status == PlayerSelection.STATUS.SELECTED)
        {
            if (player.GetButtonDown("Start"))
            {
                Player[playerNumber].status = PlayerSelection.STATUS.VALIDATED;
                Player[playerNumber].gameObject.transform.GetChild(1).gameObject.SetActive(false);
                Player[playerNumber].gameObject.transform.GetChild(2).gameObject.SetActive(true);

                status = PlayerSelection.STATUS.VALIDATED;
            }
            if (player.GetButtonDown("Hit"))
            {
                Player[playerNumber].status = PlayerSelection.STATUS.UNSELECTED;
                Player[playerNumber].playerId = -1;
                Player[playerNumber].gameObject.transform.GetChild(1).gameObject.SetActive(false);
                Player[playerNumber].gameObject.transform.GetChild(0).gameObject.SetActive(true);

                status = PlayerSelection.STATUS.UNSELECTED;
                playerNumber = -1;
            }
        }
        else if (status == PlayerSelection.STATUS.VALIDATED)
        {
            if (player.GetButtonDown("Hit"))
            {
                Player[playerNumber].status = PlayerSelection.STATUS.SELECTED;
                Player[playerNumber].gameObject.transform.GetChild(2).gameObject.SetActive(false);
                Player[playerNumber].gameObject.transform.GetChild(1).gameObject.SetActive(true);

                status = PlayerSelection.STATUS.SELECTED;
            }
        }
    }
}
