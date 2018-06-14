using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternDestroyer : MonoBehaviour
{

    // Use this for initialization
    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

}
