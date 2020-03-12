using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterConroller2D : MonoBehaviour
{
    public float speed;
    public float smooth;
    public float jumpForce;
    bool facingRight = true;

    bool grounded;

    public Collider2D groundCheck;
    public LayerMask groundLayer;

    Animator animator;
    Rigidbody2D rb2d;

    int numberOfJumps=1;
    int maxJumps = 2;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        //Get a reference to the Animator
        animator = GetComponent<Animator>();
        
    }
    private void Update()
    {
        //Use a grounded variable
        grounded = groundCheck.IsTouchingLayers(groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && (grounded || numberOfJumps < maxJumps))
        {
            rb2d.AddForce(new Vector2(0f, jumpForce));
            numberOfJumps++;
        }
        if (grounded)
            numberOfJumps = 1;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Get Input
        float x = Input.GetAxisRaw("Horizontal");

        //Update the animator
        animator.SetBool("Running", x != 0);
        animator.SetBool("Jumping", !grounded);
        //if the input is moving the player right and the player is facing left...
        if (x > 0 && !facingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (x < 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }

        //Update the player's velocity
        Vector2 targetVelocity = new Vector2(x * speed, rb2d.velocity.y);

        rb2d.velocity = Vector2.SmoothDamp(rb2d.velocity, targetVelocity,
            ref targetVelocity, Time.deltaTime * smooth);

        
         

}

    void Flip()
    {
        //Invert the facingRight variable
        facingRight = !facingRight;

        //Flip the Character
        Vector2 scale = transform.localScale;

        scale.x *= -1;

        transform.localScale = scale;
    }
}
