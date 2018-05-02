using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{

    public LayerMask whatIsSea;
    public float initialPlayerSpeed;
    public float jumpVel = 5.0f;
    public float balloonVel = 20.0f;
    private Rigidbody2D rb;
    private bool onSea;
    //surfboard Transform properties
    private float angle;
    private bool jump = true;
    private Animator animator;

    //Variables used to calculate incremental speed
    private float startPos;
    private float nextPos;
    public float incrementDistance = 100.0f;
    public float incrementSpeed = 0.5f;
    private float speedIncremented = 0.0f;

    public BoardCollision board;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(initialPlayerSpeed, 0.0f, 0.0f);
        animator = GetComponent<Animator>();
        startPos = transform.position.x;
        nextPos = startPos + incrementDistance;
    }

    // Update is called once per frame
    void Update()
    {
        //make sure velocity stays the same
        rb.velocity = new Vector3(initialPlayerSpeed + speedIncremented, rb.velocity.y, 0.0f);
        //Increment score per frame
        GameControl.instance.IncrementScorePerFrame();

        //Check whether speed incremental distance was met
        if (transform.position.x > nextPos)
        {
            startPos = nextPos;
            nextPos = startPos + incrementDistance;
            speedIncremented += incrementSpeed;
        }

        onSea = board.getOnSea();
        if (onSea)
        {
            jump = true;
        }

        if (rb.velocity.y > 1)
        {
            animator.SetTrigger("Jump");
            animator.ResetTrigger("Normal");
            animator.ResetTrigger("Fall");
        }

        if (rb.velocity.y > -0.5 & rb.velocity.y < 0.5)
        {
            animator.SetTrigger("Normal");
            animator.ResetTrigger("Jump");
            animator.ResetTrigger("Fall");
        }
        

        //prevents jumping when paused button is pressed
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            //jump
            if (Input.GetMouseButtonDown(0) && jump)
            {
                GameControl.instance.jumpSound.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpVel);

                if (!onSea)
                {
                    jump = false;
                }
            }
        }

        if (GameControl.instance.isDead == true)
        {
            rb.velocity = Vector3.zero;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            GameControl.instance.onEdibleTakeSound.Play();
            other.gameObject.SetActive(false);
            GameControl.instance.IncrementScore();
        }
        
        if (other.gameObject.CompareTag("Obstacle"))
        {
            //SceneManager.LoadScene(1);
            GameControl.instance.Die();
        }

        if (other.gameObject.CompareTag("Balloon"))
        {
            GameControl.instance.balloonPopSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, balloonVel);
            jump = true;
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wave"))
        {
            rb.velocity = new Vector3(initialPlayerSpeed + speedIncremented, rb.velocity.y, 0.0f);
        }
    }
}