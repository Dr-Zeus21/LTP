using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    private Vector2 move;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Quaternion quat;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb.inertia = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        transform.rotation = quat;
        rb.angularVelocity = 0;
    }

    private void Move()
    {
        // checking 'project settings(under edit)' axis, then multiply by change in time per frame and moveSpeed
        var deltaX = Input.GetAxis("Horizontal") * moveSpeed; 
        var deltaY = Input.GetAxis("Vertical") * moveSpeed;

        // moving pos to current pos + delta, and Mathf.Clamp keeps it on the screen

        move = new Vector2(deltaX, deltaY);
        if(deltaX < 0)
        {
            sr.flipX = true;
        }

        if(deltaX > 0)
        {
            sr.flipX = false;
        }
        rb.velocity = move;
    }

    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Mirror" || other.gameObject.tag == "Backing" || other.gameObject.tag == "Goal")
        {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
