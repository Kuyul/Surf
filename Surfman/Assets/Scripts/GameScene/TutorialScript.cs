using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour {

    //Next Trigger
    public GameObject NextTrigger;
    public GameObject Player;
    private Rigidbody2D PlayerR;
    //Current trigger's Collider
    private BoxCollider2D ThisCollider;
    public bool upSwipeToRelease;
    private Vector3 TouchPosition;
    private float SwipeResistanceY = 10.0f;
    private bool IsDragging = false;

    private void Start()
    {
        PlayerR = PlayerControl.instance.gameObject.GetComponent<Rigidbody2D>();
        ThisCollider = this.gameObject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //duplicate code from Playercontrol.... this is probably not the best way to do it, but oh well... I'll let it slide
        if (Input.GetMouseButtonDown(0))
        {
            TouchPosition = Input.mousePosition;
            IsDragging = true;
        }

        //While dragging is true, either jump or fall.
        if (IsDragging)
        {
            Vector2 deltaSwipe = TouchPosition - Input.mousePosition;
            if (Mathf.Abs(deltaSwipe.y) > SwipeResistanceY)
            {
                if (upSwipeToRelease)
                {
                    if (deltaSwipe.y < 0)
                    {
                        Release();
                    }

                }
                else
                {
                    if (deltaSwipe.y > 0)
                    {
                        Release();
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            IsDragging = false;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Freeze the player's rigidbody
        Time.timeScale = 0;
        //To prevent further collision while the player is still in contact with this collider
        ThisCollider.enabled = false;
        //Disable score update
    }

    private void Release()
    {
        Time.timeScale = 1;
        //Enable next trigger when the successfuly performs the action described here
        if (NextTrigger != null)
        {
            NextTrigger.SetActive(true);
        }
        //Entering this else statement implies that the user has successfully completed the tutorial.
        //Tutorial pattern will no longer be played in the future.
        else
        {
            PlayerPrefs.SetInt("ComTutorial", 1);
        }
        IsDragging = false;
    }
}
