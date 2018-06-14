using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(LoadMainScene());
	}

    IEnumerator LoadMainScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
