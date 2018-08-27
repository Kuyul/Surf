using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RESETPLAYERPREFS : MonoBehaviour {

    public void resetAll()
    {
        PlayerPrefs.DeleteAll();
    }

}
