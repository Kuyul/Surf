using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioController : MonoBehaviour {

    //
    public static AudioController instance;
    //Die Sounds
    public AudioSource DieLogSound;
    public AudioSource DieSeagullSound;
    public AudioSource DieSharkSound;
    public AudioSource DieTidalSound;

    //Jump Sounds
    public AudioSource[] JumpSounds;

	// Use this for initialization
	void Start () {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }
	
	//Depending on what the obstacle is, which is defined by its tag, play its sound.
	public void PlayDieToObstacleSound(GameObject obj)
    {
        if (obj.name.Contains("Log"))
        {
            DieLogSound.Play();
        }else if(obj.name.Contains("Seagull"))
        {
            DieSeagullSound.Play();
        }else if(obj.name.Contains("Shark"))
        {
            DieSharkSound.Play();
        }else if(obj.name.Contains("Collapse"))
        {
            DieTidalSound.Play();
        }
        else
        {
            Debug.Log(obj.name + " sound undefined");
        }
    }

    //Play a random Jumpsound
    public void PlayJumpSound()
    {
        int ran = Random.Range(0, JumpSounds.GetLength(0));
        JumpSounds[ran].Play();
    }
}
