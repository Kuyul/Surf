using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    private Camera Cam;
    private float CamOrigSize;
    private Transform Orig;
    public Transform PlayerPos;
    private Transform PlayerOrig;
    private float diff;

	// Use this for initialization
	void Start () {
        Cam = Camera.main;
        CamOrigSize = Cam.orthographicSize;
        Orig = Cam.GetComponent<Transform>();
        PlayerOrig = PlayerPos;
    }

    // Update is called once per frame
    void Update () {
        if (PlayerPos.position.y > 4)
        {
            float scale = (PlayerPos.position.y - 4 + Orig.position.y) / Orig.position.y;
            Cam.orthographicSize = CamOrigSize * (1 + (scale-1) * 0.7f);
        }
    }
}
