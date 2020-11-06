using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private int[] Players;

    private void Awake()
    {
        Players = SelectionScene.GetPlayers();
    }
}
