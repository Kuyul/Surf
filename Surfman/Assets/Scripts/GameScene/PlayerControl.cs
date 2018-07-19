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
    private Animator boardAnimator;
    public Animator playerAnimator;

    public GameObject balloonExplosion;
    public GameObject starExplosion;
    public GameObject waveExplosion;

    //Variables used to calculate incremental speed
    private float startPos;
    private float nextPos;
    public float incrementDistance = 100.0f;
    public float incrementSpeed = 0.5f;
    public float maxFallSpeed = -15.0f;
    private float speedIncremented = 0.0f;

    //Swipe variable
    public float downSpeed = -10.0f;
    private Vector3 touchPosition;
    private float swipeResistanceY = 10.0f;
    private bool isDragging = false;

    public BoardCollision board;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(initialPlayerSpeed, 0.0f, 0.0f);
        boardAnimator = GetComponent<Animator>();
        startPos = transform.position.x;
        nextPos = startPos + incrementDistance;
    }

    // Update is called once per frame
    void Update()
    {
        float yVel = rb.velocity.y;
        if (rb.velocity.y < maxFallSpeed)
        {
            yVel = maxFallSpeed;
        }
        //make sure velocity stays the same
        rb.velocity = new Vector3(initialPlayerSpeed + speedIncremented, yVel, 0.0f);


        //SwipeManager
        bool up = false;
        bool down = false;
        if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
            isDragging = true;
        }

        //While dragging is true, either jump or fall.
        if (isDragging)
        {
            Vector2 deltaSwipe = touchPosition - Input.mousePosition;
            if (Mathf.Abs(deltaSwipe.y) > swipeResistanceY)
            {
                if (deltaSwipe.y < 0)
                {
                    up = true;
                }
                else
                {
                    down = true;
                }
                isDragging = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            
        }

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

        //Swipe down to fall at maximum velocity
        if (!onSea)
        {
            if (down)
            {
                rb.velocity = new Vector3(initialPlayerSpeed + speedIncremented, downSpeed, 0.0f);
                playerAnimator.SetTrigger("Fall");
                playerAnimator.ResetTrigger("Jump");
                playerAnimator.ResetTrigger("DJump");
                playerAnimator.ResetTrigger("Normal");
            }
        }

        //Control player animation
        if (rb.velocity.y > 1)
        {
            boardAnimator.SetTrigger("Jump");
            boardAnimator.ResetTrigger("Normal");
        }

        if (rb.velocity.y > -0.5 & rb.velocity.y < 0.5)
        {
            boardAnimator.SetTrigger("Normal");
            boardAnimator.ResetTrigger("Jump");
            playerAnimator.SetTrigger("Normal");
            playerAnimator.ResetTrigger("Jump");
            playerAnimator.ResetTrigger("DJump");
            playerAnimator.ResetTrigger("Fall");
        }

        //prevents jumping when paused button is pressed
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            //jump
            if (up && jump)
            {
                GameControl.instance.jumpSound.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpVel);

                //double Jump
                if (!onSea)
                {
                    jump = false;
                    playerAnimator.SetTrigger("DJump");
                    playerAnimator.ResetTrigger("Normal");
                    playerAnimator.ResetTrigger("Fall");
                    playerAnimator.ResetTrigger("Jump");
                }
                //normal jump
                else
                {
                    playerAnimator.SetTrigger("Jump");
                    playerAnimator.ResetTrigger("Normal");
                    playerAnimator.ResetTrigger("Fall");
                    playerAnimator.ResetTrigger("DJump");
                }
            }
        }

        if (GameControl.instance.isDead == true)
        {
            rb.velocity = Vector3.zero;
        }

        Destroy(GameObject.Find("balloonExplosion(Clone)"), 0.6f);
        Destroy(GameObject.Find("starExplosion(Clone)"), 1f);
        Destroy(GameObject.Find("waveExplosion(Clone)"), 0.9f);


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sea"))
        {
            Instantiate(waveExplosion,transform.position, Quaternion.identity);
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            GameControl.instance.onEdibleTakeSound.Play();
            other.gameObject.SetActive(false);
            GameControl.instance.IncrementScore();
        }

        if (other.gameObject.CompareTag("Star"))
        {
            GameControl.instance.starSound.Play();
            other.gameObject.SetActive(false);        
            Instantiate(starExplosion, other.transform.position, Quaternion.identity);
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
            Instantiate(balloonExplosion, other.transform.position,Quaternion.identity);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wave") || other.gameObject.CompareTag("Sea"))
        {
            rb.velocity = new Vector3(initialPlayerSpeed + speedIncremented, rb.velocity.y, 0.0f);
        }
    }
}