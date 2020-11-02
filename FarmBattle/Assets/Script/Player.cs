using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Player : MonoBehaviour
{
    public int playerId;
    public float speed;
    [HideInInspector]
    public bool isStunned = false;

    private Rewired.Player player;
    private Vector2 moveVector;

    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
    }

    void Update()
    {
        GetInput();
        ProcessInput();
    }

    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        moveVector.x = player.GetAxis("Move Horizontal"); // get input by name or action id
        moveVector.y = player.GetAxis("Move Vertical");
    }

    private void ProcessInput()
    {
        if (!isStunned)
        {
            float moveX = moveVector.x * speed * Time.deltaTime;
            float moveY = moveVector.y * speed * Time.deltaTime;
            transform.position += new Vector3(moveX, moveY, 0);
        }
    }
}
