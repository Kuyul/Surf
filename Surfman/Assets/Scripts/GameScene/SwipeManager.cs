using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwipeDirection
{
    None = 0,
    Left = 1,
    Right = 2,
    Up = 4,
    Down = 8,
}

public class SwipeManager : MonoBehaviour {

    private static SwipeManager instance;
    public static SwipeManager Instance { get { return instance; } }

    public SwipeDirection Direction { set; get; }
    private Vector3 touchPosition;
    private float swipeResistanceX = 50.0f;
    private float swipeResistanceY = 10.0f;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        Direction = SwipeDirection.None;

        //Whenever someone presses on the screen, we're simply going to record where it is pressed
        if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 deltaSwipe = touchPosition - Input.mousePosition;

            if(Mathf.Abs(deltaSwipe.x) > swipeResistanceX)
            {
                // Swipe on the X axis
                Direction |= (deltaSwipe.x < 0)? SwipeDirection.Right : SwipeDirection.Left;
                Debug.Log(Direction);
            }

            if (Mathf.Abs(deltaSwipe.y) > swipeResistanceY)
            {
                // Swipe on the Y axis
                Direction |= (deltaSwipe.y < 0) ? SwipeDirection.Up : SwipeDirection.Down;
                Debug.Log(Direction);
            }
        }
        
    }

    public bool IsSwiping(SwipeDirection dir)
    {
        return (Direction == dir);
    }
}
