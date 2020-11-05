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
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().OnRelease += OnPutBucket;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().OnRelease -= OnPutBucket;
        }
    }

    private void OnPutBucket(Pickable pickable)
    {
        if (transform.GetChild(0).childCount == 0 && pickable.type!= Pickable.TYPE.BUCKET)
            return;
        Bucket b = pickable as Bucket;
        b.transform.parent = transform.GetChild(0);
        b.transform.position = Vector3.zero;
        b.rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        bucket = b;
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
