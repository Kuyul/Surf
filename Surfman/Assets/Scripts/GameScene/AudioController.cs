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
    public AudioSource NormalJumpSound;
    public AudioSource SurfmanJumpSound;
    public AudioSource BarrelJumpSound;
    public AudioSource LifeGuardJumpSound;
    public AudioSource JessJumpSound;


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
        NormalJumpSound.Play();
    }

    public void PlayDoubleJumpSound()
    {
        int activeCharacter = GameControl.instance.getActiveCharacter();

        switch (activeCharacter)
        {
            case 0:
                SurfmanJumpSound.Play();
                break;
            case 1:
                BarrelJumpSound.Play();
                break;
            case 2:
                JessJumpSound.Play();
                break;
            case 3:
                LifeGuardJumpSound.Play();
                break;
            default:
                NormalJumpSound.Play();
                break;
        }
    }
}
