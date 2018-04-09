using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

	public void LoadLevel(int level)
    {
        SceneManager.LoadScene(1);
    }
}
