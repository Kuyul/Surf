using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingComponents : MonoBehaviour {

    public Transform playerToFollow;
    private float diff;

    // Use this for initialization
    void Start () {
        diff = transform.position.x - playerToFollow.position.x;
    }

    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = diff + playerToFollow.position.x;
        transform.position = newPosition;
    }
}
