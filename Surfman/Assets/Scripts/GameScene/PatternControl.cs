using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternControl : MonoBehaviour {

    //Singleton Object
    public static PatternControl Instance;

    //Patterns to spawn - Easy/Normal/Hard
    public GameObject[] EasyPatterns;
    public GameObject[] NormalPatterns;
    public GameObject[] HardPatterns;
    public int NextLevelScore = 10000;
    public int PlayerSpeedIncremental = 2;

    //Parameters required for first patter spawn Logic
    public Transform SpawnPoint;
    public GameObject TutorialPattern;
    public GameObject StartPattern;

    //Level Up Sign
    public GameObject LevelUpText;

    //Level indicator
    private int CurrentLevel = 0;


    private int PreviousNo = 0;
    
    private void Start() {

        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }

        if (PlayerPrefs.GetInt("ComTutorial") == 0)
        {
            Instantiate(TutorialPattern, SpawnPoint.position, Quaternion.identity);
        }
        else
        {
            Instantiate(StartPattern, SpawnPoint.position, Quaternion.identity);
        }

    }

    //return set of patterns for the pattern generator to use based on the current score
    // E.g. 
    // cs < 10000           - Easy Patterns
    // 10000 < cs < 20000   - Normal Patterns
    // 20000 < cs           - Hard Patterns
    public GameObject[] FetchPatterns()
    {
        int currentScore = GameControl.instance.GetCurrentScore();
        GameObject[] returnPattern;
        int nextLevel;

        if (currentScore < NextLevelScore)
        {
            nextLevel = 0;
            returnPattern = EasyPatterns;
        }
        else if (currentScore >= NextLevelScore && currentScore < 2 * NextLevelScore)
        {
            PlayerControl.instance.PlayerSpeed = 9;
            nextLevel = 1;
            returnPattern = NormalPatterns;
        }
        else
        {
            PlayerControl.instance.PlayerSpeed = 11;
            nextLevel = 2;
            returnPattern = HardPatterns;
        }
        //When level changes, display the text
        if (CurrentLevel < nextLevel)
        {
            StartCoroutine(LevelUp());
        }
        //Set new Current Level
        CurrentLevel = nextLevel;
        return returnPattern;
    }

    //Returns a pattern given the current state of the game (score and previous pattern)
    public GameObject FeedPattern()
    {
        GameObject[] obj = this.FetchPatterns();

        //We don't want repeating patterns
        int n = Random.Range(0, obj.GetLength(0));
        while (n == PreviousNo)
        {
            n = Random.Range(0, obj.GetLength(0));
        }

        PreviousNo = n;

        return obj[n];
    }

    IEnumerator LevelUp()
    {
        LevelUpText.SetActive(true);
        yield return new WaitForSeconds(2);
        LevelUpText.SetActive(false);
    }
}
