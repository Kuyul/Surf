using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{

    public static PlayerControl instance;

    public GameObject[] Players;
    public GameObject[] Boards;

    //Player variables
    public LayerMask whatIsSea;
    public float PlayerSpeed;
    public float jumpVel = 5.0f;
    public float balloonVel = 20.0f;
    private Rigidbody2D rb;
    private bool onSea;
    private bool Dead;

    //surfboard Transform properties
    private float angle;
    private bool jump = true;
    private Animator boardAnimator;
    public Animator playerAnimator;

    //Particle effects
    public GameObject balloonExplosion;
    public GameObject starExplosion;
    public GameObject waveExplosion;

    //Variables used to calculate incremental speed
    public float maxFallSpeed = -15.0f;
    private float speedIncremented = 0.0f;

    //Swipe variable
    public float downSpeed = -10.0f;
    private Vector3 touchPosition;
    private float swipeResistanceY = 10.0f;
    private bool isDragging = false;

    public BoardCollision board;

    // Use this for initialization

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(PlayerSpeed, 0.0f, 0.0f);
        boardAnimator = GetComponent<Animator>();
        Dead = false;
    }

    // Update is called once per frame
    private void Update()
    {
        float yVel = rb.velocity.y;
        if (rb.velocity.y < maxFallSpeed)
        {
            yVel = maxFallSpeed;
        }
        //make sure velocity stays the same
        rb.velocity = new Vector3(PlayerSpeed, yVel, 0.0f);


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
                rb.velocity = new Vector3(PlayerSpeed, downSpeed, 0.0f);
                //Set animation trigger to Fall
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
            //Set animation trigger to Normal
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
                    //Set animation trigger to Double Jump
                    playerAnimator.SetTrigger("DJump");
                    playerAnimator.ResetTrigger("Normal");
                    playerAnimator.ResetTrigger("Fall");
                    playerAnimator.ResetTrigger("Jump");
                }
                //normal jump
                else
                {
                    //Set animation trigger to Jump
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

        if (other.gameObject.CompareTag("Shell"))
        {
            other.gameObject.SetActive(false);              
            GameControl.instance.IncrementScore();
            GameControl.instance.onEdibleTakeSound.Play();
            PlayerPrefs.SetInt("Shell", (PlayerPrefs.GetInt("Shell", 0) + 1));
        }
        if (other.gameObject.CompareTag("Starfish"))
        {
            other.gameObject.SetActive(false);
            GameControl.instance.IncrementScore();
            GameControl.instance.onEdibleTakeSound.Play();
            PlayerPrefs.SetInt("Starfish", (PlayerPrefs.GetInt("Starfish", 0) + 1));
        }
        if (other.gameObject.CompareTag("Clam"))
        {
            other.gameObject.SetActive(false);
            GameControl.instance.IncrementScore();
            GameControl.instance.onEdibleTakeSound.Play();
            PlayerPrefs.SetInt("Clam", (PlayerPrefs.GetInt("Clam", 0) + 1));
        }
        if (other.gameObject.CompareTag("Fish"))
        {
            other.gameObject.SetActive(false);
            GameControl.instance.IncrementScore();
            GameControl.instance.onEdibleTakeSound.Play();
            PlayerPrefs.SetInt("Fish", (PlayerPrefs.GetInt("Fish", 0) + 1));
        }

        if (other.gameObject.CompareTag("Star"))
        {
            other.gameObject.SetActive(false);
            GameControl.instance.IncrementScore();
            GameControl.instance.starSound.Play();
            PlayerPrefs.SetInt("Star", (PlayerPrefs.GetInt("Star", 0) + 1));
            Instantiate(starExplosion, other.transform.position, Quaternion.identity);

        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            Die();
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

    private void Die()
    {
        if (!Dead)
        {
            Dead = true;
            GameControl.instance.Die();
            //Set animation trigger to Die
            playerAnimator.SetTrigger("Die");
            playerAnimator.ResetTrigger("Normal");
            playerAnimator.ResetTrigger("Fall");
            playerAnimator.ResetTrigger("Jump");
            playerAnimator.ResetTrigger("DJump");
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wave") || other.gameObject.CompareTag("Sea"))
        {
            rb.velocity = new Vector3(PlayerSpeed, rb.velocity.y, 0.0f);
        }
    }
}