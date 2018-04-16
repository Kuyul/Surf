using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{

    public LayerMask whatIsSea;
    public float jumpVel = 5.0f;
    public float baloonVel = 20.0f;
    private Rigidbody2D rb;
    private bool onSea;
    //surfboard Transform properties
    private float angle;
    private bool jump = true;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
        }
        
        if (other.gameObject.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(0);
        }

        if (other.gameObject.CompareTag("Balloon"))
        {
            rb.velocity = new Vector2(rb.velocity.x, baloonVel);
            other.gameObject.SetActive(false);
        }
    }
}