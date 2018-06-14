using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerScript : MonoBehaviour
{

    // Use this for initialization
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Hero")
        {
            Debug.Break();
        }

        if (other.tag == "Obstacle" || other.tag == "Coin" || other.tag == "Wave")
        {
            Destroy(other.gameObject);
        }
    }

}
