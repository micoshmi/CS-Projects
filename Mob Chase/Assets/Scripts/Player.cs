using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float movingForce = 10f;
    [SerializeField]
    private float jumpingForce = 11f;

    private float movementX;
    private Rigidbody2D myBody;
    private SpriteRenderer sr;
    private Animator anim;
    private string walkingAnimation = "Walk";

    private bool onGround = true;
    private string groundTag = "Ground";
    private string enemyTag = "Enemy";
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMovementKeyboard();
        animatePlayer();
        playerJump();
    }
    void playerMovementKeyboard()
    {
        //player movement, on axis X
        movementX = Input.GetAxisRaw("Horizontal"); //outputs -1,0,1 in the background according to what keys are being pressed
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * movingForce;
    }
    void animatePlayer()
    {
        //moving to the right side
        if(movementX > 0)
        { 
            anim.SetBool(walkingAnimation, true);
            sr.flipX = false;
        }
        //moving to the left side
        else if (movementX < 0)
        {
            anim.SetBool(walkingAnimation, true);
            sr.flipX = true;
        }
        //idle standing
        else
        {
            anim.SetBool(walkingAnimation, false);
        }
    }
    void playerJump()
    {
        //predefined jump by UnityEngine
        if (Input.GetButtonDown("Jump") && onGround)
        {
            onGround = false;
            //applying force to the rigidbody
            myBody.AddForce(new Vector2(0f, jumpingForce), ForceMode2D.Impulse);
        }
    }
    //function used to detect collision between game objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            onGround = true;
        }
        if(collision.gameObject.CompareTag(enemyTag))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //player object get destryed when colliding with enemy object
        if(collision.CompareTag(enemyTag))
        {
            Destroy(gameObject);
        }
    }
}
