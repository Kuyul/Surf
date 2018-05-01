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

    public BoardCollision board;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(initialPlayerSpeed, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //center position of the box, size of the box, roation around the z axis
        //        onSea = Physics2D.OverlapBox(transform.position, transform.localScale, transform.rotation.z, whatIsSea);
        onSea = board.getOnSea();
        if (onSea)
        {
            jump = true;
        }


        //prevents jumping when paused button is pressed
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            //jump
            if (Input.GetMouseButtonDown(0) && jump)
            {
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
            rb.velocity = new Vector3(initialPlayerSpeed, rb.velocity.y, 0.0f);
        }
    }
}