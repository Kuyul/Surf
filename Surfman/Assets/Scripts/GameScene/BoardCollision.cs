using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCollision : MonoBehaviour {

    private bool onSea = true;

    public bool getOnSea()
    {
        return onSea;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wave") || collision.gameObject.CompareTag("Sea"))
        {
            onSea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wave") || collision.gameObject.CompareTag("Sea"))
        {
            onSea = false;
        }
    }
}
