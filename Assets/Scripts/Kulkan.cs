using UnityEngine;
using System.Collections;

public class Kulkan : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    [HideInInspector]
    private bool grounded;

    public float moveForce = 365f;
    public float maxSpeed = 15f;
    public float jumpForce = 1500f;

    private Animator anim;
    private Rigidbody2D rb2d;
    private int layerFloor;

    // Use this for initialization
    void Awake()
    {
        layerFloor = LayerMask.NameToLayer("Floor");
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        if (!grounded)
            h = 0;

        anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (jump)
        {
            anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.layer.Equals(layerFloor))
        {
            grounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer.Equals(layerFloor))
        {
            grounded = false;
        }
    }

    public enum State
    {
        IDLE,
        WALKING,
        RUNNING,
        AIR_UP,
        AIR_DOWN,
        HURT,
        CROUCHING
    }
}
