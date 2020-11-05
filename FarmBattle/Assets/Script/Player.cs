using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using System;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public int playerId;
    public float speed;
    public float pickDistance;
    public float stunTime;
    public TEAM team;

    private enum ANIM
    {
        IDLE,
        TOP,
        BOTTOM,
        LEFT,
        RIGHT,
        HIT,
        STUNNED,
        BAT
    }

    public enum TEAM
    {
        TEAM1,
        TEAM2
    }

    [HideInInspector]
    public Pickable item;
    [HideInInspector]
    public bool isOnPump = false;
    [HideInInspector]
    public Bucket bucketToFill = null;
    [HideInInspector]
    public bool tryPump = false;
    public Action<Pickable> OnRelease;
    [HideInInspector]
    public bool isHolding = false;

    private bool isStunned = false;
    private Rewired.Player player;
    private Vector2 moveVector;
    private float speedMalus = 0;
    private Rigidbody2D rigidbody;
    private Animator animator;

    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
            float moveX = moveVector.x * speed * (1-speedMalus);
            float moveY = moveVector.y * speed * (1-speedMalus);
            rigidbody.velocity = new Vector3(moveX, moveY, 0);
            if (moveX != 0 || moveY != 0)
            {
                if (moveY >= 0)
                    transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.right, new Vector2(moveX, moveY)));
                else
                    transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, -Vector2.Angle(Vector2.right, new Vector2(moveX, moveY)));
            }
            if (player.GetButtonDown("Action"))
                Action();
            if (player.GetButtonDown("Hit") && isHolding && item.type == Pickable.TYPE.BAT)
                TryHit();
            AnimatorManager(moveX, moveY);
        }
        else
        {
            rigidbody.velocity = new Vector3(0, 0, 0);
            animator.SetInteger("status", Convert.ToInt32(ANIM.STUNNED));
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        transform.GetChild(0).GetChild(0).position = new Vector3(transform.GetChild(0).GetChild(0).position.x, transform.GetChild(0).GetChild(0).position.y, transform.GetChild(0).GetChild(0).position.y);
    }

    private void AnimatorManager(float moveX, float moveY)
    {
        animator.SetFloat("MoveX", moveX);
        animator.SetFloat("MoveY", moveY);

        if (item && item.type == Pickable.TYPE.BAT)
        {
            animator.SetInteger("status", Convert.ToInt32(ANIM.BAT));
        }
        else
        {
            if (moveX == 0 && moveY == 0)
                animator.SetInteger("status", Convert.ToInt32(ANIM.IDLE));
            else if (moveX >= 0 && moveY >= 0)
            {
                if (moveX >= moveY)
                    animator.SetInteger("status", Convert.ToInt32(ANIM.RIGHT));
                else
                    animator.SetInteger("status", Convert.ToInt32(ANIM.TOP));
            }
            else if (moveX >= 0 && moveY <= 0)
            {
                if (moveX >= Mathf.Abs(moveY))
                    animator.SetInteger("status", Convert.ToInt32(ANIM.RIGHT));
                else
                    animator.SetInteger("status", Convert.ToInt32(ANIM.BOTTOM));
            }
            else if (moveX <= 0 && moveY >= 0)
            {
                if (Mathf.Abs(moveX) >= moveY)
                    animator.SetInteger("status", Convert.ToInt32(ANIM.LEFT));
                else
                    animator.SetInteger("status", Convert.ToInt32(ANIM.TOP));
            }
            else if (moveX <= 0 && moveY <= 0)
            {
                if (Mathf.Abs(moveX) >= Mathf.Abs(moveY))
                    animator.SetInteger("status", Convert.ToInt32(ANIM.LEFT));
                else
                    animator.SetInteger("status", Convert.ToInt32(ANIM.BOTTOM));
            }
        }
    }

    private void Action()
    {
        if (item && item.type == Pickable.TYPE.BAT)
            return;
        if (isHolding)
        {
            RemoveItem();
            return;
        }
        Debug.Log("Action");
        RaycastHit2D hit = Physics2D.Raycast(transform.GetChild(0).GetChild(0).position, transform.GetChild(0).GetChild(0).position-transform.position, pickDistance);
        if(hit && hit.transform.tag == "Pickable")
        {
            item = hit.transform.GetComponent<Pickable>();
            item.transform.parent = transform.GetChild(2);
            item.transform.localPosition = Vector3.zero;
            item.isHolding = true;
            isHolding = true;
            animator.SetBool("isHolding", isHolding);
            speedMalus = item.speedMalus / 100.0f;
            item.rigidbody.simulated = false;
            if (item.type == Pickable.TYPE.PUMPKIN)
                (item as Pumpkin).Steal(team);
            if (item.type == Pickable.TYPE.BUCKET)
                item.rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else if (isOnPump)
        {
            tryPump = true;
        }
    }

    private void TryHit()
    {
        Debug.Log("Hit");
        RaycastHit2D hit = Physics2D.Raycast(transform.GetChild(0).GetChild(0).position, transform.GetChild(0).GetChild(0).position - transform.position, pickDistance);
        Player player = hit && hit.transform.tag == "Player" ? hit.transform.GetComponent<Player>() : null;
        if (player != null && player.team != team)
        {
            player.Stunned();
            GameManager.GetInstance.PlayVoice(playerId, "hit");
        }
        item = null;
        isHolding = false;
    }

    public void Stunned()
    {
        isStunned = true;
        StartCoroutine(StunRoutine());
        RemoveItem();
    }

    public void RemoveItem()
    {
        if (!item)
            return;
        item.rigidbody.simulated = true;
        item.transform.parent = null;
        item.isHolding = false;
        OnRelease?.Invoke(item);
        item = null;
        isHolding = false;
        animator.SetBool("isHolding", isHolding);
        speedMalus = 0;
    }

    private Coroutine bubbleCoroutine = null;

    public void ActivateBubble(float time)
    {
        transform.GetChild(1).gameObject.SetActive(true);
        bubbleCoroutine = StartCoroutine(BubbleRoutine(time));
    }

    public void DesactivateBubble()
    {
        if (bubbleCoroutine != null)
            StopCoroutine(bubbleCoroutine);
        transform.GetChild(1).gameObject.SetActive(false);
        bubbleCoroutine = null;
    }

    IEnumerator BubbleRoutine(float time)
    {
        yield return new WaitForSeconds(time);
        bubbleCoroutine = null;
        DesactivateBubble();
    }

    IEnumerator StunRoutine()
    {
        yield return new WaitForSeconds(stunTime);
        isStunned = false;
        GameManager.GetInstance.PlayVoice(playerId, "get-hit");
    }
}
