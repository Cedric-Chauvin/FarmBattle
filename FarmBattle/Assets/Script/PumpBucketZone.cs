using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpBucketZone : MonoBehaviour
{
    [HideInInspector]
    public Bucket bucket = null;
    [HideInInspector]
    public bool isPumping = false;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickable"))
        {
            Bucket b = collision.gameObject.GetComponent<Bucket>();
            if (b && bucket == null)
            {
                bucket = b;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickable"))
        {
            Bucket b = collision.gameObject.GetComponent<Bucket>();
            if (b && bucket == b)
            {
                bucket = null;
            }
        }
    }

    private void Update()
    {
        if (isPumping)
        {
            animator.SetTrigger("isPumping");
            isPumping = false;
        }
    }
}
