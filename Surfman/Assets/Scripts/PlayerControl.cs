using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{

    public LayerMask whatIsSea;
    public float jumpVel = 5.0f;
    public float balloonVel = 20.0f;
    private Rigidbody2D rb;
    private bool onSea;
    //surfboard Transform properties
    private float angle;
    private bool jump = true;
    public AudioSource onEdibleTakeSound;
    public AudioSource balloonPopSound;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //center position of the box, size of the box, roation around the z axis
        onSea = Physics2D.OverlapBox(transform.position, transform.localScale, transform.rotation.z, whatIsSea);
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

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            onEdibleTakeSound.Play();
            other.gameObject.SetActive(false);
        }
        
        if (other.gameObject.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(0);
        }

        if (other.gameObject.CompareTag("Balloon"))
        {
            balloonPopSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, balloonVel);
            jump = true;
            other.gameObject.SetActive(false);
        }
    }
}