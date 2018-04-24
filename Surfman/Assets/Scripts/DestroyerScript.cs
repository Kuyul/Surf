using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerScript : MonoBehaviour
{

    // Use this for initialization
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hero")
        {
            Debug.Break();
        }

        if (other.tag == "Pattern")
        {
            Destroy(other.gameObject);
        }
    }

}
