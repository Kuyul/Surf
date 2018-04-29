using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardCollision {

    public static bool onSea = true;

    public static void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sea"))
        {
            onSea = true;
        }
    }

    public static void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sea"))
        {
            onSea = false;
        }
    }
}
