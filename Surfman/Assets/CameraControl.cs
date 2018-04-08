using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Transform playerToFollow;
    private Vector3 originalPosition;
    private Vector3 playerOriginalPosition;
    private float diff;

	// Use this for initialization
	void Start () {
        diff = transform.position.y - playerToFollow.position.y;

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 newPosition = transform.position;
        newPosition.y = diff + playerToFollow.position.y;
        transform.position = newPosition;
	}
}
