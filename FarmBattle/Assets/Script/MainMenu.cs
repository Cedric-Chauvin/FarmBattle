using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int playerId;
    public string sceneToLoad;
    public GameObject pressA;
    public float pressACooldownAppear;
    public float pressACooldownDisappear;

    private Rewired.Player player;
    private bool aAppear = false;
    private bool aDisappear = true;

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

        if (aAppear)
        {
            pressA.SetActive(true);
            StartCoroutine(PressACooldownAppear());
        }
        if (aDisappear)
        {
            pressA.SetActive(false);
            StartCoroutine(PressACooldownDisappear());
        }
    }

    IEnumerator PressACooldownAppear()
    {
        aAppear = false;
        yield return new WaitForSeconds(pressACooldownAppear);
        aDisappear = true;
    }

    IEnumerator PressACooldownDisappear()
    {
        aDisappear = false;
        yield return new WaitForSeconds(pressACooldownDisappear);
        aAppear = true;
    }
}
