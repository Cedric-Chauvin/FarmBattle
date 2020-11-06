using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int playerId;
    public string sceneToLoad;

    private Rewired.Player player;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
    }

    private void Update()
    {
        if (player.id == playerId && player.GetButtonDown("Action"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
